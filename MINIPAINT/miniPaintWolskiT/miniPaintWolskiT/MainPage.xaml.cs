using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;


//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace miniPaintWolskiT
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Stack<UIElement> listaUndo = new Stack<UIElement>();
       
        SolidColorBrush pisak = new SolidColorBrush(Windows.UI.Colors.Black);
        Point pktStartu;
        Point pktRuch;
        bool czyRysuje = false;
        bool czyDowolny = false;
        UIElement usunPoprzednie;
        public MainPage()
        {
            this.InitializeComponent();
        }
        void usunLinie()
        {
            if (usunPoprzednie != null)
            {
                poleRysowania.Children.Remove(usunPoprzednie);
                usunPoprzednie = null;
                
            }
        }
        private void poleRysowania_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            pktRuch = e.GetCurrentPoint(poleRysowania).Position;
            SolidColorBrush pisak = new SolidColorBrush(Windows.UI.Colors.Black);
            RysujLinie();

            pktRuch = e.GetCurrentPoint(poleRysowania).Position;
            RysujLinie();
            if (czyDowolny)
            {
                pktStartu = pktRuch;
            }
        }
        private void RysujLinie()
        {
            if (czyRysuje == true)
            {
                Line linia = new Line()
                {

                    X1 = pktStartu.X,
                    Y1 = pktStartu.Y,
                    X2 = pktRuch.X,
                    Y2 = pktRuch.Y,
                    Stroke = pisak,
                    StrokeThickness=slider.Value,
                    StrokeStartLineCap=PenLineCap.Round,
                    StrokeEndLineCap=PenLineCap.Round,

                };
                
                if (!czyDowolny)
                {
                    usunLinie();
                    
                }
                poleRysowania.Children.Add(linia);
                usunPoprzednie = linia;
                listaUndo.Push(linia);
            }
            
        }

        private void poleRysowania_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pktStartu = e.GetCurrentPoint(poleRysowania).Position;
            czyRysuje = true;
        }

        private void poleRysowania_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            czyRysuje = false;
            usunPoprzednie = null;
            


        }

        private void Prosta_Checked(object sender, RoutedEventArgs e)
        {
            czyDowolny = false;
            
        }

        private void Dowolna_Checked(object sender, RoutedEventArgs e)
        {
            czyDowolny = true;
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
        }

        private void StackPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Red);
        }

        private void StackPanel_PointerPressed_1(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Black);
        }

        private void StackPanel_PointerPressed_2(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Green);
        }

        private void StackPanel_PointerPressed_3(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Blue);
        }

        private void Button_PointerPressed(object sender, PointerRoutedEventArgs e) 
        {

        }

        private async void Button_PointerPressed_1(object sender, PointerRoutedEventArgs e) 
        {
          
        }

        private void StackPanel_PointerPressed_4(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Purple);
        }

        private void StackPanel_PointerPressed_5(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Yellow);
        }

        private void StackPanel_PointerPressed_6(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Orange);
        }

        private void StackPanel_PointerPressed_7(object sender, PointerRoutedEventArgs e)
        {
            pisak = new SolidColorBrush(Windows.UI.Colors.Pink);
        }

        private async void Button_Click(object sender, RoutedEventArgs e) //zakoncz
        {
            ContentDialog zakoncz = new ContentDialog
            {
                Title = "Wyjście z programu.",
                Content = "Utracisz swoje arcydzieło.",
                CloseButtonText = "Zakończ",
                PrimaryButtonText = "Anuluj"
                

            };
            
            ContentDialogResult result = await zakoncz.ShowAsync();
            if (result == ContentDialogResult.Primary)
            { }
            else
            {
                CoreApplication.Exit();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //cofnij
        {
            if (listaUndo.Count > 0) 
            {
                Shape undo = (Shape)listaUndo.Pop();
                poleRysowania.Children.Remove(undo); 
            }

        }
    }
}
