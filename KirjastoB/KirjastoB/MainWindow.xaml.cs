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

namespace KirjastoB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Määritellään kirjasto, jossa asioidaan
        private Kirjasto valittuKirjasto;

        public MainWindow()
        {
            // Alustetaan kirjasto kun sovellus käynnistyy
            valittuKirjasto = new Kirjasto("Palosaaren kirjasto",
                "Pikitehtaankatu 19-23, 65200 Vaasa");

            // Luodaan mock teoksia
            valittuKirjasto.LuoMockDataa(20);

            InitializeComponent();


            // ListView.ItemsSource kertoo, mistä listassa näytettävät
            // itemit tulevat
            hakuTuloksetLista.ItemsSource = valittuKirjasto.Teokset;
        }
    }
}
