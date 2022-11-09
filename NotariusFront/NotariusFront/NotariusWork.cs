using System.Net;

namespace NotariusFront
{
    public partial class NotariusWork : UserControl
    {
        (int, string)[] Open { get; set; }

        int? Id { get; set; }

        public NotariusWork()
        {
            InitializeComponent();
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/Get");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            resp = req.GetResponse();
            string s;
            using (var streamWriter = new StreamReader(resp.GetResponseStream()))
            {
                s = streamWriter.ReadToEnd();
            }
            if (s == "null")
            {
                comboBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                numericUpDown1.Visible = false;
                Select();
            }
            else
            {
                comboBox1.Visible = false;
                button1.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                numericUpDown1.Visible = true;
                Work();
            }
        }

        private void Select()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/GetOpen");
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
            catch
            {
            }
        }

        private void Work()
        {
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/Get");
            req.ContentType = "application/json";
            req.Method = "GET";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string[] s = streamWriter.ReadToEnd().Split('%');
                    Open = new (int, string)[s.Length];
                    Id = int.Parse(s[0]);
                    label1.Text = s[1];
                    label2.Text = s[4].Split('~')[1];
                    string[] strings = s[3].Split('~');
                    label3.Text = strings[1];
                    label4.Text = strings[3];
                    label5.Text = strings[4] == "0" ? "Физическое лицо" : "Юридическое лицо";
                }
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Open.FirstOrDefault(t => t.Item2 == comboBox1.Text).Item1;
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/ToInProgress?id={id}");
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
                    comboBox1.Visible = false;
                    button1.Visible = false;
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    numericUpDown1.Visible = true;
                    Work();
                }
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Open.FirstOrDefault(t => t.Item2 == comboBox1.Text).Item1;
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/ToDone?id={Id}&transactionAmount={numericUpDown1.Value}");
            req.ContentType = "application/json";
            req.Method = "PUT";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                comboBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                numericUpDown1.Visible = false;
                Select();
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Open.FirstOrDefault(t => t.Item2 == comboBox1.Text).Item1;
            WebRequest req = WebRequest.CreateHttp($"https://localhost:7086/Deal/ToCanceld?id={Id}");
            req.ContentType = "application/json";
            req.Method = "PUT";
            req.Headers.Add("Authorization:Bearer " + Form1.Token);
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                comboBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                numericUpDown1.Visible = false;
                Select();
            }
            catch
            {
            }
        }
    }
}
