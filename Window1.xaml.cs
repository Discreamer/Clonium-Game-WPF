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
using System.Windows.Shapes;

namespace WpfApp1
{

    public partial class Window1 : Window
    {
        
        public Window1()
        {
            InitializeComponent();
           
        }
        public void ToGame()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PlayersToDelete[0]=3;
            MainWindow.PlayersToDelete[1]=4;
            ToGame();
        }
        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PlayersToDelete[1] = 4;
            ToGame();
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            ToGame();
        }
    }
}
