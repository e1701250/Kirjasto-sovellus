using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirjastoA
{
    class Teos
    {
        public int _Id = 0;
        public string _Nimi = "";
        public string _Kuvaus = "";

        //Property, joka palauttaa Id-numeron muuttujasta _Id
        //Käytetään listview näkymän bindingissä
        public int IdNumero
        {
            get { return _Id; }
        }

        public string TeoksenNimi
        {
            get { return _Nimi; }
        }

        public string TeoksenKuvaus
        {
            get { return _Kuvaus; }
        }

        public Teos(int id, string nimi, string kuvaus)
        {
            _Id = id;
            _Nimi = nimi;
            _Kuvaus = kuvaus;
        }
    }
}

