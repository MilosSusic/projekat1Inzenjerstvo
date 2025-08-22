using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr14_2022
{
    public class Film
    {
        public int ocjenaFilma;   //brojevnog tipa
        public string nazivFilma;  //tekstualno
        public DateTime datumDodavanja;  //datum
        private string slika;   //prikaz slike
        private string fajl; //referenca na *.rtf datoteku

        public Film()
        {
        }

        public Film(int ocjenaFilma, string nazivFilma, DateTime datumDodavanja, string slika, string fajl)
        {
            this.ocjenaFilma = ocjenaFilma;
            this.nazivFilma = nazivFilma;
            this.datumDodavanja = datumDodavanja;
            this.slika = slika;
            this.fajl = fajl;
        }

        public int OcjenaFilma
        {
            get { return ocjenaFilma; }
            set { ocjenaFilma = value; }

        }

        public string NazivFilma
        {
            get { return nazivFilma; }
            set { nazivFilma = value; }
        }

        public DateTime DatumDodavanja
        {
            get { return datumDodavanja; }
            set { datumDodavanja = value; }
        }

        public string Slika
        {
            get { return slika; }
            set { slika = value; }
        }

        public string Fajl
        {
            get { return fajl; }
            set { fajl = value; }
        }
    }
}

