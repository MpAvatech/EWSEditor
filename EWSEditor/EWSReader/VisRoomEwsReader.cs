using EWSEditor.Exchange;
using Microsoft.Exchange.WebServices.Data;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;


namespace EWSReader
{
    public partial class VisRoomEwsReader2 : ServiceBase
    {
        public string EWSUrl { get; set; }
        public string Username { get; set; }
        public string AppId { get; set; }
        public string TenandId { get; set; }
        public string ClientSecret { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string XMLPath { get; set; }
        public string EVersion { get; set; }
        public bool IgnoreVCharPosition { get; set; }
        public bool validationCharacterValid { get; set; }
        public bool invalidationCharacterValid { get; set; }
        public int Interval { get; set; }
        public List<string> EmailList { get; set; }
        AppointmentReader readerThread;
        public List<string> validationCharacter = new List<string>();
        public List<string> invalidationCharacter = new List<string>();

        public Logger logger = LogManager.GetCurrentClassLogger();
        public bool connectionEstablished = false;

        public ExchangeService CurrentService;

        public VisRoomEwsReader2()
        {
            InitializeComponent();
            
            
        }

       /* private void init()
        {
            //EwsProxyFactory.InitializeWithDefaults(exchangeVersionCombo.SelectedIndex,
            EwsProxyFactory.RequestedExchangeVersion = ExchangeVersion.Exchange2010_SP2;

            EwsProxyFactory.SelectedTimeZone = null;

            
            EwsProxyFactory.UseDefaultCredentials = false;
            if (!Domain.Equals(""))
            {
                logger.Info("Setting NetworkCredential Username, Password and Domain");
                EwsProxyFactory.ServiceCredential = new NetworkCredential(
                    Username.Trim(),
                    Password.Trim(), //TODO:  This will fail on passwords ending with whitespace
                    Domain.Trim());
            }
            else
            {
                logger.Info("Setting NetworkCredential Username and Password");
                EwsProxyFactory.ServiceCredential = new NetworkCredential(Username.Trim(), Password.Trim());
            }
            if (!EWSUrl.Equals(""))
            {
                EwsProxyFactory.EwsUrl = new Uri(EWSUrl.Trim());
                EwsProxyFactory.AllowAutodiscoverRedirect = false;
            }
            else
            {
                EwsProxyFactory.AllowAutodiscoverRedirect = true;
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
        */
        private void StartReaderThread()
        {
            logger.Debug("Starting ReaderThread");
            readerThread = new AppointmentReader(EmailList, CurrentService, Username, XMLPath, Interval, validationCharacter, invalidationCharacter, validationCharacterValid, invalidationCharacterValid, EVersion, IgnoreVCharPosition);
            readerThread.EWSDomain = Domain;
            readerThread.EWSUrl = EWSUrl;
            readerThread.EWSUsername = Username;
            readerThread.EWSPassword = Password;
            readerThread.Start();
            logger.Debug("ReaderThread Started");
        }

        protected override void OnStart(string[] args)
        {
            EWSUrl = Username = Password = Domain = "";

            logger.Info("Starting Service");
            try
            {
                EVersion = "";
                if (ConfigurationManager.AppSettings["ExchangeVersion"] != null)
                    EVersion = ConfigurationManager.AppSettings["ExchangeVersion"].ToString();

                if (ConfigurationManager.AppSettings["IgnoreValidationCharacterPosition"] != null)
                    IgnoreVCharPosition = Convert.ToBoolean(ConfigurationManager.AppSettings["IgnoreValidationCharacterPosition"]);

                if (ConfigurationManager.AppSettings["EWSUrl"] != null)
                    EWSUrl = ConfigurationManager.AppSettings["EWSUrl"].ToString();

                if (ConfigurationManager.AppSettings["appId"] != null)
                    AppId = ConfigurationManager.AppSettings["appId"].ToString();

                if (ConfigurationManager.AppSettings["clientSecret"] != null)
                    AppId = ConfigurationManager.AppSettings["clientSecret"].ToString();

                if (ConfigurationManager.AppSettings["tenantId"] != null)
                    AppId = ConfigurationManager.AppSettings["tenantId"].ToString();

                if (ConfigurationManager.AppSettings["Username"] != null)
                    Username = ConfigurationManager.AppSettings["Username"].ToString();

                if (ConfigurationManager.AppSettings["Password"] != null)
                    Password = ConfigurationManager.AppSettings["Password"].ToString();

                if (ConfigurationManager.AppSettings["Domain"] != null)
                    Domain = ConfigurationManager.AppSettings["Domain"].ToString();

                if (ConfigurationManager.AppSettings["XMLSavePath"] != null)
                    XMLPath = ConfigurationManager.AppSettings["XMLSavePath"].ToString();

                if (ConfigurationManager.AppSettings["ValidationCharacterValid"] != null)
                    validationCharacterValid = Convert.ToBoolean(ConfigurationManager.AppSettings["ValidationCharacterValid"]);

                if (ConfigurationManager.AppSettings["InvalidationCharacterValid"] != null)
                    invalidationCharacterValid = Convert.ToBoolean(ConfigurationManager.AppSettings["InvalidationCharacterValid"]);

                if (ConfigurationManager.AppSettings["Interval"] != null)
                {
                    try
                    {
                        Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
                string emailfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailList.txt");
                logger.Info("Reading Email File: [" + emailfile + "]");
                EmailList = System.IO.File.ReadAllLines(emailfile).ToList();
                foreach (string s in EmailList)
                {
                    logger.Info("Email ["+s+"] found in EmailList.txt");
                }

                logger.Info("EWSUrl = " + EWSUrl + Environment.NewLine +
                    "Username = " + Username + Environment.NewLine +
                    "Password = " + Password + Environment.NewLine +
                    "Domain = " + Domain + Environment.NewLine +
                    "XMLSavePath = " + XMLPath);

                bool validationSuccessful = true;
                if (Username.Equals("") || Password.Equals("") || XMLPath.Equals(""))
                {
                    validationSuccessful = false;
                    throw new Exception("EwsReader ServiceConfiguration not valid. SERVICE END.");

                }

                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ValidationCharacterValid.txt");
                logger.Info("Reading Validation File: [" + file + "]");
                if (File.Exists(file))
                {
                    validationCharacter = File.ReadAllLines(file).ToList();
                    foreach (string s in validationCharacter)
                    {
                        logger.Info("Validation character [" + s + "] found in ValidationCharacterValid.txt");
                    }
                }

                string file2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ValidationCharacterInvalid.txt");
                logger.Info("Reading Invalidation File: [" + file + "]");
                if (File.Exists(file))
                {
                    invalidationCharacter = File.ReadAllLines(file).ToList();
                    foreach (string s in invalidationCharacter)
                    {
                        logger.Info("Invalidation character [" + s + "] found in ValidationCharacterInvalid.txt");
                    }
                }

                if (EmailList.Count == 0)
                {
                    validationSuccessful = false;
                    logger.Warn("Now EmailAdress-list found");
                    //throw new Exception("EwsReader List of Email Adresses not valid. SERVICE END.");
                }
                /*if (validationSuccessful)
                    init();

                if (connectionEstablished)
                    StartReaderThread();*/
                logger.Info("Validation successful: " + validationSuccessful);
                if (validationSuccessful)
                {
                    StartReaderThread();
                    logger.Info("Appointmentreader Thread started");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (readerThread != null)
                {
                    readerThread.Stop();
                    readerThread.Kill();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            logger.Info("Service stopped");
        }
    }
}
