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
    public partial class FinancerPrice : UserControl
    {
        (int, string, int, double)[] Services { get; set; }
        public FinancerPrice()
        {
            InitializeComponent();
            Services = GetAll();
            foreach(var value in Services)
            {
                comboBox1.Items.Add(value.Item2);
            }
            if (Services.Length > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                button1.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
        }

        private (int, string, int, double)[] GetAll()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Service/GetAll");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string s = streamWriter.ReadToEnd();
                    string[] services = s.Split('~');
                    var values = new List<(int, string, int, double)>();
                    for (int i = 0; i < services.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(services[i]))
                        {
                            string[] value = services[i].Split('%');
                            values.Add((int.Parse(value[0]),
                                         value[1],
                                         int.Parse(value[3]),
                                         double.Parse(value[4])));
                        }
                    }
                    return values.ToArray();
                }
            }
            catch (WebException)
            {
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Service/UpdatePrice?id={Services.FirstOrDefault(t => t.Item2 == comboBox1.Text).Item1}&price={(int)numericUpDown1.Value}&commission={(int)numericUpDown2.Value}");
            req.ContentType = "application/json";
            req.Method = "PUT";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string s = streamWriter.ReadToEnd();
                    Services = GetAll();
                }
            }
            catch (WebException)
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = Services.FirstOrDefault(t => t.Item2 == comboBox1.Text);
            numericUpDown1.Value = value.Item3;
            numericUpDown2.Value = (decimal)value.Item4;
        }
    }
}
