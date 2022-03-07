using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirjastoA
{
    class Kirjasto
    {
        public string _Nimi = "";
        public string _Sijainti = "";

        public List<Teos> Teokset = new List<Teos>();

        public Kirjasto(string nimi, string sijainti)
        {
            _Nimi = nimi;
            _Sijainti = sijainti;
        }

        /// <summary>
        /// Luodaan x määrä teoksia kirjastoon
        /// </summary>
        /// <param name="lkm">montako luodaan</param>
        public void LuoMockDataa(int lkm)
        {
            Teokset.Clear(); //Tyhjentää listan

            for(int i = 0; i < lkm; i++)
            {
                Teos t = new Teos(i,
                    "Teos " + i,
                    "Hiihuu " + i);

                Teokset.Add(t); //Lisätään teos listaan

            }
        }
    }
}
