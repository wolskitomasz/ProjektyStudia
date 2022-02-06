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
//nowe
using Windows.Devices.Geolocation;
using Windows.Services.Maps;



//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace Nawigator
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class Koordynaty : Page
    {
        public Koordynaty()
        {
            this.InitializeComponent();
            //wyznacz dł i szer -> tbGPS.text
            GdzieJaNaMapie();
        }
        async void GdzieJaNaMapie()
        {
            var mojGps = new Geolocator()
            {
                DesiredAccuracy = PositionAccuracy.High
            };
            var daneGPS = await mojGps.GetGeopositionAsync();
            tbGPS.Text = "dłg.: " + daneGPS.Coordinate.Point.Position.Longitude;
            tbGPS.Text += " szer.: " + daneGPS.Coordinate.Point.Position.Latitude;

            DaneGeograficzne.pktStartowy = daneGPS.Coordinate.Point.Position;
            DaneGeograficzne.opisCelu = "";
            BasicGeoposition PozycjaStartu = new BasicGeoposition();
            PozycjaStartu.Latitude = daneGPS.Coordinate.Point.Position.Latitude;
            PozycjaStartu.Longitude = daneGPS.Coordinate.Point.Position.Longitude;

        }

        private void wstecz(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void szukaj(object sender, RoutedEventArgs e)
        {
            DaneGeograficzne.opisCelu = txAdres.Text;
            if (DaneGeograficzne.opisCelu == "")
                return;

            MapLocationFinderResult SzukajAdres = await MapLocationFinder.FindLocationsAsync(txAdres.Text, new Geopoint(DaneGeograficzne.pktStartowy), 3);

            if (SzukajAdres.Status == MapLocationFinderStatus.Success)
            {
                DaneGeograficzne.pktDocelowy = SzukajAdres.Locations[0].Point.Position;
                DaneGeograficzne.opisCelu = SzukajAdres.Locations[0].DisplayName;
                tbDlg.Text += SzukajAdres.Locations[0].Point.Position.Longitude.ToString();
                tbSzer.Text += SzukajAdres.Locations[0].Point.Position.Latitude.ToString();
            }
            else
            {
                txAdres.Text = "Wystąpił błąd";
            }
        }
    }
    
}
