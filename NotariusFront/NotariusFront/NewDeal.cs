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

        private int? Id { get; set; }

        public NewDeal()
        {
            InitializeComponent();
            radioButton1_CheckedChanged(this, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Id = null;
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Id = null;
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Client/Get?name={textBox1.Text}");
            req.ContentType = "application/json";
            req.Method = "GET";
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new LoginDto { Name = textBox1.Text, Password = textBox2.Text }));
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
                    textBox4.Enabled = false;
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
                    textBox4.Enabled = false;
                    button2.Text = "Обновить";
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
    }
}
