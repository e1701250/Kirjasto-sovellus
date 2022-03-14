using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KirjastoA
{
    /// <summary>
    /// Määritellään oma TabItem, joka periytyy sisäänrakennetusta TabItem luokasta.
    /// Näin ollen se on TabItem, niinkuin normaalit välilehdet, mutta se sisältää meidän
    /// räätälöimää toiminnallisuutta.
    /// </summary>
    public partial class CustomTabItem : TabItem
    {
        public CustomTabItem()
        {
            // Tämä alustaa meidän CustomTabItem:n käyttöliittymäelementit luonnin yhteydessä
            InitializeComponent();
        }

    }
}
