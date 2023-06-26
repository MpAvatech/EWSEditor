using EWSEditor.Exchange;
using EWSEditor.Forms;
using EWSEditor.Resources;
using EWSUtils;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Identity.Client;
using NLog;
using RM.Utils.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EWSEditor.CustomForm
{
    public partial class EWSTest : Form
    {
        ExchangeService _service = null;
        public List<string> lines;
        static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public ExchangeService CurrentService
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
            }
        }

        public EWSTest()
        {
            try
            {
                InitializeComponent();
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                ExchangeServiceURLText.Text = ConfigurationManager.AppSettings["EWSUrl"].ToString();
                txtUserName.Text = ConfigurationManager.AppSettings["Username"].ToString();
                txtPassword.Text = ConfigurationManager.AppSettings["Password"].ToString();
                txtDomain.Text = ConfigurationManager.AppSettings["Domain"].ToString();

                lines = System.IO.File.ReadAllLines(Path.Combine(Application.StartupPath, "EmailList.txt")).ToList();

                if (lines.Count > 0)
                {
                    foreach (string astring in lines)
                    {
                        textBox_SharedCalendarUser.Text += (astring + Environment.NewLine);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Validation for credential input...
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            if ((txtUserName.Text.Length == 0 || txtPassword.Text.Length == 0))
                {
                    ErrorDialog.ShowInfo(DisplayStrings.MSG_SPECIFY_CREDS);
                    return;
                }


                // Validation for URL input...
                if (String.IsNullOrEmpty(this.ExchangeServiceURLText.Text))
                {
                    ErrorDialog.ShowInfo(DisplayStrings.MSG_SERVICE_REQ);
                    return;
                }

                try
                {
                    Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    //EwsProxyFactory.InitializeWithDefaults(exchangeVersionCombo.SelectedIndex,
                    EwsProxyFactory.RequestedExchangeVersion = ExchangeVersion.Exchange2013;// Exchange2010_SP2;

                    EwsProxyFactory.SelectedTimeZone = null;

                    EwsProxyFactory.AllowAutodiscoverRedirect = false;
                    EwsProxyFactory.UseDefaultCredentials = false;
                //EwsProxyFactory.ServiceCredential = new NetworkCredential(
                //        this.txtUserName.Text.Trim(),
                //        this.txtPassword.Text.Trim(), //TODO:  This will fail on passwords ending with whitespace
                //        this.txtDomain.Text.Trim());
                    var credenatials = await getNetworkCredentials();
                    EwsProxyFactory.OAuthCredential = credenatials;

                    EwsProxyFactory.EwsUrl = new Uri(ExchangeServiceURLText.Text.Trim());
                    EwsProxyFactory.RequestedExchangeVersion = ExchangeVersion.Exchange2010;
                    //EwsProxyFactory.UserToImpersonate = null;


                    CurrentService = EwsProxyFactory.CreateExchangeService();

                    bool connectionEstablished = false;

                    try
                    {
                        CurrentService.ConvertIds(
                            new AlternateId[] { new AlternateId(IdFormat.HexEntryId, "00", "blah@blah.com") },
                            IdFormat.HexEntryId);
                    

                        connectionEstablished = true;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (connectionEstablished)
                    {
                        List<string> sharedList = new List<string>();

                        foreach (string line in textBox_SharedCalendarUser.Lines)
                        {
                            if (line.Contains(';'))
                            {
                                sharedList.Add(line.Split(';')[0]);
                            }
                            else
                            {
                                sharedList.Add(line);
                            }
                            
                        }

                        EWSHelper ahelper = new EWSHelper(CurrentService, txtUserName.Text);

                        int days = 1;
                        if (ConfigurationManager.AppSettings["ReadDays"] != null)
                        {
                            days = Convert.ToInt32(ConfigurationManager.AppSettings["ReadDays"]);
                        }
                        List<DataTable> aTable = ahelper.GetAllCalendarFoldersWithAppointments(sharedList, DateTime.Now.Date, DateTime.Now.Date.AddDays(days).Subtract(new TimeSpan(0, 0, 1)), false, false, true);




                        List<RoomAppointment> roomAppointments = new List<RoomAppointment>();
                        List<RoomAppointment> roomApp = new List<RoomAppointment>();
                        foreach (DataTable table in aTable)
                        {

                            foreach (DataRow row in table.Rows)
                            {
                                RoomAppointment roomAppointment = new RoomAppointment();


                                DateTime startDate = Convert.ToDateTime(row["Start"]).Date;
                                DateTime endDate = Convert.ToDateTime(row["End"]).Date;


                                roomAppointment.Subject = row["Subject"].ToString();
                                roomAppointment.StartTime = Convert.ToDateTime(row["StartTime"]).ToShortTimeString();
                                roomAppointment.EndTime = Convert.ToDateTime(row["EndTime"]).ToShortTimeString();
                                roomAppointment.Start = Convert.ToDateTime(row["Start"]).ToString("dd.MM.yyyy");
                                roomAppointment.End = Convert.ToDateTime(row["End"]).ToString("dd.MM.yyyy");
                                roomAppointment.StartTimeForSorting = Convert.ToDateTime(row[2]);
                                roomAppointment.EndTimeForSorting = Convert.ToDateTime(row[3]);
                                roomAppointment.Location = Convert.ToString(row["Location"]);


                                bool aliasExists = false;
                                string stringToWrite = roomAppointment.Room_Alias = row["Kalender"].ToString();
                                foreach (string aLine in textBox_SharedCalendarUser.Lines)
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

                        roomApp = roomAppointments.OrderBy(x => x.StartTimeForSorting).ToList();

                        if (roomApp.Count > 0)
                        {
                            string savePath = Path.Combine(Application.StartupPath, "XMLFile");

                            if (!Directory.Exists(savePath))
                                Directory.CreateDirectory(savePath);

                            Serializer.ToFile<List<RoomAppointment>>(roomApp, Path.Combine(Application.StartupPath, "XMLFile", "Output.xml"));
                            MessageBox.Show("The XML-File was saved to this path: " + Path.Combine(Application.StartupPath, "XMLFile", "Output.xml"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No Appointment found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                finally
                {
                    Cursor = System.Windows.Forms.Cursors.Default;
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
                Console.WriteLine($"Error acquiring access token: {ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

            return null;
        }
    }
}
