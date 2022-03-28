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
        public string _Tekijä = "";
        public string _Laji = "";
        public int _sivuMäärä = 0;
        public string _esittelyTeksti = "";



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

        public string TeoksenTekijä
        {
            get { return _Tekijä; }
        }

        public string TeoksenLaji
        {
            get { return _Laji; }
        }

        public int TeoksenSivuMäärä
        {
            get { return _sivuMäärä; }
        }

        public string TeoksenEsittelyTeksti
        {
            get { return _esittelyTeksti; }

        }

        public Teos(int id, string nimi, string tekijä, string laji, int sivuMäärä, string esittelyTeksti)
        {
            _Id = id;
            _Nimi = nimi;
            _Tekijä = tekijä;
            _Laji = laji;
            _sivuMäärä = sivuMäärä;
            _esittelyTeksti = esittelyTeksti;


        }
    }
}

