using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        double aantalBTC = 19999999;

        //prijs van elk shopitem
        double prijsBasic = 15;
        double prijsAdvanced = 100;
        double prijsMiningRig = 1100;
        double prijsQuantum = 12000;
        double prijsClock = 130000;
        double prijsCooler = 1400000;
        double prijsSecurity = 20000000;

        //aantal shopitems de klikker heeft
        double aantalBasic = 0;
        double aantalAdvanced = 0;
        double aantalMiningRig = 0;
        double aantalQuantum = 0;
        double aantalClock = 0;
        double aantalCooler = 0;
        double aantalSecurity = 0;

        //passief inkomen dat er bekomen in
        double passiefBasic = 0;
        double passiefAdvanced = 0;
        double passiefMiningRig = 0;
        double passiefQuantum = 0;
        double passiefClock = 0;
        double passiefCooler = 0;
        double passiefSecurity = 0;
        double passiefTotaal = 0;


        DispatcherTimer timer_ms = new DispatcherTimer();

        private Stopwatch stopwatch = new Stopwatch();
        private double elapsedTimeInSeconds = 0;


        private void Timer_ms_Load()
        {
            timer_ms.Interval = TimeSpan.FromMilliseconds(10);
            timer_ms.Tick += Timer_ms_Tick;
            timer_ms.Start();
        }
        private void Timer_ms_Tick(object sender, EventArgs e)
        {
            CheckStoreAvailability();

            elapsedTimeInSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
            stopwatch.Restart();

            //update totale passief BTC
            passiefTotaal = passiefBasic + passiefAdvanced + passiefMiningRig + passiefQuantum + passiefClock + passiefCooler + passiefSecurity;
            LblBTCpersec.Content = passiefTotaal.ToString();

            //update totale BTC adhv passief inkomen
            aantalBTC += (passiefTotaal * elapsedTimeInSeconds);

            //update totale BTC in shop en titel
            WeergaveCijfer();
        }

        private void WeergaveCijfer()
        {
            double tempBTC = 0;

            if (aantalBTC >= 1000000000000000000)
            {
                tempBTC = aantalBTC / 1000000000000000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Quintillion";
                this.Title = $"You have {tempBTC:f3} Quintillion BTC!";
            }
            else if (aantalBTC >= 1000000000000000)
            {
                tempBTC = aantalBTC / 1000000000000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Quadrillion";
                this.Title = $"You have {tempBTC:f3} Quadrillion BTC";
            }
            else if (aantalBTC >= 1000000000000)
            {
                tempBTC = aantalBTC / 1000000000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Trillion";
                this.Title = $"You have {tempBTC:f3} Trillion BTC";
            }
            else if (aantalBTC >= 1000000000000)
            {
                tempBTC = aantalBTC / 1000000000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Trillion";
                this.Title = $"You have {tempBTC:f3} Trillion BTC";
            }
            else if (aantalBTC >= 1000000000)
            {
                tempBTC = aantalBTC / 1000000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Billion";
                this.Title = $"You have {tempBTC:f3} Billion BTC";
            }
            else if (aantalBTC >= 1000000)
            {
                tempBTC = aantalBTC / 1000000;
                TxtAantalBTC.Content = $"{tempBTC:f3} Million";
                this.Title = $"You have {tempBTC:f3} Million BTC";
            }
            else if (aantalBTC <1000000)
            {
                NumberFormatInfo formatInfo = new NumberFormatInfo
                {
                    NumberGroupSeparator = " "
                };
                TxtAantalBTC.Content = aantalBTC.ToString("n0", formatInfo);

            }
            else
            {
                TxtAantalBTC.Content = $"{Math.Ceiling(aantalBTC)}";
                this.Title = $"You have {Math.Ceiling(aantalBTC)} BTC";
            }
        }

        #region BTC_Munt_klikEvents
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
        #endregion
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

            if (aantalBTC >= prijsClock)
            {
                GridClock.Opacity = 1;
                GridClock.IsEnabled = true;
                BorderClock.BorderThickness = new Thickness(2);
            }
            else
            {
                GridClock.Opacity = 0.3;
                GridClock.IsEnabled = false;
                BorderClock.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsCooler)
            {
                GridCooling.Opacity = 1;
                GridCooling.IsEnabled = true;
                BorderCooling.BorderThickness = new Thickness(2);
            }
            else
            {
                GridCooling.Opacity = 0.3;
                GridCooling.IsEnabled = false;
                BorderCooling.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsSecurity)
            {
                GridSecurity.Opacity = 1;
                GridSecurity.IsEnabled = true;
                BorderSecurity.BorderThickness = new Thickness(2);
            }
            else
            {
                GridSecurity.Opacity = 0.3;
                GridSecurity.IsEnabled = false;
                BorderSecurity.BorderThickness = new Thickness(0);
            }
        }

        private void TekstGamification()
        {
            if (passiefTotaal >= 100 )
            {
                TxtAantalBTC.FontSize = 36;
                TxtBTC.FontSize = 36;
            }
            else if (passiefTotaal >= 50)
            {
                TxtAantalBTC.FontSize = 32;
                TxtBTC.FontSize = 32;
            }
            else if (passiefTotaal >= 25)
            {
                TxtAantalBTC.FontSize = 28;
                TxtBTC.FontSize = 28;
            }
            else if (passiefTotaal >= 10)
            {
                TxtAantalBTC.FontSize = 24;
                TxtBTC.FontSize = 24;
            }
            else if (passiefTotaal >= 1)
            {
                TxtAantalBTC.FontSize = 20;
                TxtBTC.FontSize = 20;
            }
        }

        #region Shopitems click events
        private void GridBasic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBasic;
            prijsBasic *= 1.15;
            aantalBasic++;
            TxtAantalBasic.Content = (int)aantalBasic;
            TxtprijsBasic.Content = $"{Math.Ceiling(prijsBasic)} BTC";

            passiefBasic += 0.1;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BasicTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BasicTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BasicTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BasicTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridAdvanced_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsAdvanced;
            prijsAdvanced *= 1.15;
            aantalAdvanced++;
            TxtAantalAdvanced.Content = (int)aantalAdvanced;
            TxtprijsAdvanced.Content = $"{Math.Ceiling(prijsAdvanced)} BTC";

            passiefAdvanced += 1;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            AdvancedTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            AdvancedTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            AdvancedTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            AdvancedTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);

        }

        private void GridMiningRig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsMiningRig;
            prijsMiningRig *= 1.15;
            aantalMiningRig++;
            TxtAantalMiningRig.Content = (int)aantalMiningRig;
            TxtprijsMiningRig.Content = $"{Math.Ceiling(prijsMiningRig)} BTC";

            passiefMiningRig += 8;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            MiningRigTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            MiningRigTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            MiningRigTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            MiningRigTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridQuantum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsQuantum;
            prijsQuantum *= 1.15;
            aantalQuantum++;
            TxtAantalQuantum.Content = (int)aantalQuantum;
            TxtprijsQuantum.Content = $"{Math.Ceiling(prijsQuantum)} BTC";

            passiefQuantum += 47;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            QuantumTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            QuantumTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            QuantumTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            QuantumTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridClock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsClock;
            prijsClock *= 1.15;
            aantalClock++;
            TxtAantalClock.Content = (int)aantalClock;
            TxtPrijsClock.Content = $"{Math.Ceiling(prijsClock)} BTC";

            passiefClock += 260;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            ClockTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            ClockTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            ClockTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            ClockTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridCooling_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsCooler;
            prijsCooler *= 1.15;
            aantalCooler++;
            TxtAantalCooling.Content = (int)aantalCooler;
            TxtPrijsCooling.Content = $"{Math.Ceiling(prijsCooler)} BTC";

            passiefClock += 1400;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            CoolingTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            CoolingTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            CoolingTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            CoolingTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridSecurity_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsSecurity;
            prijsSecurity *= 1.15;
            aantalSecurity++;
            TxtAantalSecurity.Content = (int)aantalSecurity;
            TxtPrijsSecurity.Content = $"{Math.Ceiling(prijsSecurity)} BTC";

            passiefSecurity += 1400;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            SecurityTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            SecurityTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            SecurityTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            SecurityTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
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
        private void GridClock_MouseEnter(object sender, MouseEventArgs e)
        {
            GridClock.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridClock_MouseLeave(object sender, MouseEventArgs e)
        {
            GridClock.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }

        private void GridCooling_MouseEnter(object sender, MouseEventArgs e)
        {
            GridCooling.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridCooling_MouseLeave(object sender, MouseEventArgs e)
        {
            GridCooling.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }

        private void GridSecurity_MouseEnter(object sender, MouseEventArgs e)
        {
            GridSecurity.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridSecurity_MouseLeave(object sender, MouseEventArgs e)
        {
            GridSecurity.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TooltipLoad();
            Timer_ms_Load();
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

            ToolTip TTclock = new ToolTip();
            StringBuilder sbClock = new StringBuilder();
            sbClock.AppendLine("Every Overclocking Module will provide a passive income of 260 BTC every second");
            TTclock.Content = sbClock.ToString();
            GridClock.ToolTip = sbClock;

            ToolTip TTcooler = new ToolTip();
            StringBuilder sbCooler = new StringBuilder();
            sbClock.AppendLine("Every Automatic Cooling System will provide a passive income of 1400 BTC every second");
            TTcooler.Content = sbCooler.ToString();
            GridCooling.ToolTip = sbCooler;

            ToolTip TTsecurity = new ToolTip();
            StringBuilder sbSecurity = new StringBuilder();
            sbSecurity.AppendLine("Every BlockChain Security Protocol will provide a passive income of 7800 BTC every second");
            TTsecurity.Content = sbSecurity.ToString();
            GridSecurity.ToolTip = sbSecurity;



        }

        private void LblNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VraagNaam();

        }
        private void VraagNaam()
        {
            string invoer = Interaction.InputBox("What's the name of your Bitcoin Mining Emperium?");
            if (invoer == "")
            {
                MessageBox.Show("Please enter a valid name");
                VraagNaam();
            }
            else if (invoer.Length > 12 | invoer.Length < 3)
            {
                MessageBox.Show("Please enter a name between 3 and 12 characters");
                VraagNaam();
            }
          
            else
            {
                LblNaam.Content = $"{invoer}'s Mining Emperium";
            }
        }

        private void LblNaam_MouseEnter(object sender, MouseEventArgs e)
        {
            LblNaam.Foreground = Brushes.Purple;
        }

        private void LblNaam_MouseLeave(object sender, MouseEventArgs e)
        {
            LblNaam.Foreground = new SolidColorBrush(Color.FromRgb(0, 172, 209));
        }
    }
}
