using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoReader
{
    public partial class Form1 : Form
    {
        //RichTextBox rtb2 = new RichTextBox();
        SerialPort serialPort1;//= new SerialPort("COM3");
        int temperature=0;
        int light=0;
        
        public Form1()
        {
            InitializeComponent();
            try
            {
               // Form1 f= new Form1();
                String[] ports = SerialPort.GetPortNames();
              
              foreach (String port in ports)
                {
                    comboBox1.Items.Add(port);
                  // MessageBox.Show(port);
                }
                
                //this.Controls.Add(rtb2);
               // serialPort1.BaudRate = 115200;
               // serialPort1.DataBits = 8;
               // serialPort1.Parity = Parity.None;
               // serialPort1.StopBits = StopBits.One;
               // serialPort1.ReadTimeout = 200;
               //serialPort1.Close();
               // serialPort1.Open();
                
               // serialPort1.DataReceived += serialPort1_DataReceived;
            }
            catch (Exception err)
            { MessageBox.Show(err.ToString()); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText("Sensor_Data_Obtained.txt", richTextBox1.Text);
            richTextBox1.Text="";
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            //rtb2.Text+="pelak\\n";
            int dataLength = 10;//serialPort1.BytesToRead;
           byte[] data = new byte[dataLength];
           try
           {
               int nbrDataRead = serialPort1.Read(data, 0, dataLength);
               string array = "";
               for (int i = 0; i < dataLength; i++)
               {
                   array += data[i].ToString() + "_";
               }
               if(data[7]!=0) light = data[7];
               if(data[8] != 0 ) temperature = data[8];
               //textBox1.Text = light.ToString();
               //textBox2.Text = temperature.ToString();
               //MessageBox.Show(array);
               //serialPort1.r
               var q = serialPort1.ReadExisting().ToString();

               SetText(array);
           }
           catch (Exception err)
           {
               //MessageBox.Show(err.ToString());
               //System.IO.File.WriteAllText("Sensor_Data_Obtained_ERROR.txt", richTextBox1.Text);
               //MessageBox.Show(err.ToString());
           }


        }

        private delegate void LineReceivedEvent(string POT);

        private void LineReceived(string POT)
        {
            //What to do with the received line here

           // richTextBox1.Text = POT+richTextBox1.Text;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
           
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                textBox1.Text = light.ToString();
                textBox2.Text = temperature.ToString();
                //MessageBox.Show(array);
                this.richTextBox1.Text = text+"\n"+richTextBox1.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedItem.ToString());
            serialPort1 = new SerialPort(comboBox1.SelectedItem.ToString());
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.ReadTimeout = 200;
            serialPort1.Close();
            serialPort1.Open();

            serialPort1.DataReceived += serialPort1_DataReceived;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}

