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
    public partial class Notarius : UserControl
    {
        Control WorkControl { get; set; }
        public Notarius()
        {
            InitializeComponent();
        }

        private void ServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(WorkControl);
            WorkControl = new NotariusService();
            Controls.Add(WorkControl);
        }

        private void DoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(WorkControl);
            WorkControl = new NotariusWork();
            Controls.Add(WorkControl);
        }
    }
}
