using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using System.Xml.Linq;
using Windows.Storage;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace kalkulatorWalut_Wolski
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        const string daneNBP = "http://www.nbp.pl/kursy/xml/LastA.xml"; //tu adres pliku z danymi kursowymi
        List<PozycjaTabeliA> kursyAktualne = new List<PozycjaTabeliA>();  //lista pozycji z danymi dla kolejnych walut
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        async private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //dane o kursach ze strony NBP
            var serwerNBP = new HttpClient();
            string dane="";
            try
            {
                dane = await serwerNBP.GetStringAsync(new Uri(daneNBP));
            }
            catch(Exception ex)
            { }
            if (dane!="")
            {
                XDocument daneKursowe = XDocument.Parse(dane);
                kursyAktualne = (from item in daneKursowe.Descendants("pozycja")
                                 select new PozycjaTabeliA()
                                 {
                                     kod_waluty = item.Element("kod_waluty").Value,
                                     kurs_sredni = item.Element("kurs_sredni").Value,
                                     przelicznik = item.Element("przelicznik").Value,

                                 }

                               
                
                ).ToList();
                foreach (XElement xelement in daneKursowe.Descendants("data_publikacji"))
                {
                    Data_Aktualizacji.Text = xelement.Value.ToString();
                }

                kursyAktualne.Insert(0, new PozycjaTabeliA() { kurs_sredni = "1,0000", kod_waluty = "PLN", przelicznik = "1" });
                lbxzWaluty.ItemsSource = kursyAktualne;
                lbxNaWalute.ItemsSource = kursyAktualne;
                



            }
        }

        private void txtKwota_TextChanged(object sender, TextChangedEventArgs e) //
        {
            string kwota = txtKwota.Text;

            int dl = txtKwota.Text.Length;
            bool check = true;
            int i = 0;
            do
            {
                if (txtKwota.Text == "")
                {
                    tbPrzeliczona.Text = "";
                }
                else
                {
                    bool czyCyfra = Char.IsNumber(kwota, i);
                    if (!czyCyfra)
                    {

                        if (kwota[i] == ',')
                        {


                        }
                        else
                        {
                            check = false;
                            break;
                        }


                    }
                }

                i++;
            } while (i < dl);
            if (check)
            {
                double wynik = 0;
                if (txtKwota.Text.Length > 0)
                {
                    double liczba1 = double.Parse(txtKwota.Text);
                    double kurs = double.Parse(kursyAktualne[lbxzWaluty.SelectedIndex].kurs_sredni.Replace(",", "."),
                        System.Globalization.CultureInfo.InvariantCulture);
                    double kurs1 = double.Parse(kursyAktualne[lbxNaWalute.SelectedIndex].kurs_sredni.Replace(",", "."),
                        System.Globalization.CultureInfo.InvariantCulture);
                    wynik = (liczba1 * kurs) / kurs1;
                    tbPrzeliczona.Text = wynik.ToString();
                }
            }
            else
            {
                string blad = "BŁĄD";
                tbPrzeliczona.Text = blad.ToString();
            }


          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OProgramie));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string kod = kursyAktualne[lbxzWaluty.SelectedIndex].kod_waluty;
            this.Frame.Navigate(typeof(Pomoc),kod);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //waluta przed
        {
           // textzwaluty.Text = kursyAktualne[lbxzWaluty.SelectedIndex].kod_waluty;

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) //waluta po
        {
          //  textnawalute.Text = kursyAktualne[lbxNaWalute.SelectedIndex].kod_waluty;
        }

        private void lbxzWaluty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double wynik = 0;
            if (txtKwota.Text.Length > 0)
            {
                double liczba1 = double.Parse(txtKwota.Text);
                double kurs = double.Parse(kursyAktualne[lbxzWaluty.SelectedIndex].kurs_sredni.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double kurs1 = double.Parse(kursyAktualne[lbxNaWalute.SelectedIndex].kurs_sredni.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                wynik = (liczba1 * kurs) / kurs1;
                tbPrzeliczona.Text = wynik.ToString();
            }
            textzwaluty.Text = kursyAktualne[lbxzWaluty.SelectedIndex].kod_waluty;
        }

        private void lbxNaWalute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double wynik = 0;
            if (txtKwota.Text.Length > 0)
            {
                double liczba1 = double.Parse(txtKwota.Text);
                double kurs = double.Parse(kursyAktualne[lbxzWaluty.SelectedIndex].kurs_sredni.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                double kurs1 = double.Parse(kursyAktualne[lbxNaWalute.SelectedIndex].kurs_sredni.Replace(",", "."),
                    System.Globalization.CultureInfo.InvariantCulture);
                wynik = (liczba1 * kurs) / kurs1;
                tbPrzeliczona.Text = wynik.ToString();
            }
            textnawalute.Text = kursyAktualne[lbxNaWalute.SelectedIndex].kod_waluty;
        }
    }
}
