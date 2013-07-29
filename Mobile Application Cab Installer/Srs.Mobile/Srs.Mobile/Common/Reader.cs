using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Srs.Mobile.Properties;
using Symbol.RFID2;
using Symbol.ResourceCoordination;
using TriggerState = Symbol.ResourceCoordination.TriggerState;

namespace Srs.Mobile.Common
{

    /// <summary>
    /// Motorola EMDK sample application has been reffered to implement the following class
    /// </summary>
 
    internal static class Reader
    {
        //RFID reader static variables
        internal static ReaderModel readerModel;
        internal static IRFIDReader _deviceReader;
        internal static Trigger _myTrigger;
        internal static TriggerDevice _triggerDev;

        static Reader()
        {
            readerModel = ReaderModel.MC9090;
            
            //Creates a trigger object
            _triggerDev = new TriggerDevice(
                   TriggerID.ALL_TRIGGERS,
                   (TriggerState[])null);

            _myTrigger = new Trigger(_triggerDev);
            CreateRFIDReader();
        }

        internal static ReaderModel ReaderModel
        {
            get { return readerModel; }
        }

        internal static IRFIDReader DeviceReader
        {
            get { return _deviceReader; }
        }
        internal static Trigger GetTrigger
        {
            get { return _myTrigger; }
        }

        /// <summary>
        /// Creates / Reads the configuration file and initialize the RFID reader
        /// </summary>
        internal static void CreateRFIDReader()
        {
            try
            {
                string strPath = Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
                strPath = strPath.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, @"DeviceReader.Config");

                if (!File.Exists(strPath))
                {
                    CreateFile(strPath);
                }

                //Reads the file for the reader parameters
                var readerStream = new StreamReader(strPath);
                Stream configStream = readerStream.BaseStream;

                var configBytes = new byte[Convert.ToInt32(configStream.Length)];

                configStream.Read(configBytes, 0, configBytes.Length);
                string configStreamStr = Encoding.UTF8.GetString(configBytes, 0, configBytes.Length);

                _deviceReader = ReaderFactory.CreateReader(readerModel, configStreamStr);
            }
            catch
            {
                string configStreamStr = string.Empty;
                if (readerModel == ReaderModel.MC9090)
                {
                    configStreamStr = @"<?xml version='1.0' ?>
                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                        <ComPortSettings>
                                        <COMPort>COM7</COMPort>
                                        <BaudRate>57600</BaudRate>
                                        </ComPortSettings>
                                        <ReaderInfo>
                                        <Model>MC9090</Model>
                                        <StartingQ>6</StartingQ>
                                        <StartingQWrite>6</StartingQWrite>
                                        </ReaderInfo>
                                        </ReaderConfig>";
                }

                _deviceReader = ReaderFactory.CreateReader(readerModel, configStreamStr);
            }
        }

        /// <summary>
        /// Write the MC9090 configuration to a file
        /// </summary>
        /// <param name="path"></param>
        private static void CreateFile(string path)
        {
            
            try
            {
                var wrXmlConfig = new XmlTextWriter(@path, Encoding.UTF8);
                wrXmlConfig.WriteStartDocument();

                wrXmlConfig.WriteStartElement("ReaderConfig");
                wrXmlConfig.WriteAttributeString("xmlns", "xsi", null, @"http://www.w3.org/2001/XMLSchema-instance");
                wrXmlConfig.WriteStartElement("ComPortSettings");


                wrXmlConfig.WriteStartElement("COMPort");

                if (readerModel == ReaderModel.MC9090)
                {
                    wrXmlConfig.WriteString("COM7");
                }
                //else
                //{
                //   wrXmlConfig.WriteString("COM3");
                //}

                wrXmlConfig.WriteEndElement();

                wrXmlConfig.WriteStartElement("BaudRate");
                wrXmlConfig.WriteString("57600");
                wrXmlConfig.WriteEndElement();

                wrXmlConfig.WriteEndElement();
                wrXmlConfig.WriteStartElement("ReaderInfo");

                wrXmlConfig.WriteStartElement("Model");
                if (readerModel == ReaderModel.MC9090)
                {
                    wrXmlConfig.WriteString("MC9090");
                }
                //else
                //{ // not supported
                //wrXmlConfig.WriteString("MC9000");
                //}
                wrXmlConfig.WriteEndElement();

                wrXmlConfig.WriteEndElement();
                wrXmlConfig.WriteEndElement();

                wrXmlConfig.WriteEndDocument();
                wrXmlConfig.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error:{0}", ex.Message), Resources.Srs_Title);
              
            }
            
        }

        /// <summary>
        /// Close the reader
        /// </summary>
        internal static void Shutdown()
        {
            if (_deviceReader != null)
            {
                _deviceReader.ReadMode = ReadMode.ONDEMAND;
            }

            Thread.Sleep(50);

            if (_deviceReader != null)
            {
                _deviceReader.Disconnect();
            }

            Thread.Sleep(50);
            //Dispose the trigger
            if (_myTrigger != null)
            {
                _myTrigger.Dispose();
            }
        }
    }
}