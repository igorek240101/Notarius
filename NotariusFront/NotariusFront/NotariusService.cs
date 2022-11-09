using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotariusFront
{
    public partial class NotariusService : UserControl
    {
        (int, string, string, int, double)[] Services;
        int? Id { get; set; }
        public NotariusService()
        {
            InitializeComponent();
            ReList();
            radioButton2_CheckedChanged(this, EventArgs.Empty);
        }

        private void ReList()
        {
            Services = GetAll();
            if (Services.Length == 0)
            {
                radioButton1.Checked = true;
                radioButton2.Enabled = false;
            }
            else
            {
                radioButton2.Enabled = true;
                comboBox1.Items.Clear();
                foreach(var value in Services)
                {
                    comboBox1.Items.Add(value.Item2);
                }
                comboBox1.SelectedIndex = 0;
                comboBox1_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = radioButton2.Checked;
            textBox1.Enabled = !radioButton2.Checked;
            button2.Enabled = radioButton2.Checked;
            button1.Text = radioButton2.Checked ? "Обновить" : "Добавить";
            if(radioButton1.Checked) textBox1_TextChanged(this, EventArgs.Empty);
            if (radioButton2.Checked)
            {
                comboBox1.SelectedIndex = 0;
                comboBox1_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private (int, string, string, int, double)[] GetAll()
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
                    var values = new List<(int, string, string, int, double)>();
                    for(int i = 0; i < services.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(services[i]))
                        {
                            string[] value = services[i].Split('%');
                            values.Add( (int.Parse(value[0]),
                                         value[1],
                                         value[2],
                                         int.Parse(value[3]),
                                         double.Parse(value[4])));
                        }
                    }
                    return values.ToArray();
                }
            }
            catch
            {
            }
            return null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked && Services.ToList().ConvertAll(t => t.Item2).Contains(textBox1.Text))
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) Add();
            else Update();
        }

        private void Add()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Service/Add");
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new ServiceDto { Name = textBox1.Text, Description = textBox2.Text, Price = (int)numericUpDown1.Value, Commission = (double)numericUpDown2.Value }));
            }
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string s = streamWriter.ReadToEnd();
                    ReList();
                    textBox1_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label5.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void Update()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Service/Update?id={Id}&price={(int)numericUpDown1.Value}&commission={numericUpDown2.Value}&description={textBox2.Text}");
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
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label5.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Service/Delete?id={Id}");
            req.ContentType = "application/json";
            req.Method = "DELETE";
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
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label5.Text = streamWriter.ReadToEnd();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = Services.FirstOrDefault(t => t.Item2 == comboBox1.SelectedItem);
            Id = value.Item1;
            textBox1.Text = value.Item2;
            textBox2.Text = value.Item3;
            numericUpDown1.Value = value.Item4;
            numericUpDown2.Value = (decimal)value.Item5;
        }
    }
}
