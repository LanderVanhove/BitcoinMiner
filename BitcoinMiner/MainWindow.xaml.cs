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
        double aantalBTC = 0;
        double kostBasic = 15;
        double kostAdvanced = 100;
        double kostMiningRig = 1100;
        double kostQuantum = 12000;
        
     
        private void ImgBTC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 230;
            aantalBTC+= 15;
            LblBTC.Content = aantalBTC.ToString();
            this.Title = $"You have mined {aantalBTC} BTC so far!";
            CheckStoreAvailability();
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
            if (aantalBTC >= kostBasic)
            {
                GridBasic.Opacity = 1;
                GridBasic.IsEnabled = true;
                BorderBasic.BorderThickness = new Thickness(2);
            }
            if (aantalBTC >= kostAdvanced)
            {
                GridAdvanced.Opacity = 1;
                GridAdvanced.IsEnabled = true;
                BorderAdvanced.BorderThickness = new Thickness(2);
            }
            if (aantalBTC >= kostMiningRig)
            {
                GridMiningRig.Opacity = 1;
                GridMiningRig.IsEnabled = true;
                BorderMiningRig.BorderThickness = new Thickness(2);
            }
            if (aantalBTC >= kostQuantum)
            {
                GridQuantum.Opacity = 1;
                GridQuantum.IsEnabled = true;
                BorderQuantum.BorderThickness = new Thickness(2);
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
        }
    }
}
