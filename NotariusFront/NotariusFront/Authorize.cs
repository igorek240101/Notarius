using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NotariusFront
{
    public partial class Authorize : UserControl
    {
        public Authorize()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp("https://localhost:7086/User/Login");
            req.ContentType = "application/json";
            req.Method = "POST";
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new LoginDto{ Name = textBox1.Text, Password = textBox2.Text }));
            }
            WebResponse resp;
            try
            {

                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string[] strings = streamWriter.ReadToEnd().Split(" ");
                    Form1.Token = strings[1];
                    Visible = false;
                    switch(strings[0])
                    {
                        case "0":
                            {
                                Form1.That.Controls.Clear();
                                Admin admin = new Admin();
                                Form1.That.Controls.Add(admin);
                            }
                            break;
                        case "1":
                            {
                                Form1.That.Controls.Clear();
                                Financer financer = new Financer();
                                Form1.That.Controls.Add(financer);
                            }
                            break;
                        case "2":
                            {
                                Form1.That.Controls.Clear();
                                Notarius notarius = new Notarius();
                                Form1.That.Controls.Add(notarius);
                            }
                            break;
                    }
                }
            }
            catch (WebException ex)
            {
                var value = ex.Response;
                using (var streamWriter = new StreamReader(value.GetResponseStream()))
                {
                    label3.Text = streamWriter.ReadToEnd();
                }
            }
        }
    }
}
