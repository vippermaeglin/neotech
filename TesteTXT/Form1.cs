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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public string arquivo_lido;
        public string coordenadas_gps = "-19.91209,-43.941386";
        public char delimitador = '\t';
        public float tarifa_solar = 00.45F;
        public float tarifa_apoio = 00.65F;
        public string LogSerial;
        int numero_leituras = 0;
        string[] arquivo_lista;
        Leitura[] leituras=new Leitura[100];
       /* Bitmap camera = LoadPicture("http://infotrafego.pbh.gov.br/rlt/images/camara01.jpg");
        pictureBox1.Image = camera;
        textBox3.Text = "Belo Horizonte";
        textBox4.Text = "Minas Gerais";
        textBox5.Text = "Brasil";    
        */
        
       
        private void Exibe_Leituras(Leitura leia)
        {
            MessageBox.Show("DATA: " + leia.GetData());
            MessageBox.Show("HORA: " + leia.GetHora());
            MessageBox.Show("TEMPERATURA 1: " + leia.GetTemp1());
            MessageBox.Show("CONTADOR 1: " + leia.GetCont1());
            MessageBox.Show("TEMPERATURA 2: " + leia.GetTemp2());
            MessageBox.Show("CONTADOR 2: " + leia.GetCont2());
            MessageBox.Show("TEMPERATURA 3: " + leia.GetTemp3());
            MessageBox.Show("CONTADOR 3: " + leia.GetCont3());
            MessageBox.Show("TEMPERATURA 4: " + leia.GetTemp4());
            MessageBox.Show("CONTADOR 4: " + leia.GetCont4());
        }
                

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);
                arquivo_lido = sr.ReadToEnd();
                arquivo_lista = arquivo_lido.Split(delimitador);
                int i = -1;
                int j = 0;
       
               
            
                 foreach (string s in arquivo_lista)
                 {
                     if (i == -1) coordenadas_gps = s;
                     if (i == 10) //Separa leituras
                     {
                         i = 0;
                         j++;
                         numero_leituras++;
                     }
                     if (i == 0) //Data
                     {
                         leituras[j] = new Leitura();
                         leituras[j].SetData(s.Replace("\n",""));
                     }
                     if (i == 1) //Hora
                     {
                         leituras[j].SetHora(s);
                     }
                     if (i == 2) //Temperatura Entrada 1
                     {
                         leituras[j].SetTemp1((float)System.Convert.ToDouble(s)/10);
                     }
                     if (i == 3) //Vazão 1
                     {
                         leituras[j].SetCont1(System.Convert.ToInt32(s));
                     }
                     if (i == 4) //Temperatura Saida 1
                     {
                         leituras[j].SetTemp2((float)System.Convert.ToDouble(s)/10);
                     }
                     if (i == 5) //Vazão 2
                     {
                         leituras[j].SetCont2((int)System.Convert.ToDouble(s));
                     }
                     if (i == 6) //Temperatura Entrada 2
                     {
                         leituras[j].SetTemp3((float)System.Convert.ToDouble(s)/10);
                     }
                     if (i == 7) //Energia Apoio 1
                     {
                         leituras[j].SetCont3(System.Convert.ToInt32(s));
                     }
                     if (i == 8) //Temperatura Saida 2
                     {
                         leituras[j].SetTemp4((float)System.Convert.ToDouble(s)/10);
                     }
                     if (i == 9) //Energia Apoio 2
                     {
                         leituras[j].SetCont4(System.Convert.ToInt32(s));
                     }
                     if (i >= 0)
                     {
                         leituras[j].SetTerm1();    //Energia Termosolar 1
                         leituras[j].SetTerm2();    //Energia Termosolar 2
                         leituras[j].SetTermT();    //Energia Termosolar Total
                         leituras[j].SetApoioT();   //Energia Apoio Total
                         leituras[j].SetEnergiaT(); //Energia Consumida Total
                         leituras[j].SetSolar();    //Parcela Solar
                         leituras[j].SetApoio();    //Parcela Apoio
                     }
                     i++;


                 }
                object[] leia_aux = new object[17];
                //Exibição no GridView
                for (i = 0; i < numero_leituras; i++)
                {
                leia_aux[0] = leituras[i].GetData();        // Data
                leia_aux[1] = leituras[i].GetHora();        // Hora
                leia_aux[2] = leituras[i].GetTemp1();       // T Entrada 1
                leia_aux[3] = leituras[i].GetTemp2();       // T Saida 1
                leia_aux[4] = leituras[i].GetCont1();       // Vazão 1
                leia_aux[5] = leituras[i].GetTemp3();       // T Entrada 2
                leia_aux[6] = leituras[i].GetTemp4();       // T Saida 2
                leia_aux[7] = leituras[i].GetCont2();       // Vazão 2
                leia_aux[8] = leituras[i].GetTerm1();       // Energia Termosolar 1
                leia_aux[9] = leituras[i].GetTerm2();       // Energia Termosolar 2
                leia_aux[10] = leituras[i].GetTermT();      // Energia Termosolar Total                            
                leia_aux[11] = leituras[i].GetCont3();      // Energia Apoio 1
                leia_aux[12] = leituras[i].GetCont4();      // Energia Apoio 2
                leia_aux[13] = leituras[i].GetApoioT();     // Energia Apoio Total
                leia_aux[14] = leituras[i].GetEnergiaT();   // Energia Consumida
                leia_aux[15] = leituras[i].GetSolar();      // Parcela Solar
                leia_aux[16] = leituras[i].GetApoio();      // Parcela Apoio
                dataGridView1.Rows.Add(leia_aux);
                }
                for (i = 0; i < numero_leituras; i++)
                {
                    chart1.Series["T Entrada 1"].Points.AddXY(leituras[i].GetHora(), leituras[i].GetTemp1());
                    chart1.Series["T Saida 1"].Points.AddXY(leituras[i].GetHora(), leituras[i].GetTemp2());
                    chart1.Series["T Entrada 2"].Points.AddXY(leituras[i].GetHora(), leituras[i].GetTemp3());
                    chart1.Series["T Saida 2"].Points.AddXY(leituras[i].GetHora(), leituras[i].GetTemp4());
                }
                
            }


        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Configuracoes = new Form2(this);
            Configuracoes.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gráficoAcumuladoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void relatórioDeTarifasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this);
            form.Show();
        }

        private void comunicaçãoSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4(this);
            form.Show();
        }

        private void sMSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form4 form = new Form4(this);
            form.Show();
        }

       

    }
}
