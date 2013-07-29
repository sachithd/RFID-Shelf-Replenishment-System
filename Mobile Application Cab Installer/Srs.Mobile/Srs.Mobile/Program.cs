using System;
using System.Windows.Forms;
using Srs.Mobile.Common;
using Srs.Mobile.UI;

namespace Srs.Mobile
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            ConfigurationSettings.LoadConfig();
            Application.Run(new FormMain());
        }
    }
}