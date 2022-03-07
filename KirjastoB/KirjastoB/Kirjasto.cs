using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirjastoB
{
    class Kirjasto
    {
        public string Nimi = "";
        public string Sijainti = "";

        /// <summary>
        /// Tässä muuttujassa ylläpidetään kirjaston kokoelmaa.
        /// </summary>
        public List<Teos> Teokset = new List<Teos>();

        public Kirjasto(string kirjNimi, string kirjSijainti)
        {
            Nimi = kirjNimi;
            Sijainti = kirjSijainti;
        }

        /// <summary>
        /// Tällä funktiolla voidaan luoda X kpl teoksia kirjaston kokoelmaan.
        /// </summary>
        /// <param name="lkm"></param>
        public void LuoMockDataa(int lkm)
        {
            Teokset.Clear(); // Tyhjätään lista

            for(int i = 0; i < lkm; i++)
            {
                // Luodaan joka silmukan suorituskerralla yksi Teos
                Teos t = new Teos(
                    i, 
                    "Teos " + i, 
                    "Hiihuu " + i
                    );

                Teokset.Add(t); // Lisätään luotu Teos listaan
            }
        }
    }
}
