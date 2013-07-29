using Srs.Mobile.WebReference;
using System.Net;
using System.Xml;
using System.Diagnostics;

namespace Srs.Mobile.Common
{
    internal class WebServices
    {
        private readonly server _ws;

        internal WebServices()
        {
            if (_ws == null)
                _ws = new server();
            _ws.Url = ConfigurationSettings.AppSettings["webServiceUrl"];
            _ws.Timeout = int.Parse(ConfigurationSettings.AppSettings["webServiceTimeOut"]); //Time out after 5 secs

        }

        /// <summary>
        /// Method to call the web service for user authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int LoginSucess(string username, string password)
        {
            int loginsucess;

            try
            {
                loginsucess = _ws.login(username, password);
            }
            catch (WebException)
            {
                //Debug.WriteLine("Web Error" + ex);
                loginsucess = 99;
            }
            catch (XmlException ex)
            {
                //this report had an error loading!
                Debug.WriteLine(ex.Message);
                loginsucess = 99;
            }

            return loginsucess;
        }


        /// <summary>
        /// Load aisle information from the web service 
        /// </summary>
        public Aisles[] GetAisleList()
        {
            Aisles[] aislelist = null;
            try
            {
                aislelist = _ws.GetAllAisles();
            }
            catch (WebException)
            {

            }

            return aislelist;

        }

        /// <summary>
        /// Upload stock-take data to the main database
        /// </summary>
        /// <param name="stockItemses"></param>
        /// <param name="stock"></param>
        /// <returns>Upload Status</returns>
        public int UploadStockData(StockItems[] stockItemses, Stock stock)
        {
            int ustatus = 0;

            try
            {
                ustatus = _ws.InsertStockCount(stockItemses, stock);
            }
            catch (WebException)
            {

            }

            return ustatus;
        }

    }
}
