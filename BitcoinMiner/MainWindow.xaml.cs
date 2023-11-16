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
using System.Windows.Threading;

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
        double aantalBTC = 11999;

        double prijsBasic = 15;
        double prijsAdvanced = 100;
        double prijsMiningRig = 1100;
        double prijsQuantum = 12000;

        double aantalBasic = 0;
        double aantalAdvanced = 0;
        double aantalMiningRig = 0;
        double aantalQuantum = 0;



        DispatcherTimer timer_ms = new DispatcherTimer();
        

        private void Timer_ms_Load()
        {
            timer_ms.Interval = TimeSpan.FromMilliseconds(10);
            timer_ms.Tick += Timer_ms_Tick;
            timer_ms.Start();
        }

        private void Timer_ms_Tick(object sender, EventArgs e)
        {
            CheckStoreAvailability();
            LblBTC.Content = aantalBTC.ToString("N");
            this.Title = $"You have mined {aantalBTC:f2} BTC so far!";
        }

        private void ImgBTC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 230;
            aantalBTC+= 1;
        }
        private void ImgBTC_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 210;

        }
        private void ImgBTC_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgBTC.Height = 210;
        }

        private void CheckStoreAvailability()
        {
            if (aantalBTC >= prijsBasic)
            {
                GridBasic.Opacity = 1;
                GridBasic.IsEnabled = true;
                BorderBasic.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBasic.Opacity = 0.3;
                GridBasic.IsEnabled = false;
                BorderBasic.BorderThickness = new Thickness(0);
            }

            if (aantalBTC >= prijsAdvanced)
            {
                GridAdvanced.Opacity = 1;
                GridAdvanced.IsEnabled = true;
                BorderAdvanced.BorderThickness = new Thickness(2);
            }
            else
            {
                GridAdvanced.Opacity = 0.3;
                GridAdvanced.IsEnabled = false;
                BorderAdvanced.BorderThickness = new Thickness(0);
            }

            if (aantalBTC >= prijsMiningRig)
            {
                GridMiningRig.Opacity = 1;
                GridMiningRig.IsEnabled = true;
                BorderMiningRig.BorderThickness = new Thickness(2);
            }
            else
            {
                GridMiningRig.Opacity = 0.3;
                GridMiningRig.IsEnabled = false;
                BorderMiningRig.BorderThickness = new Thickness(0);
            }

            if (aantalBTC >= prijsQuantum)
            {
                GridQuantum.Opacity = 1;
                GridQuantum.IsEnabled = true;
                BorderQuantum.BorderThickness = new Thickness(2);
            }
            else
            {
                GridQuantum.Opacity = 0.3;
                GridQuantum.IsEnabled = false;
                BorderQuantum.BorderThickness = new Thickness(0);
            }
        }

        private void TooltipLoad()
        {
            ToolTip TTbasic = new ToolTip();
            StringBuilder sbBasic = new StringBuilder();
            sbBasic.AppendLine("Every basic miner will provide a passive income of 0.1 BTC every second");
            TTbasic.Content = sbBasic.ToString();
            GridBasic.ToolTip = TTbasic;

            ToolTip TTadvanced = new ToolTip();
            StringBuilder sbAdvanced = new StringBuilder();
            sbAdvanced.AppendLine("Every Advanced miner will provide a passive income of 1 BTC every second");
            TTadvanced.Content = sbAdvanced.ToString();
            GridAdvanced.ToolTip = TTadvanced;

            ToolTip TTminingRig = new ToolTip();
            StringBuilder sbMiningRig = new StringBuilder();
            sbMiningRig.AppendLine("Every Mining Rig will provide a passive income of 8 BTC every second");
            TTminingRig.Content = sbMiningRig.ToString();
            GridMiningRig.ToolTip = sbMiningRig;

            ToolTip TTquantum = new ToolTip();
            StringBuilder sbQuantum = new StringBuilder();
            sbQuantum.AppendLine("Every Quantum miner will provide a passive income of 47 BTC every second");
            TTquantum.Content = sbQuantum.ToString();
            GridQuantum.ToolTip = sbQuantum;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TooltipLoad();
            Timer_ms_Load();
        }


        #region Shopitems click events
        private void GridBasic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBasic;
            prijsBasic *= 1.15;
            aantalBasic++;
            TxtAantalBasic.Content = (int)aantalBasic;
            TxtprijsBasic.Content = $"{prijsBasic:f2} BTC";
        }

        private void GridAdvanced_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsAdvanced;
            prijsAdvanced *= 1.15;
            aantalAdvanced++;
            TxtAantalAdvanced.Content = (int)aantalAdvanced;
            TxtprijsAdvanced.Content = $"{prijsAdvanced:f2} BTC";
        }

        private void GridMiningRig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsMiningRig;
            prijsMiningRig *= 1.15;
            aantalMiningRig++;
            TxtAantalMiningRig.Content = (int)aantalMiningRig;
            TxtprijsMiningRig.Content = $"{prijsMiningRig:f2} BTC";
        }

        private void GridQuantum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsQuantum;
            prijsQuantum *= 1.15;
            aantalQuantum++;
            TxtAantalQuantum.Content = (int)aantalQuantum;
            TxtprijsQuantum.Content = $"{prijsQuantum:f2} BTC";
        }
        #endregion


        #region Mouse events to change BG
        private void GridBasic_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBasic.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));
        }

        private void GridBasic_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBasic.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }

        private void GridAdvanced_MouseEnter(object sender, MouseEventArgs e)
        {
            GridAdvanced.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridAdvanced_MouseLeave(object sender, MouseEventArgs e)
        {
            GridAdvanced.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }

        private void GridMiningRig_MouseEnter(object sender, MouseEventArgs e)
        {
            GridMiningRig.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridMiningRig_MouseLeave(object sender, MouseEventArgs e)
        {
            GridMiningRig.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridQuantum_MouseEnter(object sender, MouseEventArgs e)
        {
            GridQuantum.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));
        }

        private void GridQuantum_MouseLeave(object sender, MouseEventArgs e)
        {
            GridQuantum.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));
        }
        #endregion
    }
}
