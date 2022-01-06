using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Gra_w_kosci_tutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public ObservableCollection<Dice> results { get; set; }
        public ObservableCollection<Score> scores { get; set; }
        public int NumberOfDice { get; set;}
        public int total { get; set; }
        public int trial { get; set; } //ilosc prob, 3 do wyboru wyniku
        public MainWindow()
        {
            InitializeComponent();
            NumberOfDice = 5;
            results = new ObservableCollection<Dice>();
            scores = new ObservableCollection<Score>();
            DataContext = this;
            prepareGame();


        }

        private void prepareGame()
        {
            scores.Add(new Score("jedynki"));
            scores.Add(new Score("dwojki"));
            scores.Add(new Score("trojki"));
            scores.Add(new Score("czworki"));
            scores.Add(new Score("piatki"));
            scores.Add(new Score("szostki"));
            scores.Add(new Score("trzy"));
            scores.Add(new Score("cztery"));
            scores.Add(new Score("full"));
            scores.Add(new Score("malystrit"));
            scores.Add(new Score("duzystrit"));
            scores.Add(new Score("geberal"));
            scores.Add(new Score("szansa"));
            total = 0;
            trial = 3;
            
        }

        private void rollbtn_Click(object sender, RoutedEventArgs e)
        {
            if (trial > 0)
            {
                var random = new Random();
                foreach (var item in results)
                {
                    if (!item.IsSelected)
                        item.Value = random.Next(1, 7);
                }
                showpoints();
                trial -= 1;
            }
            else
            {
                rollbtn.IsEnabled = false;
            }
               
        }

        private void showpoints()
        { if (!scores[0].Done)
            scores[0].Points = suma(results, 1);
          if (!scores[1].Done)
                scores[1].Points = suma(results, 2);
            if (!scores[2].Done)
                scores[2].Points = suma(results, 3);
            if (!scores[3].Done)
                scores[3].Points = suma(results, 4);
            if (!scores[4].Done)
                scores[4].Points = suma(results, 5);
            if (!scores[5].Done)
                scores[5].Points = suma(results, 6);
            //trojka
            if (!scores[6].Done && czytrzy(results)) 
                scores[6].Points = sumaall(results);
            //czworka
            if (!scores[7].Done && czycztery(results))
                scores[7].Points = sumaall(results);
            //full, maly i duzy strit
            if (!scores[8].Done && czyfull(results))
                scores[8].Points = 25;
            if (!scores[9].Done && czymalystrit(results))
                scores[9].Points = 30;
            if (!scores[10].Done && czyduzystrit1(results))
                scores[10].Points = 40;
            if (!scores[11].Done && czygeneral(results))
                scores[11].Points = 50;
            if (!scores[12].Done)
                scores[12].Points = sumaall(results);

        }

        

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            results.Clear(); 
            scores.Clear();
            for (int i = 0; i < NumberOfDice; i++)
            {
                results.Add(new Dice());
            }
            prepareGame();
        }

        private void Button_Dice_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;  
            var dice = button.DataContext as Dice;
            dice.IsSelected = !dice.IsSelected;            
        }

        private int suma(ObservableCollection<Dice> tablica,int n)
        {   int s = 0;
            foreach (Dice d in tablica)
                if (d.Value == n)
                    s = s + d.Value;
            return s;

        }

        private int sumaall(ObservableCollection<Dice> tablica)
        {
            int s = 0;
            foreach (Dice d in tablica)
                s = s + d.Value;
            return s;

        }

        private bool czyduzystrit(ObservableCollection<Dice> tablica)
        {
            int[] liczby = new int[5];
            int i = 0;
            foreach (Dice d in tablica)
            {
                liczby[i] = d.Value;
                i++;
            }
            Array.Sort(liczby);
            int[] p1 = { 1, 2, 3, 4, 5 };
            int[] p2 = { 2, 3, 4, 5 ,6 };

            if (liczby.Equals(p1) || liczby.Equals(p2))
                return true;
            else
                return false;
        }

        private bool czymalystrit(ObservableCollection<Dice> tablica)
        {
            string[] liczby = new string[5];
            int i = 0;
            foreach (Dice d in tablica)
            {
                liczby[i] = d.Value.ToString();
                i++;
            }
            Array.Sort(liczby);
            string ciag = string.Concat(liczby);   

            if (ciag.Contains("1234") || ciag.Contains("2345") || ciag.Contains("3456"))
                return true;
            else
                return false;
        }

        private bool czyduzystrit1(ObservableCollection<Dice> tablica)
        {
            string[] liczby = new string[5];
            int i = 0;
            foreach (Dice d in tablica)
            {
                liczby[i] = d.Value.ToString();
                i++;
            }
            Array.Sort(liczby);
            string ciag = string.Concat(liczby);

            if (ciag.Contains("12345") || ciag.Contains("23456") )
                return true;
            else
                return false;
        }

        private bool czyfull(ObservableCollection<Dice> results)
        {
            int[] ilosc = { 0, 0, 0, 0, 0,0 };
            for (int i=0; i<6; i++)
            {
                foreach(var item in results)
                    if (item.Value.Equals(i+1))
                        ilosc[i]++;
            }
            if (ilosc.Contains(3) && ilosc.Contains(2))
                return true;
            else
                return false;

        }

        private bool czygeneral(ObservableCollection<Dice> results)
        {
            int[] ilosc = { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++)
            {
                foreach (var item in results)
                    if (item.Value.Equals(i + 1))
                        ilosc[i]++;
            }
            if (ilosc.Contains(5))
                return true;
            else
                return false;
        }

        private bool czycztery(ObservableCollection<Dice> results)
        {
            int[] ilosc = { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++)
            {
                foreach (var item in results)
                    if (item.Value.Equals(i + 1))
                        ilosc[i]++;
            }
            int max = ilosc.Max();
            if (max>=4) return true;
            else return false;
        }

        private bool czytrzy(ObservableCollection<Dice> results)
        {
            int[] ilosc = { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++)
            {
                foreach (var item in results)
                    if (item.Value.Equals(i + 1))
                        ilosc[i]++;
            }
            int max = ilosc.Max();
            if (max >= 3) return true;
            else return false;
        }
        private void donebtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in scores)
            {
                if (item.Done)
                {
                    item.Name = item.Name.ToUpper();
                }

            }
            trial = 3;
            rollbtn.IsEnabled = true;
            results.Clear();
            for (int i = 0; i < NumberOfDice; i++)
            {
                results.Add(new Dice());
            }
            int s = 0;
            foreach(var item in scores)
            {
                
                if (item.Done)
                {
                    s = s + item.Points;
                }
            }
            totalpoints.Content = s.ToString();


        }
    }


}

