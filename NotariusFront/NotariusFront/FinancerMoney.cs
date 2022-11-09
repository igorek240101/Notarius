using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NotariusFront
{
    public partial class FinancerMoney : UserControl
    {
        public FinancerMoney()
        {
            InitializeComponent();
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp(
                $"https://localhost:7086/Deal/GetSum?" +
                $"start={dateTimePicker1.Value.ToString("MM.dd.yyyy")}" +
                $"&end={dateTimePicker2.Value.ToString("MM.dd.yyyy")}");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {

                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    label1.Text = streamWriter.ReadToEnd();
                }
            }
            catch (WebException)
            {
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value >= dateTimePicker1.Value) button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value >= dateTimePicker1.Value) button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
