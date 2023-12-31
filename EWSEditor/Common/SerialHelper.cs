﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
 
namespace EWSEditor.Common
{
    public class SerialHelper
    {

        // Turns a class into a serialized XML string.
        // Example: sConnectionSettings = SerialHelper.SerializeObjectToString<ConnectionSetting>(oConnectionSetting);
        public static string SerializeObjectToString<T>(T obj)
        {

            string sXML = string.Empty;
            MemoryStream oMemoryStream = null;
            XmlTextWriter oXmlTextWriter = null;
            UTF8Encoding oUTF8Encoding = null;
            XmlWriterSettings oXmlWriterSettings = new XmlWriterSettings();

            try
            {
                using (oMemoryStream = new MemoryStream())
                {
                    oXmlWriterSettings.Encoding = Encoding.UTF8;
                    oXmlWriterSettings.Indent = true;
                    //oXmlWriterSettings.IndentChars = "\t";
                    //oXmlWriterSettings.NewLineChars = Environment.NewLine;
                    oXmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;

                    XmlSerializer oXmlSerializer = new XmlSerializer(typeof(T));
                    oXmlTextWriter = new XmlTextWriter(oMemoryStream, Encoding.UTF8);
                    oXmlSerializer.Serialize(oXmlTextWriter, obj);
                    oMemoryStream = (MemoryStream)oXmlTextWriter.BaseStream;
                    oUTF8Encoding = new UTF8Encoding();
                    sXML = oUTF8Encoding.GetString(oMemoryStream.ToArray());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Serializing");
                sXML = string.Empty;
            }

            return sXML;
        }

        // Turns a serialized XML string into an instance of a class.
        // Example: oConnectionSetting = SerialHelper.DeserializeObjectFromString<ConnectionSetting>(sFileContents);
        public static T DeserializeObjectFromString<T>(string xml)
        {
            XmlSerializer oXmlSerializer = null;
            MemoryStream oMemoryStream = null;
            XmlTextWriter oXmlTextWriter = null;
            UTF8Encoding oUTF8Encoding = new UTF8Encoding();

            try
            {
                oXmlSerializer = new XmlSerializer(typeof(T));
                oMemoryStream = new MemoryStream(oUTF8Encoding.GetBytes(xml));
                oXmlTextWriter = new XmlTextWriter(oMemoryStream, Encoding.UTF8);
                return (T)oXmlSerializer.Deserialize(oMemoryStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.ShowDialog(ex.Message, "Error deserializing string");
                return default(T);
            }

        }
    }
}
