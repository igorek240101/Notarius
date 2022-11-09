using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotariusFront
{
    public partial class NewDeal : UserControl
    {
        private (int, string, string, int, double)[] Services;

        private int? Id { get; set; }

        public NewDeal()
        {
            InitializeComponent();
            radioButton1_CheckedChanged(this, null);
            Services = GetServices();
            foreach(var value in  Services)
            {
                comboBox2.Items.Add(value.Item2);
            }
            if(Services.Length != 0)
            {
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Text = "Добавить";
            Id = null;
            IsDone();
            textBox1.Enabled = radioButton1.Checked;
            textBox2.Enabled = !radioButton1.Checked;
            textBox3.Enabled = !radioButton1.Checked;
            textBox4.Enabled = !radioButton1.Checked;
            comboBox1.Enabled = !radioButton1.Checked;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
            button2.Enabled =!radioButton1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Client/Get?name={textBox1.Text}");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {

                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string[] strings = streamWriter.ReadToEnd().Split("%");
                    Id = int.Parse(strings[0]);
                    IsDone();
                    textBox2.Text = strings[1];
                    textBox3.Text = strings[2];
                    textBox4.Text = strings[3];
                    comboBox1.SelectedIndex = strings[4] == "0" ? 0 : 1;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    textBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    button2.Text = "Обновить";
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label6.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void AddClient()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Client/Add");
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new ClientDto { Name = textBox2.Text, Adress = textBox2.Text, Phone = textBox3.Text, Type = comboBox1.SelectedIndex == 0 ? 0 : 1 }));
            }
            WebResponse resp;
            try
            {

                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string[] strings = streamWriter.ReadToEnd().Split("%");
                    Id = int.Parse(strings[0]);
                    textBox2.Text = strings[1];
                    textBox3.Text = strings[2];
                    textBox4.Text = strings[3];
                    comboBox1.SelectedIndex = strings[4] == "0" ? 0 : 1;
                    textBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    button2.Text = "Обновить";
                    IsDone();
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label7.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void UpdateClient()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Client/Update?id={Id}&adress={textBox3.Text}&phone={textBox4.Text}");
            req.ContentType = "application/json";
            req.Method = "PUT";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {

                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label7.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Id == null)
                AddClient();
            else
                UpdateClient();
        }

        private (int, string, string, int, double)[] GetServices()
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
                    string[] services = streamWriter.ReadToEnd().Split('~');
                    var values = new (int, string, string, int, double)[services.Length];
                    for (int i = 0; i < services.Length; i++)
                    {
                        string[] value = services[i].Split('%');
                        values[i] = (int.Parse(value[0]),
                                     value[1],
                                     value[2],
                                     int.Parse(value[3]),
                                     double.Parse(value[4]));
                    }
                    return values;
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label7.Text = streamWriter.ReadToEnd();
                }
            }
            return null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label10.Text = Services.FirstOrDefault(t => t.Item2 == comboBox2.Text).Item3;
            IsDone();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/Add");
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new DealDto { ClientId = Id.Value, ServiceId = Services.FirstOrDefault(t => t.Item2 == comboBox2.Text).Item1, Description = textBox5.Text }));
            }
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string services = streamWriter.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label7.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            IsDone();
        }

        private void IsDone() 
        {
            button3.Enabled = !string.IsNullOrEmpty(textBox5.Text) && Id.HasValue && comboBox2.SelectedIndex != -1;
        }
    }
}
