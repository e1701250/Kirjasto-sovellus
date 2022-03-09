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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Kirjasto valittuKirjasto;
        public MainWindow()
        {
            valittuKirjasto = new Kirjasto(
                "Palosaaren kirjasto",
                "Pikitehtaankatu 19, 65200 Vaasa"
                );

            valittuKirjasto.LuoMockDataa(30);

            InitializeComponent();

            //Kerrotaan ListView-komponentille, että sen pitää näyttää
            //valittuKirjasto.Teokset listaa sen sisällä
            //ListView.ItemSource kertoo itemeiden "lähteen"
            //Jokainen item näytetään rivinä ListView:n sisällä

            dataGrid.ItemsSource = valittuKirjasto.Teokset;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string hakuTeksti = textBox.Text;
            bool onTyhjä = string.IsNullOrWhiteSpace(hakuTeksti);

            if(onTyhjä == false)
            {
                //Tekstikenttään on syötetty jotain
                //Painikkeet toimivat
                haeButton.IsEnabled = true;
                tyhjääButton.IsEnabled = true;
            }
            else
            {
                //Muuten painikkeet eivät toimi
                haeButton.IsEnabled = false;
                tyhjääButton.IsEnabled = false;
            }
        }

        private void haeButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. hakutermi talteen
            string hakutermi = textBox.Text.Trim();

            // 2. etsitään hakua vataavat teokset
            List<Teos> osumat = valittuKirjasto.Teokset.FindAll(
                x =>    x.TeoksenNimi.Contains(hakutermi) == true || 
                        x.TeoksenKuvaus.Contains(hakutermi) == true
                );

            // 3. teoslistan päivitys
            dataGrid.ItemsSource = osumat;
        }

        private void tyhjääButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            //TODO tyhjää myös muut suodattimet

            //Palautetaan alkuperäinen lista teoksista
            dataGrid.ItemsSource = valittuKirjasto.Teokset;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dataGrid.SelectedItem != null)
            {
                // 2. Jos on, avataan uusi välilehti
                //Pitää tehdä tyyppimuunnos (type cast), koska SelectedItem on yleinen ominaisuus
                //joka palauttaa "object"-tyyppisen olion
                Teos klikattuTeos = (Teos)dataGrid.SelectedItem;

            }
        }
    }
}
