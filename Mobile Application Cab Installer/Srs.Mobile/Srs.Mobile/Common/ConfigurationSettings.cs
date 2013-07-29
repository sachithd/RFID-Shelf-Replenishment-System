using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Srs.Mobile.Properties;

namespace Srs.Mobile.Common
{
    /// <summary>
    /// The following code was refered from the following link
    /// http://www.eggheadcafe.com/articles/dotnetcompactframework_app_config.asp
    /// </summary>
    internal class ConfigurationSettings
    {
        public static NameValueCollection AppSettings;

        public static void LoadConfig()
        {
            
            string strPath = Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            strPath = strPath.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, @"App.config");


            if (File.Exists(strPath) == false)
            {
                MessageBox.Show(Resources.Config_Not_Exists);
                Application.Exit();
                return;
            }

            var oXml = new XmlDocument();

            oXml.Load(strPath);

            XmlNodeList oList = oXml.GetElementsByTagName("appSettings");

            AppSettings = new NameValueCollection();

            foreach (XmlNode oNode in oList)
            {
                foreach (XmlNode oKey in oNode.ChildNodes)
                {
                    AppSettings.Add(oKey.Attributes["key"].Value, oKey.Attributes["value"].Value);
                }
            }

        }
    }
}