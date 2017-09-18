using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TesteTXT
{
    public partial class Form3 : Form
    {
        Form1 principal;
        public Form3(Form1 form)
        {
            InitializeComponent();
            principal = form;
            video = "http://infotrafego.pbh.gov.br/rlt/images/camara01.jpg";
            camera = LoadPicture(video);
            pictureBox1.Image = camera;
            textBox3.Text = "Belo Horizonte";
            textBox4.Text = "Minas Gerais";
            textBox5.Text = "Brasil";
            MapsEndereco(principal.coordenadas_gps);
        }


        Bitmap camera;
        string video;
        string rua, numero, bairro, cidade, estado, pais, endereco;
       
        
        
        
        private Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (HttpWebRequest)WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }

            return (bmp);
        }
        private void MapsEndereco(string endereco)
        {
            string link = "http://maps.google.com/maps?q=" + endereco;

            try
            {

                webBrowser1.Navigate(link);
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Localização indisponível para exibição no Mapa.");

            }
        }

       

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            rua = textBox1.Text;
            numero = maskedTextBox1.Text;
            bairro = textBox2.Text;
            cidade = textBox3.Text;
            estado = textBox4.Text;
            pais = textBox5.Text;
            endereco = rua + ", " + numero + " - " + bairro + ", " + cidade + " - " + estado + " , " + pais;
            MapsEndereco(endereco);
            camera = TesteTXT.Properties.Resources.nao_disponivel;
            pictureBox1.Image = camera;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            camera = LoadPicture("http://infotrafego.pbh.gov.br/rlt/images/camara01.jpg");
            pictureBox1.Image = camera;
        }
        
    }
}
