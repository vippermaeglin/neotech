using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TesteTXT
{
    public partial class Form4 : Form
    {
        Form1 principal;
        bool starting = true;
        bool LogEnable = false;
        string file_path;
        int count_file=2;
        int baudrate = 9600;
        int databits = 8;
        string parity = "None";
        string porta = "COM1";
        string stopbits = "1";
        string flow = "None";
        public Form4(Form1 form)
        {
            InitializeComponent();
            principal = form;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            comboBox1.SelectedIndex = 5;
            comboBox2.SelectedIndex = 3;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            starting = false;
        }
        string RxString;
        private void button1_Click(object sender, EventArgs e)
        {
          ConfiguraSerial();  
          serialPort1.Open();
          if (serialPort1.IsOpen)
          {
              button1.Enabled = false;
              button2.Enabled = true;
              richTextBox1.Clear();
              richTextBox2.Clear();
              richTextBox1.Enabled = true;
              richTextBox2.Enabled = true;
              richTextBox2.ReadOnly = false;
          }
  
        }
        
        private void DisplayText(object sender, EventArgs e)
        {
            richTextBox1.AppendText(RxString);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
           //principal.LogSerial += RxString;
           if (richTextBox1.TextLength>=(richTextBox1.MaxLength-50))
           {
               richTextBox1.Clear();
               richTextBox1.AppendText("\n**************** LIMPEZA DE TELA - O BUFFER ESTÁ CHEIO **************** \n");
               richTextBox2.Clear();
               if (LogEnable)
               {
                   EncerrarLog();
                   MessageBox.Show("O ARQUIVO DO LOG NÃO PODE SER SUPERIOR A 2GB!");
               }

           }
        }
        private void ConfiguraSerial()
        {
            serialPort1.PortName = porta;
            serialPort1.BaudRate = baudrate;
            serialPort1.DataBits = databits;
            switch(parity)
            {
                case "Even":
                    serialPort1.Parity = System.IO.Ports.Parity.Even;
                    break;
                case "Odd":
                    serialPort1.Parity = System.IO.Ports.Parity.Odd;
                    break;
                case "Mark":
                    serialPort1.Parity = System.IO.Ports.Parity.Mark;
                    break;
                case "Space":
                    serialPort1.Parity = System.IO.Ports.Parity.Space;
                    break;
                default:
                    serialPort1.Parity = System.IO.Ports.Parity.None;
                    break;
            }
            switch (stopbits)
            {
                case "1":
                    serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    break;
                case "1.5":
                    serialPort1.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    break;
                case "2":
                    serialPort1.StopBits = System.IO.Ports.StopBits.Two;
                    break;
                default:
                    serialPort1.StopBits = System.IO.Ports.StopBits.None;
                    break;
            }
            switch (flow)
            {
                case "RequestToSend":
                    serialPort1.Handshake = System.IO.Ports.Handshake.RequestToSend;
                    break;
                case "XOnXOff":
                    serialPort1.Handshake = System.IO.Ports.Handshake.XOnXOff;
                    break;
                case "RequestToSendXOnXOff":
                    serialPort1.Handshake = System.IO.Ports.Handshake.RequestToSendXOnXOff;
                    break;
                default:
                    serialPort1.Handshake = System.IO.Ports.Handshake.None;
                    break;
            }
        }
                
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            if (LogEnable) EncerrarLog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
                richTextBox1.ReadOnly = true;
                richTextBox2.ReadOnly = true;
                richTextBox1.Enabled = false;
                richTextBox2.Enabled = false;
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            RxString = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
            
       
        }


        private void button3_Click(object sender, EventArgs e)
        {
            LogEnable = true;
            button3.Enabled = false;
            button4.Enabled = true;
            richTextBox1.Clear();
            richTextBox2.Clear();
            SalvarLog();
            
        }
        private void SalvarLog()
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file_path = saveFileDialog1.FileName;
                System.IO.StreamWriter sr = new System.IO.StreamWriter(file_path);
            }
        }
        private void EncerrarLog()
        {
            richTextBox1.SaveFile(file_path, RichTextBoxStreamType.PlainText);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LogEnable = false;
            button3.Enabled = true;
            button4.Enabled = false;
            EncerrarLog();
        }


        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Enabled = true;
            checkBox2.Checked = false;
            checkBox1.Enabled = false;
            porta = "COM1";
            MessageBox.Show("COM1 selecionada, reinicie a conexão!");
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            checkBox2.Enabled = false;
            porta = "COM2";
            MessageBox.Show("COM2 selecionada, reinicie a conexão!");
        }

        

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the port is closed, don't try to send a character.
            if (!serialPort1.IsOpen) return;

            // If the port is Open, declare a char[] array with one element.

            char[] buff = new char[1];

            // Load element 0 with the key character.
            buff[0] = e.KeyChar;

            // Send the one character buffer.
            serialPort1.Write(buff, 0, 1);

            // Set the KeyPress event as handled so the character won't
            // display locally. If you want it to display, omit the next line.

            //e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baudrate = System.Convert.ToInt32(comboBox1.Text);
            if(!starting)MessageBox.Show("Velocidade alterada, reinicie a conexão!");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            databits = System.Convert.ToInt32(comboBox2.Text);
            if(!starting) MessageBox.Show("Bit de Dados alterado, reinicie a conexão!");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            parity = comboBox3.Text;
            if(!starting) MessageBox.Show("Paridade alterada, reinicie a conexão!");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopbits = comboBox4.Text;
            if(!starting) MessageBox.Show("Bit de Parada alterado, reinicie a conexão!");
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            flow = comboBox5.Text;
            if(!starting) MessageBox.Show("Controle de Fluxo alterado, reinicie a conexão!");
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }


     }
}
