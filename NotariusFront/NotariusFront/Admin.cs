using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotariusFront
{
    public partial class Admin : UserControl
    {
        Control WorkControl { get; set; }

        public Admin()
        {
            InitializeComponent();
        }

        private void NewDealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(WorkControl);
            WorkControl = new NewDeal();
            Controls.Add(WorkControl);
        }
    }
}
