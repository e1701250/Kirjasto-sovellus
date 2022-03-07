using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirjastoB
{
    class Teos
    {
        // Muuttuja nimeltä _Id
        public int _Id = 0;

        // Property = luokan attribuutti
        public int Id
        {
            // Palauttaa Id-arvon, jotta voidaan esim. näyttää ListView:ssä
            get { return _Id; }
        }



        public string _Nimi = "";

        public string Nimi
        {
            // Palauttaa Nimi-arvon, jotta voidaan esim. näyttää ListView:ssä
            get { return _Nimi; }
        }




        public string _Kuvaus = "";

        public string Kuvaus
        {
            // Palauttaa Kuvaus-arvon, jotta voidaan esim. näyttää ListView:ssä
            get { return _Kuvaus; }
        }

        public Teos(int idNumero, string teoksenNimi, string teoksenKuvaus)
        {
            _Id = idNumero;
            _Nimi = teoksenNimi;
            _Kuvaus = teoksenKuvaus;
        }
    }
}
