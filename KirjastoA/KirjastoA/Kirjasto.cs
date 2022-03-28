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
    }
}
