using System.Net;
using EWSUtils;
using Microsoft.Exchange.WebServices.Data;
using RM.Utils.Serialization;
using RM.Utils.Threading;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using System.Configuration;
using EWSEditor.Exchange;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace EWSReader
{
    public class AppointmentReader : RMThread
    {
        public List<string> EmailList = new List<string>();
        public List<string> onlyEmailList = new List<string>();
        public ExchangeService CurrentService;
        public string Username = "";
        public string SavePath = "";
        public NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private List<string> EmailList1;
        private List<string> validCharacter = new List<string>();
        private ExchangeService CurrentService1;
        private string Username1;
        private bool ignoreVCharPostion;
        private string XMLPath;
        private int Interval;
        private List<string> validationCharacter;
        private bool validationCharacterValid;
        private bool connectionEstablished;
        public string EWSUsername, EWSPassword, EWSDomain, EWSUrl;
        ExchangeVersion version;
        string versionSTring = "";
        private List<string> invalidationCharacter;
        private bool invalidationCharacterValid;
        private string eVersion;

        public AppointmentReader(List<string> aEmailList, ExchangeService aCurrentService, string aUsername, string aXMLPath, int aInterval, List<string> aValidationCharacter, List<string> aInvalidationCharacter, bool aValidationCharacterValid, bool aInvalidationCharacterValid, string eVersion, bool IgnoreVCharPosition)
            : base("Read EWS", 60 * 1000 * aInterval)
        {
            versionSTring = eVersion;
            EmailList = aEmailList;
            CurrentService = aCurrentService;
            Username = aUsername;
            SavePath = aXMLPath;
            validCharacter = aValidationCharacter;
            invalidationCharacter = aInvalidationCharacter;
            validationCharacterValid = aValidationCharacterValid;
            invalidationCharacterValid = aInvalidationCharacterValid;
            this.ignoreVCharPostion = IgnoreVCharPosition;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            foreach (string aLine in EmailList)
            {
                if (aLine.Contains(';'))
                {
                    onlyEmailList.Add(aLine.Split(';')[0]);
                }
                else
                {
                    onlyEmailList.Add(aLine);
                }
            }
            logger.Info("AppointmentReader Interval=[" + aInterval + "] initialized");
        }
        private DateTime LastProcessTime = DateTime.MinValue;
        protected override void Process()
        {
            logger.Debug("Start Reading Appointment");
            if (CurrentService == null || IsServiceOnline())
                Init();
            EWSHelper ahelper = new EWSHelper(CurrentService, Username);
            int days = 1;
            if (ConfigurationManager.AppSettings["ReadDays"] != null)
            {
                days = Convert.ToInt32(ConfigurationManager.AppSettings["ReadDays"]);
            }
            List<DataTable> aTable = ahelper.GetAllCalendarFoldersWithAppointments(onlyEmailList, DateTime.Now.Date, DateTime.Now.Date.AddDays(days).Subtract(new TimeSpan(0, 0, 1)), false, false, true);
            logger.Debug("Calendars found: " + aTable.Count);
            List<RoomAppointment> roomAppointments = new List<RoomAppointment>();
            List<RoomAppointment> roomApp = new List<RoomAppointment>();

            if ((validationCharacterValid != invalidationCharacterValid)
                || validationCharacterValid == false && invalidationCharacterValid == false)
            {

                foreach (DataTable table in aTable)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bool entryValid = true;
                        string subj = row["Subject"].ToString();
                        if (validCharacter.Count > 0)
                        {
                            #region Valid ValidationCharacter
                            if (validationCharacterValid)
                            {
                                //Gültigkeitszeichen
                                entryValid = false;
                                foreach (string astring in validCharacter)
                                {
                                    if (!astring.Equals(""))
                                    {
                                        var charPos = subj.IndexOf(astring);
                                        if (charPos == 0 || (ignoreVCharPostion && charPos >= 0))
                                        {
                                            if (ignoreVCharPostion && charPos > 0)
                                                subj = subj.Substring(charPos + 1);
                                            entryValid = true;
                                            logger.Debug("Valid Appointment found: [" + subj + "]");
                                            try
                                            {
                                                if (ConfigurationManager.AppSettings["RemoveValidationCharacter"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["RemoveValidationCharacter"]))
                                                {
                                                    if(charPos==0)
                                                        subj = subj.Substring(astring.Length, subj.Length - astring.Length);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex);
                                            }
                                        }
                                        else
                                        {
                                            entryValid = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Ungültigkeitszeichen
                                foreach (string astring in validCharacter)
                                {
                                    if (!astring.Equals(""))
                                    {
                                        if (subj.IndexOf(astring) == 0)
                                        {
                                            logger.Debug("Invalid Appointment found: [" + subj + "]");
                                            entryValid = false;
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Invalid ValidationCharakter

                            if (invalidationCharacterValid)
                            {
                                //Gültigkeitszeichen
                                entryValid = true;
                                foreach (string astring in invalidationCharacter)
                                {
                                    if (!astring.Equals(""))
                                    {
                                        if (subj.IndexOf(astring) == 0)
                                        {
                                            entryValid = false;
                                            logger.Debug("Invalid Appointment found: [" + subj + "]");
                                        }
                                        else
                                        {
                                            entryValid = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        if (entryValid)
                        {

                            DateTime startDate = Convert.ToDateTime(row["Start"]).Date;
                            DateTime endDate = Convert.ToDateTime(row["End"]).Date;

                            RoomAppointment roomAppointment = new RoomAppointment();

                            roomAppointment.Subject = subj;
                            roomAppointment.StartTime = Convert.ToDateTime(row["StartTime"]).ToShortTimeString();
                            roomAppointment.EndTime = Convert.ToDateTime(row["EndTime"]).ToShortTimeString();
                            roomAppointment.Start = Convert.ToDateTime(row["Start"]).ToString("dd.MM.yyyy");
                            roomAppointment.End = Convert.ToDateTime(row["End"]).ToString("dd.MM.yyyy");
                            roomAppointment.StartTimeForSorting = Convert.ToDateTime(row[2]);
                            roomAppointment.EndTimeForSorting = Convert.ToDateTime(row[3]);
                            roomAppointment.Location = Convert.ToString(row["Location"]);

                            bool aliasExists = false;
                            string stringToWrite = roomAppointment.Room_Alias = row["Kalender"].ToString();
                            foreach (string aLine in EmailList)
                            {
                                if (aLine.Contains(';'))
                                {

                                    string[] splittetLine = aLine.Split(';');

                                    if (stringToWrite.Contains(splittetLine[0]))
                                    {
                                        stringToWrite = splittetLine[1];
                                        break;
                                    }
                                }
                            }
                            roomAppointment.Room_Alias = stringToWrite;
                            roomAppointment.Organizer = row["Organizer"].ToString();

                            roomAppointments.Add(roomAppointment);
                        }
                    }
                }

                roomApp = roomAppointments.OrderBy(x => x.StartTimeForSorting).ToList();

                if (roomApp.Count > 0)
                {
                    logger.Debug("Appointments found: " + roomApp.Count.ToString());

                    try
                    {
                        Serializer.ToFile<List<RoomAppointment>>(roomApp, SavePath);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }

                    logger.Debug("The XML-File was saved to this path: " + SavePath);
                }
                else
                {
                    Serializer.ToFile<List<RoomAppointment>>(new List<RoomAppointment>(), SavePath);
                    logger.Debug("No Appointment found.");
                }
                logger.Debug("End Reading Appointment");
            }
            else
            {
                logger.Info("The Configuration for Valid and Invalid Charakter are the Same. Please Change that.");
            }
        }
        private async void Init()
        {
            //EwsProxyFactory.InitializeWithDefaults(exchangeVersionCombo.SelectedIndex,

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            EwsProxyFactory.RequestedExchangeVersion = ExchangeVersion.Exchange2013;

            EwsProxyFactory.SelectedTimeZone = null;


            EwsProxyFactory.UseDefaultCredentials = false;

            if (ConfigurationManager.AppSettings["appId"] == null)
            {
                if (!EWSDomain.Equals(""))
                {
                    logger.Info("Setting NetworkCredential Username, Password and Domain");
                    EwsProxyFactory.ServiceCredential = new NetworkCredential(
                        EWSUsername.Trim(),
                        EWSPassword.Trim(), //TODO:  This will fail on passwords ending with whitespace
                        EWSDomain.Trim());
                }
                else
                {
                    logger.Info("Setting NetworkCredential Username and Password");
                    EwsProxyFactory.ServiceCredential = new NetworkCredential(EWSUsername.Trim(), EWSPassword.Trim());
                }
            }
            else
            {
                try
                {
                    logger.Info("Setting NetworkCredential to Oauth");
                    var credenatials = await getNetworkCredentials();
                    EwsProxyFactory.Username = EWSUsername.Trim();
                    EwsProxyFactory.OAuthCredential = credenatials;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }



            if (!EWSUrl.Equals(""))
            {
                EwsProxyFactory.EwsUrl = new Uri(EWSUrl.Trim());
                if (!string.IsNullOrEmpty(versionSTring))
                {
                    try
                    {
                        EwsProxyFactory.RequestedExchangeVersion = (ExchangeVersion)Enum.Parse(typeof(ExchangeVersion), versionSTring);
                        version = (ExchangeVersion)Enum.Parse(typeof(ExchangeVersion), versionSTring);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);

                    }
                }
                else
                    EwsProxyFactory.RequestedExchangeVersion = ExchangeVersion.Exchange2010;
                EwsProxyFactory.AllowAutodiscoverRedirect = false;
            }
            else
            {
                EwsProxyFactory.AllowAutodiscoverRedirect = true;
                logger.Info("DoAutodiscover...");
                //EwsProxyFactory.DoAutodiscover();
            }
            EwsProxyFactory.UserToImpersonate = null;


            CurrentService = EwsProxyFactory.CreateExchangeService();

            try
            {
                logger.Debug("Connection Try....");
                CurrentService.ConvertIds(
                    new AlternateId[] { new AlternateId(IdFormat.HexEntryId, "00", "blah@blah.com") },
                    IdFormat.HexEntryId);
                logger.Debug("Connection Established");
                connectionEstablished = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        private async Task<OAuthCredentials> getNetworkCredentials()
        {
            // Using Microsoft.Identity.Client 4.22.0
            var cca = ConfidentialClientApplicationBuilder
                .Create(ConfigurationManager.AppSettings["appId"])
                .WithClientSecret(ConfigurationManager.AppSettings["clientSecret"])
                .WithTenantId(ConfigurationManager.AppSettings["tenantId"])
                .Build();

            // The permission scope required for EWS access
            var ewsScopes = new string[] { "https://outlook.office365.com/.default" };


            try
            {
                //Make the token request
                var authResult = await cca.AcquireTokenForClient(ewsScopes).ExecuteAsync();
                return new OAuthCredentials(authResult.AccessToken);
                // Configure the ExchangeService with the access token
                //var ewsClient = new ExchangeService();
                //ewsClient.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                //ewsClient.Credentials = new OAuthCredentials(authResult.AccessToken);

                //// Make an EWS call
                //var folders = ewsClient.FindFolders(WellKnownFolderName.MsgFolderRoot, new FolderView(10));
                //foreach (var folder in folders)
                //{
                //    Console.WriteLine($"Folder: {folder.DisplayName}");
                //}
            }
            catch (MsalException ex)
            {
                logger.Error($"Error acquiring access token: {ex}");
            }
            catch (Exception ex)
            {
                logger.Error($"Error: {ex}");
            }

            return null;
        }

        private bool IsServiceOnline()
        {
            try
            {
                logger.Debug("Connection Try....");
                CurrentService.ConvertIds(
                    new AlternateId[] { new AlternateId(IdFormat.HexEntryId, "00", "blah@blah.com") },
                    IdFormat.HexEntryId);
                logger.Debug("Connection Established");
                return true;
            }
            catch (Exception ex)
            {
                logger.Warn("Connection: offline");
                return false;
            }
        }
    }
}
