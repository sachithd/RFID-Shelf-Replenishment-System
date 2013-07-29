using System;
using System.Windows.Forms;

namespace Srs.Mobile.UI
{
    public partial class FormReaderSettings : Form
    {
        internal FormReaderSettings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}