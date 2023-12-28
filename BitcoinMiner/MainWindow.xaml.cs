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

        double aantalBTC = 0;
        double aantalBTCooit = 0;

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

        //aantal bonus shopitems de klikker heeft
        double aantalBonusBasic = 1;
        double aantalBonusAdvanced = 1;
        double aantalBonusMiningRig = 1;
        double aantalBonusQuantum = 1;
        double aantalBonusClock = 1;
        double aantalBonusCooler = 1;
        double aantalBonusSecurity = 1;


        //passief inkomen dat er bekomen in
        double passiefBasic = 0;
        double passiefAdvanced = 0;
        double passiefMiningRig = 0;
        double passiefQuantum = 0;
        double passiefClock = 0;
        double passiefCooler = 0;
        double passiefSecurity = 0;
        double passiefTotaal = 0;

        //prijs van elk Bonusshop item
        double prijsBonusBasic = 1500;
        double prijsBonusAdvanced = 10000;
        double prijsBonusMiningRig = 110000;
        double prijsBonusQuantum = 1200000;
        double prijsBonusClock = 13000000;
        double prijsBonusCooler = 140000000;
        double prijsBonusSecurity = 2000000000;

        double[] bonusPrijzen = new double[7] {1500, 10000, 110000, 1200000, 13000000, 140000000, 2000000000};

        private void BonusStorePrijzen(double item, int x)
        {
            if (item == 1)
            {
                bonusPrijzen[x] *= 5;
            }
            else
            {
                bonusPrijzen[x] *= 10;
            }
        }
        private void BonusPrijzenUpdaten()
        {
             prijsBonusBasic = bonusPrijzen[0];
             prijsBonusAdvanced = bonusPrijzen[1];
             prijsBonusMiningRig = bonusPrijzen[2];
             prijsBonusQuantum = bonusPrijzen[3];
             prijsBonusClock = bonusPrijzen[4];
             prijsBonusCooler = bonusPrijzen[5];
             prijsBonusSecurity = bonusPrijzen[6];
        }

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
            CheckBonusStoreAvailability();
            ShowStoreItemsBasedOnBTC();

            elapsedTimeInSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
            stopwatch.Restart();

            //update totale passief BTC
            passiefTotaal = passiefBasic + passiefAdvanced + passiefMiningRig + passiefQuantum + passiefClock + passiefCooler + passiefSecurity;
            LblBTCpersec.Content = passiefTotaal.ToString();

            //update totale BTC adhv passief inkomen
            aantalBTC += (passiefTotaal * elapsedTimeInSeconds);
            aantalBTCooit += (passiefTotaal * elapsedTimeInSeconds);

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
            else if (aantalBTC < 1000000)
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
            aantalBTC += 1;
            aantalBTCooit += 1;
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

        private void ShowStoreItemsBasedOnBTC()
        {
            if (aantalBTCooit < 15)
            {
                GridBasic.Opacity = 0;
                GridBonusBasic.Opacity = 0;
                BasicMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 100)
            {
                GridAdvanced.Opacity = 0;
                GridBonusAdvanced.Opacity = 0;
                AdvancedMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 1100)
            {
                GridMiningRig.Opacity = 0;
                GridBonusMining.Opacity = 0;
                MiningMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 12000)
            {
                GridQuantum.Opacity = 0;
                GridBonusQuantum.Opacity = 0;
                QuantumMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 130000)
            {
                GridClock.Opacity = 0;
                GridBonusClock.Opacity = 0;
                ClockMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 1400000)
            {
                GridCooling.Opacity = 0;
                GridBonusCooling.Opacity = 0;
                CoolingMultiplier.Opacity = 0;
            }
            if (aantalBTCooit < 20000000)
            {
                GridSecurity.Opacity = 0;
                GridBonusSecurity.Opacity = 0;
                SecurityMultiplier.Opacity = 0;
            }
            if (aantalBTCooit > 14)
            {
                Shop.Visibility = Visibility.Visible;
            }

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
            if (aantalBTC >= prijsBonusBasic)
            {
                GridBonusBasic.Opacity = 1;
                GridBonusBasic.IsEnabled = true;
                BorderBonusBasic.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusBasic.Opacity = 0.3;
                GridBonusBasic.IsEnabled = false;
                BorderBonusBasic.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusAdvanced)
            {
                GridBonusAdvanced.Opacity = 1;
                GridBonusAdvanced.IsEnabled = true;
                BorderBonusAdvanced.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusAdvanced.Opacity = 0.3;
                GridBonusAdvanced.IsEnabled = false;
                BorderBonusAdvanced.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusMiningRig)
            {
                GridBonusMining.Opacity = 1;
                GridBonusMining.IsEnabled = true;
                BorderBonusMining.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusMining.Opacity = 0.3;
                GridBonusMining.IsEnabled = false;
                BorderBonusMining.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusQuantum)
            {
                GridBonusQuantum.Opacity = 1;
                GridBonusQuantum.IsEnabled = true;
                BorderBonusQuantum.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusQuantum.Opacity = 0.3;
                GridBonusQuantum.IsEnabled = false;
                BorderBonusQuantum.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusClock)
            {
                GridBonusClock.Opacity = 1;
                GridBonusClock.IsEnabled = true;
                BorderBonusClock.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusClock.Opacity = 0.3;
                GridBonusClock.IsEnabled = false;
                BorderBonusClock.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusCooler)
            {
                GridBonusCooling.Opacity = 1;
                GridBonusCooling.IsEnabled = true;
                BorderBonusCooling.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusCooling.Opacity = 0.3;
                GridBonusCooling.IsEnabled = false;
                BorderBonusCooling.BorderThickness = new Thickness(0);
            }
            if (aantalBTC >= prijsBonusSecurity)
            {
                GridBonusSecurity.Opacity = 1;
                GridBonusSecurity.IsEnabled = true;
                BorderBonusSecurity.BorderThickness = new Thickness(2);
            }
            else
            {
                GridBonusSecurity.Opacity = 0.3;
                GridBonusSecurity.IsEnabled = false;
                BorderBonusSecurity.BorderThickness = new Thickness(0);
            }



        }

        private void CheckBonusStoreAvailability()
        {
            if (aantalBTCooit >= 15)
            {
                DpMenu.Visibility = Visibility.Visible;

            }

        }

        #region Shopitems click events
        private void GridBasic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToevoegenBasic();
            aantalBTC -= prijsBasic;
            prijsBasic *= 1.15;
            aantalBasic++;
            TxtAantalBasic.Content = (int)aantalBasic;
            TxtprijsBasic.Content = $"{Math.Ceiling(prijsBasic)} BTC";

            passiefBasic += (0.1 * aantalBonusBasic);

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
            ToevoegenAdvanced();
            aantalBTC -= prijsAdvanced;
            prijsAdvanced *= 1.15;
            aantalAdvanced++;
            TxtAantalAdvanced.Content = (int)aantalAdvanced;
            TxtprijsAdvanced.Content = $"{Math.Ceiling(prijsAdvanced)} BTC";

            passiefAdvanced += (1 * aantalBonusAdvanced);

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
            ToevoegenMining();
            aantalBTC -= prijsMiningRig;
            prijsMiningRig *= 1.15;
            aantalMiningRig++;
            TxtAantalMiningRig.Content = (int)aantalMiningRig;
            TxtprijsMiningRig.Content = $"{Math.Ceiling(prijsMiningRig)} BTC";

            passiefMiningRig += (8 * aantalBonusMiningRig);

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
            ToevoegenQuantum();
            aantalBTC -= prijsQuantum;
            prijsQuantum *= 1.15;
            aantalQuantum++;
            TxtAantalQuantum.Content = (int)aantalQuantum;
            TxtprijsQuantum.Content = $"{Math.Ceiling(prijsQuantum)} BTC";

            passiefQuantum += (47 * aantalBonusQuantum);

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
            ToevoegenClock();
            aantalBTC -= prijsClock;
            prijsClock *= 1.15;
            aantalClock++;
            TxtAantalClock.Content = (int)aantalClock;
            TxtPrijsClock.Content = $"{Math.Ceiling(prijsClock)} BTC";

            passiefClock += (260 * aantalBonusClock);

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
            ToevoegenCooling();
            aantalBTC -= prijsCooler;
            prijsCooler *= 1.15;
            aantalCooler++;
            TxtAantalCooling.Content = (int)aantalCooler;
            TxtPrijsCooling.Content = $"{Math.Ceiling(prijsCooler)} BTC";

            passiefClock += (1400 * aantalBonusCooler);

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
            ToevoegenSecurity();
            aantalBTC -= prijsSecurity;
            prijsSecurity *= 1.15;
            aantalSecurity++;
            TxtAantalSecurity.Content = (int)aantalSecurity;
            TxtPrijsSecurity.Content = $"{Math.Ceiling(prijsSecurity)} BTC";

            passiefSecurity += (1400 * aantalBonusSecurity);

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
        private void GridBonusBasic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusBasic;
            BonusStorePrijzen(aantalBonusBasic, 0);
            BonusPrijzenUpdaten();
            aantalBonusBasic *= 2;
            TxtAantalBonusBasic.Content = $"x{aantalBonusBasic}";
            BasicMultiplier.Text = $"x{aantalBonusBasic}";
            TxtPrijsBonusBasic.Content = $"Cost: {Math.Ceiling(prijsBonusBasic)}";

            passiefBasic *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusBasicTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusBasicTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusBasicTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusBasicTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusAdvanced_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusAdvanced;
            BonusStorePrijzen(aantalBonusAdvanced, 1);
            BonusPrijzenUpdaten();
            aantalBonusAdvanced *= 2;
            TxtAantalBonusAdvanced.Content = $"x{aantalBonusAdvanced}";
            AdvancedMultiplier.Text = $"x{aantalBonusAdvanced}";
            TxtPrijsBonusAdvanced.Content = $"{Math.Ceiling(prijsBonusAdvanced)} BTC";

            passiefAdvanced *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusAdvancedTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusAdvancedTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusAdvancedTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusAdvancedTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusMining_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusMiningRig;
            BonusStorePrijzen(aantalBonusMiningRig, 2);
            BonusPrijzenUpdaten();
            aantalBonusMiningRig *= 2;
            TxtAantalBonusMining.Content = $"x{aantalBonusMiningRig}";
            MiningMultiplier.Text = $"x{aantalBonusMiningRig}";
            TxtPrijsBonusMining.Content = $"{Math.Ceiling(prijsBonusMiningRig)} BTC";

            passiefMiningRig *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusMiningTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusMiningTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusMiningTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusMiningTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusQuantum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusQuantum;
            BonusStorePrijzen(aantalBonusQuantum, 3);
            BonusPrijzenUpdaten();
            aantalBonusQuantum *= 2;
            TxtAantalBonusQuantum.Content = $"x{aantalBonusQuantum}";
            QuantumMultiplier.Text = $"x{aantalBonusQuantum}";
            TxtPrijsBonusQuantum.Content = $"{Math.Ceiling(prijsBonusQuantum)} BTC";

            passiefQuantum *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusQuantumTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusQuantumTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusQuantumTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusQuantumTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusClock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusClock;
            BonusStorePrijzen(aantalBonusClock, 4);
            BonusPrijzenUpdaten();
            aantalBonusClock *= 2;
            TxtAantalBonusClock.Content = $"x{aantalBonusClock}";
            ClockMultiplier.Text = $"x{aantalBonusClock}";
            TxtPrijsBonusClock.Content = $"{Math.Ceiling(prijsBonusClock)} BTC";

            passiefClock *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusClockTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusClockTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusClockTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusClockTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusCooling_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusCooler;
            BonusStorePrijzen(aantalBonusCooler, 5);
            BonusPrijzenUpdaten();
            aantalBonusCooler *= 2;
            TxtAantalBonusCooling.Content = $"x{aantalBonusCooler}";
            CoolingMultiplier.Text = $"x{aantalBonusCooler}";
            TxtPrijsBonusCooling.Content = $"{Math.Ceiling(prijsBonusCooler)} BTC";

            passiefClock *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusCoolingTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusCoolingTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusCoolingTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusCoolingTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
        }

        private void GridBonusSecurity_MouseDown(object sender, MouseButtonEventArgs e)
        {
            aantalBTC -= prijsBonusSecurity;
            BonusStorePrijzen(aantalBonusSecurity, 6);
            BonusPrijzenUpdaten();
            aantalBonusSecurity *= 2;
            TxtAantalBonusSecurity.Content = $"x{aantalBonusSecurity}";
            SecurityMultiplier.Text = $"x{aantalBonusSecurity}";
            TxtPrijsBonusSecurity.Content = $"{Math.Ceiling(prijsBonusSecurity)} BTC";

            passiefSecurity *= 2;

            DoubleAnimation zoom = new DoubleAnimation
            {
                To = 1.05,
                Duration = TimeSpan.FromMilliseconds(100),
                AutoReverse = true,
            };
            BonusSecurityTransform.BeginAnimation(ScaleTransform.ScaleXProperty, null);
            BonusSecurityTransform.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            BonusSecurityTransform.BeginAnimation(ScaleTransform.ScaleXProperty, zoom);
            BonusSecurityTransform.BeginAnimation(ScaleTransform.ScaleYProperty, zoom);
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

        private void GridBonusBasic_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusBasic.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));
        }

        private void GridBonusBasic_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusBasic.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));
        }
        private void GridBonusAdvanced_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusAdvanced.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusAdvanced_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusAdvanced.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridBonusMining_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusMining.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusMining_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusMining.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridBonusQuantum_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusQuantum.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusQuantum_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusQuantum.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridBonusClock_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusClock.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusClock_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusClock.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridBonusCooling_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusCooling.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusCooling_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusCooling.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        private void GridBonusSecurity_MouseEnter(object sender, MouseEventArgs e)
        {
            GridBonusSecurity.Background = new SolidColorBrush(Color.FromArgb(219, 74, 88, 255));

        }

        private void GridBonusSecurity_MouseLeave(object sender, MouseEventArgs e)
        {
            GridBonusSecurity.Background = new SolidColorBrush(Color.FromArgb(0, 25, 105, 255));

        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowStoreItemsBasedOnBTC();
            TooltipLoad();
            Timer_ms_Load();
            WrapItems.Visibility = Visibility.Collapsed;
            Shop.Visibility = Visibility.Collapsed;
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
            sbClock.AppendLine("Every Cooling System will provide a passive income of 1400 BTC every second");
            TTcooler.Content = sbCooler.ToString();
            GridCooling.ToolTip = sbCooler;

            ToolTip TTsecurity = new ToolTip();
            StringBuilder sbSecurity = new StringBuilder();
            sbSecurity.AppendLine("Every Security Protocol will provide a passive income of 7800 BTC every second");
            TTsecurity.Content = sbSecurity.ToString();
            GridSecurity.ToolTip = sbSecurity;

        }

        #region Naam Miner Veranderen
        private void LblNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VraagNaam();

        }
        private void VraagNaam()
        {
            string invoer = Interaction.InputBox("What's the name of your Bitcoin Mining Emperium?");
            if (invoer.Trim() == "")
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
        #endregion

        #region Gekochte items toevoegen aan UI
        private void ToevoegenBasic()
        {
            Image basic = new Image();
            basic.Source = new BitmapImage(new Uri(@"Media/basicMiner.png", UriKind.RelativeOrAbsolute));
            basic.Height = 70;
            basic.Margin = new Thickness(2);

            wrapBasic.Children.Add(basic);
            wrapBasic.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }
        private void ToevoegenAdvanced()
        {
            Image advanced = new Image();
            advanced.Source = new BitmapImage(new Uri(@"Media/AdvancedMiner.png", UriKind.RelativeOrAbsolute));
            advanced.Height = 70;
            advanced.Margin = new Thickness(2);
            wrapAdvanced.Children.Add(advanced);
            wrapAdvanced.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;

        }

        private void ToevoegenMining()
        {
            Image mining = new Image();
            mining.Source = new BitmapImage(new Uri(@"Media/miningRig.png", UriKind.RelativeOrAbsolute));
            mining.Height = 70;
            mining.Margin = new Thickness(2);
            wrapMining.Children.Add(mining);
            wrapMining.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }
        private void ToevoegenQuantum()
        {
            Image quantum = new Image();
            quantum.Source = new BitmapImage(new Uri(@"Media/quantum.png", UriKind.RelativeOrAbsolute));
            quantum.Height = 70;
            quantum.Margin = new Thickness(2);
            wrapQuantum.Children.Add(quantum);
            wrapQuantum.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }
        private void ToevoegenClock()
        {
            Image Clock = new Image();
            Clock.Source = new BitmapImage(new Uri(@"Media/laptop.png", UriKind.RelativeOrAbsolute));
            Clock.Height = 70;
            Clock.Margin = new Thickness(2);
            wrapClock.Children.Add(Clock);
            wrapClock.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }
        private void ToevoegenCooling()
        {
            Image cooling = new Image();
            cooling.Source = new BitmapImage(new Uri(@"Media/cooling-fan.png", UriKind.RelativeOrAbsolute));
            cooling.Height = 70;
            cooling.Margin = new Thickness(2);
            wrapCooling.Children.Add(cooling);
            wrapCooling.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }
        private void ToevoegenSecurity()
        {
            Image security = new Image();
            security.Source = new BitmapImage(new Uri(@"Media/blockchain.png", UriKind.RelativeOrAbsolute));
            security.Height = 70;
            security.Margin = new Thickness(2);
            wrapSecurity.Children.Add(security);
            wrapSecurity.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;
        }







        #endregion

        #region Menu items
        private void BonusShop_Click(object sender, RoutedEventArgs e)
        {
            Shop.Visibility = Visibility.Hidden;
            BonusShopList.Visibility = Visibility.Visible;
        }

        private void ShopList_Click(object sender, RoutedEventArgs e)
        {
            BonusShopList.Visibility = Visibility.Hidden;
            Shop.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
