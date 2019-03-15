using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace ser
{
    public partial class Form1 : Form
    {
        public SerialPort _serialPort { get; private set; }

        public Form1()
        {
            InitializeComponent();
            getport();
        }
        void getport() {
            String[] po = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(po);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    textBox2.Text = "SELECT PORT AND BAUD RATE";

                }
                else {
                    _serialPort = new SerialPort();
                    _serialPort.PortName = comboBox1.Text;
                    _serialPort.BaudRate = Convert.ToInt32(comboBox2.Text);
                    _serialPort.Open();
                    progressBar1.Value = 100;
                    //dynamic stuff = JObject.Parse("{ 'charge': '987', 'discharge': '654' }");
                    //string jinp = "discharge";
                    //string ch = (string)stuff[jinp];
                    //string dch = stuff.Address.City;
                    //string dch = stuff.charge;
                    //Console.WriteLine(ch);
                    //Console.WriteLine(dch);

                }
            }
            catch(UnauthorizedAccessException) {
                textBox2.Text = "ACCESS NOT PERMITTED";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _serialPort.Close();
            progressBar1.Value = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox3.Text == "")
            {
                textBox2.Text = "PROVIDE JSON CONTENT";
            }
            else
            {
                try
                {
                    //string text1 = File.ReadAllText("E:\\inp.txt");
                    //textBox1.Text = text1;
                    dynamic stuff = JObject.Parse(textBox3.Text);
                    string jinp = textBox1.Text;
                    Console.WriteLine(stuff[jinp]);
                    string ch = (string)stuff[jinp];
                    _serialPort.WriteLine(ch);
                    textBox1.Text = "";
                    textBox2.Text = _serialPort.ReadLine();
                    string wri = textBox2.Text;
                    File.WriteAllText("E:\\inp.txt", wri);

                }
                
                catch (Exception a) {
                    Console.WriteLine(a);
                    textBox2.Text = "please provide correct data";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebClient cli = new WebClient();
            string download = cli.DownloadString(textBox4.Text);
            File.WriteAllText("E:\\ip.json",download);
            string text = File.ReadAllText("E:\\ip.json");
            textBox3.Text = text;
        }
    }
}
