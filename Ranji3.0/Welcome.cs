using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ranji3._0
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        Rectangle rect = System.Windows.Forms.SystemInformation.VirtualScreen;
        private void Welcome_Load(object sender, EventArgs e)
        {
            this.Top = rect.Height / 2 - this.Height / 2;
            this.Left = rect.Width / 2 - this.Width / 2;
        }
    }
}
