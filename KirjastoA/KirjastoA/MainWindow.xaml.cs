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
            string hakuTeksti = HakuTextBox.Text;
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
            string hakutermi = HakuTextBox.Text.Trim();

            // 2. etsitään hakua vataavat teokset
            List<Teos> osumat = valittuKirjasto.Teokset.FindAll(
                x =>    x.TeoksenNimi.Contains(hakutermi) == true || 
                        x.TeoksenKuvaus.Contains(hakutermi) == true
                );

            // 3. teoslistan päivitys
            dataGrid.ItemsSource = osumat;
        }
        private void tyyppiBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void lajiBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tyhjääButton_Click(object sender, RoutedEventArgs e)
        {
            HakuTextBox.Text = "";
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

                //Tarkistetaan onko teokselle olemassa välilehteä
                TabItem välilehtoJoOlemassa = null;
                for(int i = 0; i < tabControl.Items.Count; i++)
                {
                    //käydään jokainen tabitem läpi
                    //+tyyppimuutos
                    TabItem t = (TabItem)tabControl.Items.GetItemAt(i);
                    string tHeader = (string)t.Header;
                    if(tHeader.Contains(klikattuTeos.TeoksenNimi) == true)
                    {
                        //Välilehti löytyi teokselle
                        välilehtoJoOlemassa = t;
                        break; //Keskeytetään silmukan suoritus
                    }
                }

                if(välilehtoJoOlemassa != null)
                {
                    //siirretään käyttäjä avatulle välilehdelle
                    tabControl.SelectedItem = välilehtoJoOlemassa;
                }
                else
                {              
                    //Luodaan olio meidän custom tab itemista
                    CustomTabItem t = new CustomTabItem();
                    // Luodaan olio tab item sisällöstä
                    CustomTabContentTeos tcont = new CustomTabContentTeos();

                    // Laitetaan tab itemin otsikkoon teksti
                    t.Header = klikattuTeos.TeoksenNimi;

                    // Tämä on olio siitä testidatasta, jota halutaan näyttää (esim. Teos/Asiakas)
                    TeoksenTiedot tl = new TeoksenTiedot();
                    // Asetetaan testiolioon joku näytettävä data
                    tl.TNimi = "Nimi: " + klikattuTeos.TeoksenNimi;
                    tl.TSivuMäärä = "Sivumäärä: " + klikattuTeos.TeoksenSivuMäärä.ToString();
                    tl.TLaji = "Laji: " + klikattuTeos.TeoksenLaji;
                    tl.TKuvaus = "Kuvaus: " + klikattuTeos.TeoksenKuvaus;
                    tl.TesittelyTeksti = "Esittely teksti: " + klikattuTeos.TeoksenEsittelyTeksti;

                    // Sijoitetaan TabItem sisällön DataContext-muuttujaan yllä luotu olio
                    // DataContext:iin sijoitettu olio on se, johon DataBinding-linkitykset tehdään
                    tcont.DataContext = tl;

                    // Asetetaan TabItem:n sisällöksi meidän CustomTabContent olio
                    t.Content = tcont;

                    // Lisätään lopuksi meidän custom tab item näkymään
                    tabControl.Items.Add(t);
                    tabControl.SelectedItem = t;
                }


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string käyttäjä = KäyttäjäTextBox.Text.Trim();
            string salasana = SalasanaTextBox.Text.Trim();
        }

        private void RekisteröidyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
