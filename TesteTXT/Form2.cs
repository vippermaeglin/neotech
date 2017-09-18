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
    public partial class Form2 : Form
    {
        Form1 principal;
        string aux;
        public Form2(Form1 form)
        {
            InitializeComponent();
            principal = form;
            maskedTextBox3.Text = principal.delimitador.ToString();
            maskedTextBox1.Text = "0"+principal.tarifa_solar.ToString();
            maskedTextBox2.Text ="0"+principal.tarifa_apoio.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            principal.delimitador = System.Convert.ToChar(maskedTextBox3.Text);
            MessageBox.Show("Novo delimitador: " + principal.delimitador);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            aux = maskedTextBox1.Text.Remove(5);
            principal.tarifa_solar = (float)System.Convert.ToDouble(aux)/100;
            MessageBox.Show("Novo valor para a tarifa: "+principal.tarifa_solar.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            aux = maskedTextBox2.Text.Remove(5);
            principal.tarifa_apoio = (float)System.Convert.ToDouble(aux) / 100;
            MessageBox.Show("Novo valor para a tarifa: " + principal.tarifa_apoio.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            principal.delimitador = '\t';
            principal.tarifa_solar = 00.45F;
            principal.tarifa_apoio = 00.65F;
            maskedTextBox3.Text = principal.delimitador.ToString();
            maskedTextBox1.Text = "0" + principal.tarifa_solar.ToString();
            maskedTextBox2.Text = "0" + principal.tarifa_apoio.ToString();
            MessageBox.Show("Valores Recuperados!");
        }
        //delimitador = 
    }
}
