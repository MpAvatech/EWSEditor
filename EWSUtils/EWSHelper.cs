using Microsoft.Exchange.WebServices.Data;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace EWSUtils
{
    public class EWSHelper
    {
        private ExchangeService actualService;
        List<string> SharedCalendarUsers = new List<string>();
        private string Mailbox = "";
        private string msg = "";
        Logger logger = LogManager.GetCurrentClassLogger();

        public EWSHelper(ExchangeService aService, string aMailbox)
        {
            actualService = aService;
            Mailbox = aMailbox;
        }

        public List<DataTable> GetAllCalendarFoldersWithAppointments(List<string> aSharedCalenderList, DateTime startTime, DateTime endTime, bool ownFolder,
                                                     bool publicFolder, bool sharedFolder)
        {
            CalendarView calendarView = new CalendarView(startTime, endTime);
            List<DataTable> calendarCollection = new List<DataTable>();
            PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties);
            propertySet.RequestedBodyType = BodyType.Text;

            try
            {
                List<Folder> allCalendarFolders = GetAllCalendars(true,ownFolder,publicFolder,sharedFolder, aSharedCalenderList);

                msg = "GetAllCAlendars ok with " + allCalendarFolders.Count + " Folders";
                logger.Debug(msg);
                //DoLog(msg);

                foreach (Folder folder in allCalendarFolders)
                {
                    if (folder is CalendarFolder)
                    {
                        try
                        {
                            DataTable calendar = GetAppointmentsOfFolder(folder, startTime, endTime);
                            calendarCollection.Add(calendar);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                            msg = "Failure getting Appointments of folder [" + folder.DisplayName + "] " + ex.Message + "; " + ex.StackTrace;
                            logger.Debug(msg);
                            //DoLog(msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                msg = "Failure " + ex.Message + "; " + ex.StackTrace;
                logger.Debug(msg);
                //DoLog(msg);
            }
            msg = "GetAllCalendarFoldersWithAppointments(Main) ok";
            logger.Info(msg);
            //DoLog(msg);
            return calendarCollection;
        }
        public List<Folder> GetAllCalendars(bool suppressDeletedItems, bool ownFolder, bool publicFolder, bool sharedFolder, List<string> aSharedCalenderLIst)
        {
            msg = "GetAllCalendarFolders with suppressDeletedItems = [" + suppressDeletedItems + "]";
            logger.Debug(msg);
            //DoLog(msg);
            //Folder rootFolder = Folder.Bind(service, WellKnownFolderName.Root);

            //deletedItems = Folder.Bind(service, WellKnownFolderName.DeletedItems);
            //rootFolder.Load();
            //deletedItems.Load();
            //Sämtliche Ordner, mit und ohne Kalender
            SharedCalendarUsers = aSharedCalenderLIst;
            List<Folder> totalFolderList = new List<Folder>();

            try
            {
                if (publicFolder)
                    totalFolderList.AddRange(FindPublicFolders());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                //msg = "Failure " + ex.Message + "; " + ex.StackTrace;

                //DoLog(msg);
            }

            try
            {
                if (sharedFolder)
                    totalFolderList.AddRange(FindSharedCalendars());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                //msg = "Failure " + ex.Message + "; " + ex.StackTrace;

                //DoLog(msg);
            }

            try
            {
                if (ownFolder)
                    totalFolderList.AddRange(FindOwnFolders());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                //msg = "Failure " + ex.Message + "; " + ex.StackTrace;

                //DoLog(msg);
            }

            List<Folder> allCalendarFolders = new List<Folder>();

            if (!(totalFolderList.Equals(null)))
            {

                foreach (Folder folder in totalFolderList)
                {
                    allCalendarFolders.AddRange(FindAllCalendars(folder));
                }
            }
            if (suppressDeletedItems)
            {
                //Ordner, die sich im Abfalleimer befinden entfernen
                try
                {
                    Folder deletedItems = Folder.Bind(actualService, WellKnownFolderName.DeletedItems);
                    FindFoldersResults delRes = deletedItems.FindFolders(new FolderView(int.MaxValue));
                    List<FolderId> folderIds = new List<FolderId>();

                    foreach (Folder folder in delRes)
                    {
                        folderIds.Add(folder.Id);
                    }
                    foreach (Folder folder in allCalendarFolders.ToArray())
                    {
                        if (folderIds.Contains(folder.Id)) allCalendarFolders.Remove(folder);
                    }
                }
                catch (Exception ex)
                {
                    msg = "Failure on loading deleted items: " + ex.Message + "; " + ex.StackTrace;
                    logger.Error(ex);
                    //DoLog(msg);
                }
            }
            msg = "GetAllCalendarFolders ok with " + suppressDeletedItems + " Folders";
            logger.Info(msg);
            //DoLog(msg);
            return allCalendarFolders;
        }

        /// <summary>
        /// Geht das Rootverzeichnis durch und gibt alle Folders in einer List zurück, mit ausnahme von System, auf das man keinen Zugriff hat.
        /// Es würde eine Exception auslösen. Der Displayname jedes Ordners wir mit "mailbox" belegt, weil dieser Später zur Identifikation des
        /// Users des Ordners benötigt wird. Keine schöne Lösung, wird aber solange sie keine Fehler verursacht so beibehalten.
        /// </summary>
        /// <returns></returns>
        private List<Folder> FindOwnFolders()
        {
            msg = "FindOwnFolders called. Binding WellKnownFolder.Root";
            logger.Debug(msg);
            //DoLog(msg);
            List<Folder> folderList = new List<Folder>();
            try
            {
                Folder rootFolder = Folder.Bind(actualService, WellKnownFolderName.Root);
                msg = "Binding WellKnownFolder.Root ok, loading Folder";
                logger.Debug(msg);
                //DoLog(msg);
                rootFolder.Load();

                FindFoldersResults folderResults;
                folderResults = null;

                msg = "WellKnownFolder.Root loaded, Calling FindFolders and check DisplayName != System";
                logger.Debug(msg);
                //DoLog(msg);
                folderResults = rootFolder.FindFolders(new FolderView(int.MaxValue));

                foreach (Folder f in folderResults)
                {
                    if (!f.DisplayName.Equals("System"))
                    {
                        f.DisplayName = Mailbox;
                    }
                    folderList.Add(f);
                }
            }
            catch (Exception ex)
            {
                msg = "Failure in finding OwnFolders: " + ex.Message + "; " + ex.StackTrace;
                logger.Error(ex);
                //DoLog(msg);
            }
            msg = "FindOwnFolders finished with " + folderList.Count + "Folders";
            logger.Info(msg);
            //DoLog(msg);
            return folderList;
        }

        /// <summary>
        /// Gibt alle Ordner der Public Folders in einer Liste zurück
        /// </summary>
        /// <returns></returns>
        private List<Folder> FindPublicFolders()
        {
            msg = "FindPublicFolders called";
            logger.Debug(msg);
            //DoLog(msg);
            List<Folder> folderList = new List<Folder>();

            FindFoldersResults folderResults;
            folderResults = null;
            folderResults = actualService.FindFolders(WellKnownFolderName.PublicFoldersRoot, new FolderView(int.MaxValue));

            foreach (Folder f in folderResults)
            {
                folderList.Add(f);
            }
            //msg = "FindPublicFolders ok with " + folderList.Count + " Folders";
            //logger.Info(msg);
            //DoLog(msg);
            return folderList;
        }

        /// <summary>
        /// Gibt alle freigegebenen Kalender zurück
        /// </summary>
        /// <returns></returns>
        public List<Folder> FindSharedCalendars()
        {
            msg = "FindSharedCalendars called";
            logger.Debug(msg);
            //DoLog(msg);
            List<Folder> folderList = new List<Folder>();

            //foreach (DataRow r in sharedCalendarUsers.Rows)
            if (SharedCalendarUsers != null)
            {
                msg = "Amount Shared Calendar Users [" + SharedCalendarUsers.Count.ToString() + "]...";
                logger.Debug(msg);
                //DoLog(msg);
                foreach (String aSharedCalendar in SharedCalendarUsers)
                {
                    try
                    {
                        if (!aSharedCalendar.Equals(""))
                        {
                            //FolderId sharedFolderID = new FolderId(WellKnownFolderName.Calendar, (string)r["Exchange_Mailbox"]);
                            FolderId sharedFolderID = new FolderId(WellKnownFolderName.Calendar, aSharedCalendar);
                            msg = "Binding and Loading Calendar [" + aSharedCalendar + "]...";
                            logger.Debug(msg);
                            //DoLog(msg);
                            Folder sharedFolder = Folder.Bind(actualService, sharedFolderID);
                            sharedFolder.Load();
                            sharedFolder.DisplayName = (aSharedCalendar + "\\" + sharedFolder.DisplayName);
                            folderList.Add(sharedFolder);
                            msg = "Calendar [" + sharedFolder.DisplayName + "] found";
                            logger.Debug(msg);
                            //DoLog(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "Failure Binding and loading SharedCalendar[" + aSharedCalendar + "]: " + ex.Message + "; " + ex.StackTrace;
                        logger.Error(msg);
                        logger.Error(ex);
                        //DoLog(msg);
                    }
                }
            }
            msg = "FindSharedCalendars ok with " + folderList.Count + " Folders";
            logger.Info(msg);
            //DoLog(msg);
            return folderList;
        }
        public DataTable GetAppointmentsOfFolder(Folder aFolder, DateTime start, DateTime end)
        {
            msg = "GetAppointmentsOfFolder with Folder [" + aFolder.DisplayName + "], start = [" + start.ToString() + "], end = [" + end.ToString() + "]";
            logger.Debug(msg);
            //DoLog(msg);

            DataTable calendar = new DataTable();
            calendar.Clear();
            calendar.Columns.Add("StartTime", typeof(string));
            calendar.Columns.Add("EndTime", typeof(string));
            calendar.Columns.Add("Start", typeof(DateTime));
            calendar.Columns.Add("End", typeof(DateTime));
            //calendar.Columns.Add("Date", typeof(DateTime));
            calendar.Columns.Add("Subject", typeof(string));
            calendar.Columns.Add("Organizer");
            calendar.Columns.Add("Location");
            calendar.Columns.Add("Duration");
            calendar.Columns.Add("Body");
            calendar.Columns.Add("Room_Place");
            calendar.Columns.Add("Kalendarfolder");
            calendar.Columns.Add("Room_Alias");
            calendar.Columns.Add("Date", typeof(string));
            calendar.Columns.Add("Attachements", typeof(bool));
            calendar.Columns.Add("Kalendername", typeof(string));
            calendar.Columns.Add("Kalender", typeof(string));
            //calendar.Columns.Add("ParentFolder");

            calendar.TableName = aFolder.DisplayName;
            calendar.Clear();

            //DateTime EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            //CalendarView calendarView = new CalendarView(start, end);//EndTime); // statt end

            DateTime EndTime = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            CalendarView calendarView = new CalendarView(start, EndTime);
            PropertySet propertySet = new PropertySet(BasePropertySet.FirstClassProperties);
            propertySet.RequestedBodyType = BodyType.Text;

            FindItemsResults<Appointment> appointments = ((CalendarFolder)aFolder).FindAppointments(calendarView);
            msg = appointments.TotalCount + " Appointments found";
            logger.Debug(msg);
            //DoLog(msg);


            List<Item> items = new List<Item>();
            items.AddRange(appointments);

            if (items.Count > 0)
            {
                try
                {
                    actualService.LoadPropertiesForItems(items, propertySet);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex);
                }
                foreach (Item i in items)
                {
                    bool isPrivate = false;
                    Appointment a = (Appointment) i;

                    try
                    {
                        a.Load(propertySet);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn(ex);
                    }

                    //Bugfix: Appointment that is cancelled
                    if (a.IsCancelled)
                    {
                        logger.Debug("Appointment [" + a.Subject + "/" + a.Location + "/" + a.Start + "/" + a.End + "] wegen IsCancelled gelöscht.");
                        continue;
                    }
                    else
                    {
                        if (a.Sensitivity == Sensitivity.Private)
                        {
                            isPrivate = true;
                            logger.Debug("Private Appointment [" + a.Subject + "/" + a.Location + "/" + a.Start + "/" + a.End + "] found.");
                        }
                    }
                    


                    if (a.HasAttachments)
                    {
                        foreach (Attachment attachment in a.Attachments)
                        {
                            try
                            {

                                if (attachment is FileAttachment)
                                {
                                    FileAttachment file = (FileAttachment)attachment;
                                    //file.Load("d:\\temp\\"+file.Name);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Warn(ex.Message);
                            }
                        }
                    }
                    string organizer = "";
                    try
                    {
                        organizer = a.Organizer.ToString();
                        if (ConfigurationManager.AppSettings["CleanUpOrganizer"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["CleanUpOrganizer"]))
                        {
                            string[] spilletorganizer = organizer.Split(' ');
                            logger.Debug("Splitting organizer found elements [" + spilletorganizer.Length + "]");

                            int counter = 0;
                            foreach (String temp in spilletorganizer)
                            {
                                if (!temp.StartsWith("<"))
                                {
                                    if (counter.Equals(0))
                                    {
                                        organizer = temp;
                                    }
                                    else
                                    {
                                        organizer += " " + temp;
                                    }
                                    counter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Warn(ex.Message);
                    }

                    
                    DataRow dr = calendar.NewRow();
                    dr["Start"] = a.Start;
                    dr["End"] = a.End;
                    dr["StartTime"] = a.Start.ToLongTimeString();
                    dr["EndTime"] = a.End.ToLongTimeString();
                    if (!isPrivate)
                    {
                        dr["Subject"] = a.Subject;
                    }
                    else
                    {
                        dr["Subject"] = "Privater Termin";
                    }
                    dr["Organizer"] = organizer;
                    dr["Location"] = a.Location;
                    dr["Duration"] = a.Duration;
                    dr["Body"] = a.Body;
                    string[] roomPlace = (aFolder.DisplayName).Split('\\');
                    dr["Room_Place"] = roomPlace.Last();
                    dr["Kalendername"] = roomPlace.Last();
                    dr["Kalender"] = aFolder.DisplayName;
                    dr["Attachements"] = a.HasAttachments;
                    dr["Date"] = a.Start.Date;
                    calendar.Rows.Add(dr);
                    msg = "Appointment [" + a.Subject + ", " + a.Start.ToShortTimeString() + "-" + a.End.ToShortTimeString() + " Organizer="+organizer+"] in Calendar [" + aFolder.DisplayName + "] found";
                    logger.Debug(msg);
                    //DoLog(msg);
                }
            }
            msg = "GetAppointmentsOfFolder ok with " + calendar.Rows.Count + " Appointments for Folder [" + aFolder.DisplayName + "]";
            logger.Info(msg);
            //DoLog(msg);
            return calendar;
        }

        private List<Folder> FindAllCalendars(Folder aFolder)
        {
            msg = "FindAllCalendars with Folder [" + aFolder.DisplayName + "] called";
            logger.Debug(msg);
            //DoLog(msg);
            FolderView view = new FolderView(int.MaxValue);
            List<Folder> folderList = new List<Folder>();

            if (aFolder is CalendarFolder)
            {
                //aFolder.DisplayName = (mailBox + "\\" + aFolder.DisplayName); 
                folderList.Add(aFolder);
                msg = "CalendarFolder [" + aFolder.DisplayName + "] found";
                logger.Debug(msg);
                //DoLog(msg);
            }
            //Keine Berechtigung für System
            if (aFolder.DisplayName != null && !aFolder.DisplayName.Equals("System"))
            {
                try
                {
                    FindFoldersResults fres = aFolder.FindFolders(view);

                    foreach (Folder folder in fres)
                    {
                        if (folder is CalendarFolder)
                        {
                            CalendarFolder cf = (CalendarFolder)folder;
                            //if (!OnlyPublicFolders && !OnlySharedCalendars)
                            //{
                            folder.DisplayName = aFolder.DisplayName + "\\" + folder.DisplayName;
                            //}
                            //else
                            //{
                            //folder.DisplayName = aFolder.DisplayName + "\\" + folder.DisplayName;
                            //}

                        }
                        List<Folder> aTempList = FindAllCalendars(folder);
                        folderList.AddRange(aTempList);
                    }
                }
                catch (Exception ex)
                {
                    msg = "FindAllCalendars failure: " + ex.Message + "; " + ex.StackTrace;
                    logger.Error(ex);
                    //DoLog(msg);

                    throw ex;
                }
            }
            msg = "FindAllCalendars ok with " + folderList.Count + " Folders";
            logger.Debug(msg);
            //DoLog(msg);
            return folderList;
        }
    }
}
