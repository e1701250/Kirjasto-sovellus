using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
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

        private string kirjautunutUsername = "";
        public MainWindow()
        {
            valittuKirjasto = new Kirjasto(
                "Palosaaren kirjasto",
                "Pikitehtaankatu 19, 65200 Vaasa"
                );

            valittuKirjasto.LuoMockDataa(30);

            InitializeComponent();

            
        }

        private void avaaKirjButtonClick(object sender, RoutedEventArgs e)
        {
            // Luodaan instanssi meidän omasta dialogista
            OmaDialogi d = new OmaDialogi();

            // ShowDialog funktio näyttää sen dialogina
            // Paluuarvo false = käyttäjä ei suorittanut dialogia loppuun
            // Paluuarvo true = käyttäjä suoritti dialogin loppuun
            // (eli pääsi klikkaamaan Kirjaudu-painiketta)
            bool tulos = (bool)d.ShowDialog();
            if (tulos == false)
            {
                // Käyttäjä klikkasi "Peruuta" tai sulki ikkunan
                //MessageBox.Show("Ei kirjautunut.");
            }
            else
            {
                // Käyttäjä klikkasi "Kirjaudu" painiketta
                // OmaDialogi-luokassa sen painikkeen tapahtumakäsittelijässä asetetaan DialogResult = true
                // Siksi päädymme tähän haaraan

                // Luetaan dialogi-ikkunasta käyttäjän syöttämä username ja salasana
                string useremail = d.EnteredUsername;
                string pw = d.EnteredPassword;

                // Muuta tämä vastaamaan sinun omaa polkua
                string tietokantaPolku = "U:/Ohjelmoinnin perusteet 2/Käyttäjät.accdb";

                // Tällä komennolla valmistellaan yhteyden luomista tietokantaan
                // HUOM! Jos Visual Studio valittaa, ettei löydä OleDbConnection, klikkaa
                // luokan nimeä hiiren oikealla, valitse "Quick actions..." ja valitse
                // "using System.Data.OleDb".
                OleDbConnection myConn = new OleDbConnection(
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    @"Data Source=" + tietokantaPolku + ";"
                    );

                try
                {
                    // Avataan yhteys
                    myConn.Open();

                    // Luodaan SQL-komento, jolla luetaan kaikki tieto Users-nimisestä taulukosta
                    OleDbCommand myQuery = new OleDbCommand("SELECT * FROM Users WHERE UserEmail = '" + (useremail) + "';", myConn);

                    // Komento suoritetaan tällä koodirivillä
                    OleDbDataReader myReader = myQuery.ExecuteReader();

                    // Tarkistetaan, jos saimme tietokannasta rivejä
                    if (myReader.HasRows)
                    {
                        // Suoritetaan while-silmukka, niin kauan kun on enemmän prosessoitavaa dataa
                        // .Read() funktio palauttaa true, jos on lisää rivejä jotka pitää käsitellä
                        while (myReader.Read() == true)
                        {
                            // Luetaan tiedot paluudatasta
                            // Pitää lukea oikea tietotyyppi (esim int/string)
                            // Kentät luetaan siinä järjestyksessä, kuin ne ovat tietokannan taulussa
                            // määriteltyjä (minulla on id-numero ensin -> järjestysnumero 0)
                            // Sitten minulla on kaksi merkkijonoa, ensimmäinen on username
                            
                            //int userId = myReader.GetInt32(0);
                            //string userName = myReader.GetString(1);
                            string userEmail = myReader.GetString(2);
                            string userPassword = myReader.GetString(3);

                            // Muodostan merkkijonon luetusta datasta ja lisään list-boxiin
                            //string luettuTieto = userId + ": " + userName + ", " + userEmail;
                            //luetutTiedot.Items.Add(luettuTieto);
                            if (useremail.CompareTo(userEmail) == 0 &&
                            pw.CompareTo(userPassword) == 0)
                            {
                                MessageBox.Show("Kirjautunut käyttäjä: " + useremail);

                                // Tallennetaan kirjautuneen käyttäjän username muuttujaan, tilatietona
                                kirjautunutUsername = useremail;

                                // Päivitetään käyttöliittymä, koska käyttäjä on kirjautunut
                                PäivitäKäyttöliittymä();
                            }
                            else
                            {
                                MessageBox.Show("Väärä tunnus tai salasana");
                            }
                        }
                    }

                    // Suljetaan tietokantayhteys
                    myConn.Close();
                }
                catch (InvalidOperationException ioexc)
                {
                    // Käsitellään mahdollisia poikkeustilanteita
                    Trace.WriteLine(ioexc.Message);
                    Trace.WriteLine(ioexc.StackTrace);
                }
                catch (OleDbException oledbexc)
                {
                    // Käsitellään mahdollisia poikkeustilanteita
                    Trace.WriteLine(oledbexc.Message);
                    Trace.WriteLine(oledbexc.StackTrace);
                }
                catch (Exception exc)
                {
                    // Käsitellään mahdollisia poikkeustilanteita
                    Trace.WriteLine(exc.Message);
                    Trace.WriteLine(exc.StackTrace);
                }

                //Kerrotaan ListView-komponentille, että sen pitää näyttää
                //valittuKirjasto.Teokset listaa sen sisällä
                //ListView.ItemSource kertoo itemeiden "lähteen"
                //Jokainen item näytetään rivinä ListView:n sisällä

                dataGrid.ItemsSource = valittuKirjasto.Teokset;

                // Demotarkistus, kovakoodatut arvot
                // Nämä haettaisiin tietokannasta tmv

            }
        }


        private void PäivitäKäyttöliittymä()
        {
            // Tarkistetaan, onko käyttäjä kirjautuneena (onko kirjautunutUsername muuttujassa joku arvo)
            bool kirjautunut = String.IsNullOrEmpty(kirjautunutUsername) == false;

            if (kirjautunut == true)
            {
                // Jos on, näytä "Kirjaudu ulos" painike ja disabloi "Kirjaudu" painike
                kirjauduUlosButton.Visibility = Visibility.Visible;
                avaaKirjButton.IsEnabled = false;
            }
            else
            {
                // Jos ei ole kirjautuneena, piilota "Kirjaudu ulos" painike
                kirjauduUlosButton.Visibility = Visibility.Hidden;
                // Enabloi "Kirjaudu" painike
                avaaKirjButton.IsEnabled = true;
            }
        }

        private void kirjauduUlosButton_Click(object sender, RoutedEventArgs e)
        {
            // Kun käyttäjä klikkaa "Kirjaudu ulos", tyhjää käyttämämme tilatieto
            kirjautunutUsername = "";
            // Päivitä käyttöliittymä, koska käyttäjä kirjautui ulos
            PäivitäKäyttöliittymä();
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
    }
}
