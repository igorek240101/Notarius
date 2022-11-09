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
    public partial class Financer : UserControl
    {
        Control WorkControl { get; set; }
        public Financer()
        {
            InitializeComponent();
        }

        private void PriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(WorkControl);
            WorkControl = new FinancerPrice();
            Controls.Add(WorkControl);
        }

        private void MoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(WorkControl);
            WorkControl = new FinancerMoney();
            Controls.Add(WorkControl);
        }
    }
}
