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

namespace BitcoinMiner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int aantalBTC = 0;
     
        private void ImgBTC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 230;
            aantalBTC++;
            LblBTC.Content = aantalBTC.ToString();
            this.Title = $"You have mined {aantalBTC} BTC so far!";
        }
        private void ImgBTC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 210;

        }
        private void ImgBTC_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgBTC.Height = 210;
        }
    }
}
