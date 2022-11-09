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

namespace NotariusFront
{
    public partial class ClouseDeal : UserControl
    {
        (int, string)[] Open { get; set; }
        public ClouseDeal()
        {
            InitializeComponent();
            ReList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Open.FirstOrDefault(t => t.Item2 == comboBox1.Text).Item1;
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/ToClouse?id={id}");
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
                    ReList();
                }
            }
            catch (WebException)
            {
            }
        }

        private void ReList()
        {
            comboBox1.Items.Clear();
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/GetDone");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string[] s = streamWriter.ReadToEnd().Split('~');
                    if (s.Length != 1 || s[0] != "")
                    {
                        Open = new (int, string)[s.Length];
                        for (int i = 0; i < s.Length; i++)
                        {
                            string[] value = s[i].Split('%');
                            Open[i] = (int.Parse(value[0]), value[1]);
                            comboBox1.Items.Add(Open[i].Item2);
                        }
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.Enabled = false;
                        button1.Enabled = false;
                    }
                }
            }
            catch (WebException)
            {
            }
        }
    }
}
