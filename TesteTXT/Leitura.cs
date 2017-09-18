using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TesteTXT
{
    class Leitura
    {
        string data;        //Data
        string hora;        //Hora
        float temp1;        //T Entrada 1
        int cont1;          //Vazão 1
        float temp2;        //T Saida 1
        int cont2;          //Vazão 2
        float temp3;        //T Entrada 2
        int cont3;          //Energia Apoio 1
        float temp4;        //T Saida 2
        int cont4;          //Energia Apoio 2
        float term1;        //Energia Termosolar 1 = (T Saida 1)-(T Entrada 1)*(Vazão 1)*60/860
        float term2;        //Energia Termosolar 2 = (T Saida 2)-(T Entrada 2)*(Vazão 2)*60/860
        float termT;        //Energia Termosolar Total = (Energia Termosolar 1)+(Energia Termosolar 2)
        float apoioT;       //Energia Apoio Total = (Energia Apoio 1)+(Energia Apoio 2) 
        float energiaT;     //Energia Consumida Total = (Energia Termosolar Total)+(Energia Apoio Total)
        float par_solar;    //Parcela Solar = (Energia Termosolar Total)/(Energia Consumida TOtal)*100
        float par_apoio;    //Parcela Apoio = (Energia Apoio Total)/(Energia Consumida TOtal)*100
        float tarifa_apoio; //Tarifa Apoio = 0,65*(SOMA: Energia Apoio Total)
        float tarifa_solar; //Tarifa Termosolar = 0,45*(SOMA: Energia Termosolar Total)


        public Leitura()
        {
            //construtor padrão
        }
        public Leitura(string DATA, string HORA, float TEMP1, int CONT1, float TEMP2, int CONT2, float TEMP3, int CONT3,
                        float TEMP4, int CONT4)
        {
            this.data = DATA;
            this.hora = HORA;
            this.temp1 = TEMP1;
            this.cont1 = CONT1;
            this.temp2 = TEMP2;
            this.cont2 = CONT2;
            this.temp3 = TEMP3;
            this.cont3 = CONT3;
            this.temp4 = TEMP4;
            this.cont4 = CONT4;
        }

        public void SetData(string s)
        {
            this.data = s;
        }
        public void SetHora(string s)
        {
            this.hora = s;
        }
        public void SetTemp1(float s)
        {
            this.temp1 = s;
        }
        public void SetCont1(int s)
        {
            this.cont1 = s;
        }
        public void SetTemp2(float s)
        {
            this.temp2 = s;
        }
        public void SetCont2(int s)
        {
            this.cont2 = s;
        }
        public void SetTemp3(float s)
        {
            this.temp3 = s;
        }
        public void SetCont3(int s)
        {
            this.cont3 = s;
        }
        public void SetTemp4(float s)
        {
            this.temp4 = s;
        }
        public void SetCont4(int s)
        {
            this.cont4 = s;
        }
        public void SetTerm1()
        {
            term1 = (temp2 - temp1) * cont1 * 60 / 860;
        }
        public void SetTerm2()
        {
            term2 = (temp4-temp3)*cont2*60/860;
        }
        public void SetTermT()
        {
            termT = term1 + term2;
        }
        public void SetApoioT()
        {
            apoioT = cont3+cont4;
        }
        public void SetEnergiaT()
        {
            energiaT = termT + apoioT;
        }
        public void SetSolar()
        {
            par_solar = termT/energiaT*100;
        }
        public void SetApoio()
        {
            par_apoio = 100-par_solar;
        }
        public string GetData()
        {
            return this.data;
        }
        public string GetHora()
        {
            return this.hora;
        }
        public float GetTemp1()
        {
            return this.temp1;
        }
        public int GetCont1()
        {
            return this.cont1;
        }
        public float GetTemp2()
        {
            return this.temp2;
        }
        public int GetCont2()
        {
            return this.cont2;
        }
        public float GetTemp3()
        {
            return this.temp3;
        }
        public int GetCont3()
        {
            return this.cont3;
        }
        public float GetTemp4()
        {
            return this.temp4;
        }
        public int GetCont4()
        {
            return this.cont4;
        }
        public float GetTerm1()
        {
            return term1;
        }
        public float GetTerm2()
        {
            return term2;
        }
        public float GetTermT()
        {
            return termT;
        }
        public float GetApoioT()
        {
            return apoioT;
        }
        public float GetEnergiaT()
        {
            return energiaT;
        }
        public float GetSolar()
        {
            return par_solar;
        }
        public float GetApoio()
        {
            return par_apoio;
        }
    }

}
