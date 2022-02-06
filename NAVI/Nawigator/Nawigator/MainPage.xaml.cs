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
using Windows.UI.Xaml.Controls.Maps;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace Nawigator
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
           
        public MainPage()
        {
            this.InitializeComponent();
            
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DaneGeograficzne.opisCelu == null)
                return;
            Geopoint PunktStartowy = new Geopoint(DaneGeograficzne.pktStartowy);
            MapIcon start = new MapIcon()
            {
                Location = PunktStartowy,
                Title = "Tu jestem!"
            };
            mojaMapa.MapElements.Add(start);
            Geopoint PunktDocelowy = new Geopoint(DaneGeograficzne.pktDocelowy);
            MapIcon koniec = new MapIcon()
            {
                Location = PunktDocelowy,
                Title = "Meta"
            };
            mojaMapa.MapElements.Add(koniec);

            MapPolyline trasaLotem = new MapPolyline()
            {
                StrokeColor = Windows.UI.Colors.Black,
                StrokeThickness = 3,
                StrokeDashed = true,
                Path = new Geopath(new List<BasicGeoposition> { DaneGeograficzne.pktStartowy, DaneGeograficzne.pktDocelowy })
            };
            mojaMapa.MapElements.Add(trasaLotem);
            await mojaMapa.TrySetViewAsync(new Geopoint(DaneGeograficzne.pktStartowy), 8);
            Odleglosc();
            Trasa();
        }
        private async void Odleglosc()
        {
            double szer1 = DaneGeograficzne.pktStartowy.Latitude;
            double szer2 = DaneGeograficzne.pktDocelowy.Latitude;
            double dl1 = DaneGeograficzne.pktStartowy.Longitude;
            double dl2 = DaneGeograficzne.pktDocelowy.Longitude;

            double dist = Math.Sqrt(Math.Pow((szer2 - szer1), 2) + Math.Pow(Math.Cos((szer1 * Math.PI) / 180) * (dl2 - dl1), 2));
            dist = dist * 40075.704;
            dist = dist / 360;
            dist = Math.Round(dist, 2);

            ContentDialog odleglosc = new ContentDialog
            {
                Title = "Dystans",
                Content = "Odległość to " + dist + " km.",
                CloseButtonText = "OK"

            };

            await odleglosc.ShowAsync();
            DaneGeograficzne.odlegloscwkm = dist.ToString();
            Odlegloscwkm.Text = DaneGeograficzne.odlegloscwkm + " km";
        }
        private async void Trasa()
        {
            
            BasicGeoposition start = new BasicGeoposition() { Latitude = DaneGeograficzne.pktStartowy.Latitude, Longitude = DaneGeograficzne.pktStartowy.Longitude };
            BasicGeoposition end = new BasicGeoposition() { Latitude = DaneGeograficzne.pktDocelowy.Latitude, Longitude = DaneGeograficzne.pktDocelowy.Longitude };

            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  new Geopoint(start),
                  new Geopoint(end),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                mojaMapa.Routes.Add(viewOfRoute);

                await mojaMapa.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }
        private void powMapa(object sender, RoutedEventArgs e)
        {
            mojaMapa.ZoomLevel++;
        }

        private void pomMapa(object sender, RoutedEventArgs e)
        {
            mojaMapa.ZoomLevel--;
        }

        private void trybMapa(object sender, RoutedEventArgs e)
        {
            
            var ab = sender as AppBarButton;
            var fIcon = new FontIcon()
            {
                FontFamily = FontFamily.XamlAutoFontFamily
            };
            if (mojaMapa.Style==MapStyle.AerialWithRoads)
            {
                //przelacz na zwykly
                mojaMapa.Style = MapStyle.Road;
                ab.Label = "satelita";
                fIcon.Glyph = "S";
            }
            else
            {
                //przelacz na satelite
                mojaMapa.Style = MapStyle.AerialWithRoads;
                ab.Label = "mapa";
                fIcon.Glyph = "M";
            }
            ab.Icon = fIcon;
        }

        private void koordynaty(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Koordynaty));
        }
    }
}
