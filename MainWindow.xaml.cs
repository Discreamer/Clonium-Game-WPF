using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static int[] PlayersToDelete = new int[4] {99, 99,99,99 };
        bool[] Players = new bool[4] { true, true, true, true };
        public const int xy = 50;
        public int[,] map = new int[8,8]
        {
            {0,0,0,0,0,0,0,0},
            {0,33,0,0,0,0,43,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,23,0,0,0,0,13,0},
            {0,0,0,0,0,0,0,0}
        };
        public int[,] TempMap= new int[8, 8]
        {
            {0,0,0,0,0,0,0,0},
            {0,33,0,0,0,0,43,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,23,0,0,0,0,13,0},
            {0,0,0,0,0,0,0,0}
        };
        public int TempOcher;
        public int Ocher;
        public bool HaveBlackBorder=false;
        public MainWindow()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            map = new int[8, 8]
            {
                {0,0,0,0,0,0,0,0},
                {0,33,0,0,0,0,43,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,23,0,0,0,0,13,0},
                {0,0,0,0,0,0,0,0}
            };
            foreach(byte some in PlayersToDelete)
            {
                DeletePlayer(some);
            }
            PlayersToDelete= new int[4] { 99, 99, 99, 99 };
            Ocher = 1;
            CreateMap();
        }
        public void DeletePlayer(Byte Modes)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (map[y, x] / 10 == Modes)
                    {
                        map[y, x] = 0;
                    }
                }
            }
        }
        public void CreateNewButton(int y, int x)
        {
            
            int NumPlayer = map[y, x] / 10;

            Button SomeButton = new Button();
            SomeButton.Width = 50;
            SomeButton.Height = 50;
            SomeButton.FontSize = 30;
            SomeButton.Click += new RoutedEventHandler(OnFigurePress);
            myCanvas.Children.Add(SomeButton);
            Canvas.SetLeft(SomeButton, x * 50);
            Canvas.SetTop(SomeButton, y * 50);
            if (NumPlayer == Ocher)
            {
                HaveBlackBorder = true;
                SomeButton.BorderBrush = Brushes.Black;
            }
            else
            {
                SomeButton.BorderBrush = Brushes.Gray;
            }

            int PieceCounter = map[y, x] % 10;
            switch (NumPlayer)
            {
                case 0:
                    SomeButton.Background = Brushes.White;
                    break;
                case 1:
                    SomeButton.Background = Brushes.Pink;
                    break;
                case 2:
                    SomeButton.Background = Brushes.Green;
                    break;
                case 3:
                    SomeButton.Background = Brushes.Red;
                    break;
                case 4:
                    SomeButton.Background = Brushes.Blue;
                    break;
                default:
                    SomeButton.Background = Brushes.Yellow;
                    break;

            }

            if (PieceCounter != 0)
            {
                SomeButton.Content = PieceCounter.ToString();
            }
        }
      
        public void CreateMap()
        {

            HaveBlackBorder = false;
            bool pl1 = false;
            bool pl2 = false;
            bool pl3 = false;
            bool pl4 = false;
            myCanvas.Children.Clear();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (map[y, x] / 10 == 1)
                    {
                        pl1 = true;
                    }
                    else if (map[y, x] / 10 == 2)
                    {
                        pl2 = true;
                    }
                    else if (map[y, x] / 10 == 3)
                    {
                        pl3 = true;
                    }
                    else if (map[y, x] / 10 == 4)
                    {
                        pl4 = true;
                    }
                    CreateNewButton(y, x);
                    if (map[y, x] % 10 >= 4)
                    {
                        DoMnoj(y, x);
                        CreateMap();
                    }
                }
            }
            Players[0] = pl1;
            Players[1] = pl2;
            Players[2] = pl3;
            Players[3] = pl4;
            int LivePlayers = 0;
            int LastPlayer;


            for (int i=0;i<4;i++)
            {
                if (Players[i])
                {
                    LivePlayers++;
                    LastPlayer = i;
                }
            }
            if (LivePlayers == 1)
            {
                init();
            }
            if (!HaveBlackBorder)
            {
                Ocher++;
                if (Ocher == 5) { Ocher = 1; }
                CreateMap();
            }
        }
        public void DoMnoj(int y, int x)
        {
            try
            {
                map[y, x + 1] = map[y, x + 1] % 10 + map[y, x] / 10 * 10+1;
            }
            catch { }
            try
            {
                map[y, x - 1] = map[y, x - 1] % 10 + map[y, x] / 10 * 10+1;
            }
            catch { }
            try
            {
                map[y-1, x] = map[y-1, x] % 10 + map[y, x] / 10 * 10+1;
            }
            catch { }
            try
            {
                map[y + 1, x] = map[y+1, x] % 10+map[y, x] / 10 * 10+1;
            }
            catch { }
            if (map[y, x] == 5)
            {
                map[y, x] = 1;
            }
            else
            {
                map[y, x] = 0;
            }
            
        }
        public void OnFigurePress(object sender, RoutedEventArgs e)
        {
            Button presBtn = sender as Button;
            int y = Convert.ToInt32(Canvas.GetTop(presBtn)) / xy;
            int x = Convert.ToInt32(Canvas.GetLeft(presBtn)) / xy;
            while (!Players[Ocher - 1])
            {
                Ocher++;
                if (Ocher == 5) { Ocher = 1; }
            }
            
            if (map[y, x] != 0 && Ocher == map[y, x] / 10)
            {
                map[y, x] += 1;
                Ocher++;
                if (Ocher == 5) { Ocher = 1; }
            }
            CreateMap();
           
           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 MainMenu = new Window1();
            MainMenu.Show();
            this.Close();
        }

    }
}
