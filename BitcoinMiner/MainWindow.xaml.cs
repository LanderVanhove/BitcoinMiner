﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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

        #region Variabelen

        double aantalBTC = 1000000;
        double aantalBTCooit = 1000000;


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

        //hoeveel elke shopitem tot nu toe heeft opgebracht
        double opbrengstBasicOoit = 0;
        double opbrengstAdvancedOoit = 0;
        double opbrengstMiningRigOoit = 0;
        double opbrengstQuantumOoit = 0;
        double opbrengstClockOoit = 0;
        double opbrengstCoolerOoit = 0;
        double opbrengstSecurityOoit = 0;

        //aantal golden BTC munten verzameld zijn
        double aantalGoldBTC = 0;

        //Welke multiplier actief is (bonus shop)
        double aantalBonusBasic = 1;
        double aantalBonusAdvanced = 1;
        double aantalBonusMiningRig = 1;
        double aantalBonusQuantum = 1;
        double aantalBonusClock = 1;
        double aantalBonusCooler = 1;
        double aantalBonusSecurity = 1;

        //passief inkomen dat er bekomen wordt door de items
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

        double[] bonusPrijzen = new double[7] { 1500, 10000, 110000, 1200000, 13000000, 140000000, 2000000000 };

        // timers en stopwatches
        DispatcherTimer timer_ms = new DispatcherTimer();
        DispatcherTimer timer_minuut = new DispatcherTimer();

        private Stopwatch stopwatch = new Stopwatch();
        private double elapsedTime = 0;


        int tijdelijkBTC;


        #endregion

        //Berekent de prijs van de BonusStore items
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


        //Update de prijzen van de Bonusstore items
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


        //Hoofdtimer 10ms
        private void Timer_ms_Tick(object sender, EventArgs e)
        {
            CheckStoreAvailability();
            CheckMenuAvailability();
            ShowStoreItemsBasedOnBTC();
            QuestChecker();
            TooltipLoad();

            elapsedTime = stopwatch.ElapsedMilliseconds / 1000.0;
            stopwatch.Restart();

            //update totale passief BTC
            passiefTotaal = passiefBasic + passiefAdvanced + passiefMiningRig + passiefQuantum + passiefClock + passiefCooler + passiefSecurity;
            LblBTCpersec.Content = passiefTotaal.ToString();

            tijdelijkBTC = (int)aantalBTC;

            //update totale BTC adhv passief inkomen
            aantalBTC += (passiefTotaal * elapsedTime);
            aantalBTCooit += (passiefTotaal * elapsedTime);

            //update van wat elk item ooit verdient heeft
             opbrengstBasicOoit += (passiefBasic * elapsedTime);
             opbrengstAdvancedOoit += passiefAdvanced * elapsedTime;
             opbrengstMiningRigOoit += (passiefMiningRig * elapsedTime);
             opbrengstQuantumOoit += (passiefQuantum * elapsedTime);
             opbrengstClockOoit += (passiefClock * elapsedTime);
             opbrengstCoolerOoit += (passiefCooler * elapsedTime);
            opbrengstSecurityOoit += (passiefSecurity * elapsedTime);

            //update totale BTC in shop en titel met juiste cijferweergave
            WeergaveCijfer();

            if (tijdelijkBTC != (int)aantalBTC)
            {
                int verschil = (int)aantalBTC - tijdelijkBTC;
                verschil = Math.Min(verschil, 50);
                for (int i = 0; i < verschil; i++)
                {
                    FallingBTC();
                }
            }

        }
        

        //Zorgt ervoor dat het cijfer van aantal cookies de juiste weergave heeft (miljoenen, triljoenen,...)
        private void WeergaveCijfer()
        {
            double tempBTC;

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
                this.Title = $"You have {aantalBTC.ToString("n0", formatInfo)} BTC";


            }
            else
            {
                TxtAantalBTC.Content = $"{Math.Ceiling(aantalBTC)}";
                this.Title = $"You have {Math.Ceiling(aantalBTC)} BTC";
            }
        }


        //Alle klikevents die te maken hebben met de BTC munt
        #region BTC_Munt_klikEvents
        private void ImgBTC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImgBTC.Height = 230;
            aantalBTC += 1;
            aantalBTCooit += 1;

            //spawnt een kleine BTC img bij elke klik
            FallingBTC();
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


        //berekening om de shopitems te tonen op basis van ooit verzamelde BTC
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


        //controleert of Store Item enabled moet worden of niet
        private void CheckStoreAvailability()
        {
            UpdateGrid(GridBasic, BorderBasic, prijsBasic);
            UpdateGrid(GridAdvanced, BorderAdvanced, prijsAdvanced);
            UpdateGrid(GridMiningRig, BorderMiningRig, prijsMiningRig);
            UpdateGrid(GridQuantum, BorderQuantum, prijsQuantum);
            UpdateGrid(GridClock, BorderClock, prijsClock);
            UpdateGrid(GridCooling, BorderCooling, prijsCooler);
            UpdateGrid(GridSecurity, BorderSecurity, prijsSecurity);
            UpdateGrid(GridBonusBasic, BorderBonusBasic, prijsBonusBasic);
            UpdateGrid(GridBonusAdvanced, BorderBonusAdvanced, prijsBonusAdvanced);
            UpdateGrid(GridBonusMining, BorderBonusMining, prijsBonusMiningRig);
            UpdateGrid(GridBonusQuantum, BorderBonusQuantum, prijsBonusQuantum);
            UpdateGrid(GridBonusClock, BorderBonusClock, prijsBonusClock);
            UpdateGrid(GridBonusCooling, BorderBonusCooling, prijsBonusCooler);
            UpdateGrid(GridBonusSecurity, BorderBonusSecurity, prijsBonusSecurity);
        }
        private void UpdateGrid(Grid grid, Border border, double price)
        {
            if (aantalBTC >= price)
            {
                grid.Opacity = 1;
                grid.IsEnabled = true;
                border.BorderThickness = new Thickness(2);
            }
            else
            {
                grid.Opacity = 0.3;
                grid.IsEnabled = false;
                border.BorderThickness = new Thickness(0);
            }
        }


        //controleert of BonusStore zichtbaar moet worden in het menu
        private void CheckMenuAvailability()
        {
            if (aantalBasic >= 1 || aantalAdvanced >= 1 || aantalMiningRig >= 1 || aantalQuantum >= 1 || aantalClock >= 1 || aantalCooler >= 1 || aantalSecurity >= 1)
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

            passiefSecurity += (7800 * aantalBonusSecurity);

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
            TxtPrijsBonusAdvanced.Content = $"Cost: {Math.Ceiling(prijsBonusAdvanced)}";

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
            TxtPrijsBonusMining.Content = $"Cost: {Math.Ceiling(prijsBonusMiningRig)}";

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
            TxtPrijsBonusQuantum.Content = $"Cost: {Math.Ceiling(prijsBonusQuantum)}";

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
            TxtPrijsBonusClock.Content = $"Cost: {Math.Ceiling(prijsBonusClock)}";

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
            TxtPrijsBonusCooling.Content = $"Cost: {Math.Ceiling(prijsBonusCooler)}";
            

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
            TxtPrijsBonusSecurity.Content = $"Cost: {Math.Ceiling(prijsBonusSecurity)}";

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

        #region Mouse events to change BG on items
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

        #region Naam Miner Veranderen
        private void LblNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VraagNaam();

        }
        //De naam kan niet leeg of enkel spaties bevatten, naam moet ook tussen 3 en 12 karakters langs zijn
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
            //aanmaken van image
            Image basic = new Image();
            basic.Source = new BitmapImage(new Uri(@"Media/basicMiner.png", UriKind.RelativeOrAbsolute));
            basic.Height = 70;
            basic.Margin = new Thickness(2);

            //image toevoegen aan het juiste wrappanel
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

        #region Menu items klikevents
        private void BonusShop_Click(object sender, RoutedEventArgs e)
        {
            Shop.Visibility = Visibility.Hidden;
            QuestGrid.Visibility = Visibility.Hidden;
            StatsGrid.Visibility = Visibility.Hidden;
            BonusShopList.Visibility = Visibility.Visible;
            WrapItems.Visibility= Visibility.Visible;
        }

        private void ShopList_Click(object sender, RoutedEventArgs e)
        {
            BonusShopList.Visibility = Visibility.Hidden;
            QuestGrid.Visibility = Visibility.Hidden;
            StatsGrid.Visibility = Visibility.Hidden;
            Shop.Visibility = Visibility.Visible;
            WrapItems.Visibility = Visibility.Visible;

        }
        private void Quests_Click(object sender, RoutedEventArgs e)
        {
            Shop.Visibility = Visibility.Hidden;
            BonusShopList.Visibility = Visibility.Hidden;
            WrapItems.Visibility = Visibility.Hidden;
            StatsGrid.Visibility = Visibility.Hidden;
            QuestGrid.Visibility = Visibility.Visible;
        }

        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            Shop.Visibility = Visibility.Hidden;
            BonusShopList.Visibility = Visibility.Hidden;
            WrapItems.Visibility = Visibility.Hidden;
            QuestGrid.Visibility = Visibility.Hidden;
            StatsGrid.Visibility = Visibility.Visible;
        }
        #endregion


        Random rand1 = new Random();
        Random rand2 = new Random();

        #region Golden BTC spawn & klik event

        //Aparte timer voor Golden Cookie
        private void Timer_minuut_Tick(object sender, EventArgs e)
        {
            RandomBTC();
        }
        private void RandomBTC()
        {
            int nummer = rand2.Next(1, 11);
            if (nummer == 1 || nummer == 2 || nummer == 3)
            {
                SpawnGoldenBTC();
            }
        }
        private void SpawnGoldenBTC()
        {

            CanvasGoldenBTC.Visibility = Visibility.Visible;

            //aanmaken image
            Image GoldBTC = new Image();
            GoldBTC.Source = new BitmapImage(new Uri("Media/GoldBtc.png", UriKind.Relative));
            GoldBTC.Height = ImgBTC.Height;

            //positioneer image op canvas
            Canvas.SetTop(GoldBTC, -100);
            int randomLeft = rand1.Next(0, 500);
            Canvas.SetLeft(GoldBTC, randomLeft);
            CanvasGoldenBTC.Children.Add(GoldBTC);

            //klikevent
            GoldBTC.MouseDown += GoldBTC_MouseDown;

            //animation
            AnimateGoldenBTC(GoldBTC);
        }
        private void GoldBTC_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Image clickedImage = sender as Image;
            aantalBTC += (passiefTotaal * 900);
            aantalBTCooit += (passiefTotaal * 900);
            aantalGoldBTC++;
            CanvasGoldenBTC.Children.Remove(clickedImage);
            CanvasGoldenBTC.Visibility = Visibility.Collapsed;
        }
        private async void AnimateGoldenBTC(Image img)
        {
            while (Canvas.GetTop(img) < 1000)
            {

                Canvas.SetTop(img, Canvas.GetTop(img) + 5);
                await Task.Delay(10);
                if (Canvas.GetTop(img) > 800)
                {
                    CanvasGoldenBTC.Children.Remove(img);
                    CanvasGoldenBTC.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Kleine BTC munt spawn bij klik/passief inkomen

        int vallendeBTC = 0;
        DispatcherTimer fallingTimer = new DispatcherTimer();
        private void StartFallingTimer()
        {
            fallingTimer.Interval = TimeSpan.FromMilliseconds(100);
            fallingTimer.Tick += (sender, e) => FallingBTC();
            fallingTimer.Start();
        }

        //spawnt een vallende BTC munt op random positie met een max van 50 stuks
        private void FallingBTC()
        {
            if (vallendeBTC < 50 && passiefTotaal > 0)
            {
                //aanmaken van image
                Image smallBTC = new Image();
                smallBTC.Source = ImgBTC.Source;
                smallBTC.Height = ImgBTC.ActualHeight / 10;
                smallBTC.Opacity = 0.5;

                //positioneer image
                Canvas.SetTop(smallBTC, -50);
                int randomLeft = rand1.Next(0, (int)FallingImages.ActualWidth-50);
                Canvas.SetLeft(smallBTC, randomLeft);

                vallendeBTC++;
                FallingImages.Children.Add(smallBTC);

                //creeert een delay op de animation van de verschillende vallende IMG's
                double delay = vallendeBTC *0.1;
                DispatcherTimer delayTimer = new DispatcherTimer();
                delayTimer.Interval = TimeSpan.FromSeconds(delay);
                delayTimer.Tick += (sender, e) =>
                {
                    AnimateSmallImg(smallBTC);
                    delayTimer.Stop();
                };
                delayTimer.Start();
            }
        }

        //animeert die vallende BTC munt
        private void AnimateSmallImg(Image img)
        {
            TranslateTransform translateTransform = new TranslateTransform();
            img.RenderTransform = translateTransform;

            DoubleAnimation animation = new DoubleAnimation
            {
                To = FallingImages.ActualHeight + 50,
                Duration = TimeSpan.FromSeconds(4)
            };

            animation.Completed += (s, args) =>
            {
                FallingImages.Children.Remove(img);
                vallendeBTC--;

            };

            translateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
            
            double adjustedInterval = Math.Max(Math.Sqrt(20/passiefTotaal),0.1);

            if (fallingTimer != null)
            {
                fallingTimer.Interval = TimeSpan.FromSeconds(adjustedInterval);
            }
            
        }
        #endregion

        #region Quest variabelen
        bool questBasic = false;
        bool questAdvanced = false;
        bool questMining = false;
        bool questQuantum = false;
        bool questClock = false;
        bool questCooler = false;
        bool questSecurity = false;

        bool questBasicBonus = false;
        bool questAdvancedBonus = false;
        bool questMiningBonus = false;
        bool questQuantumBonus = false;
        bool questClockBonus = false;
        bool questCoolerBonus = false;
        bool questSecurityBonus = false;

        bool questAantalBTC1000 = false;
        bool questAantalBTC100000 = false;
        bool questAantalBTC1000000 = false;
        bool questAantalBTC1000000000 = false;

        bool questAantalBTCperSeconde20 = false;
        bool questAantalBTCperSeconde100 = false;
        bool questAantalBTCperSeconde1000 = false;
        bool questAantalBTCperSeconde50000 = false;

        bool questGoldBTC1 = false;
        bool questGoldBTC5 = false;
        bool questGoldBTC10 = false;
        #endregion
        StringBuilder questBuilder = new StringBuilder();

        //Checkt of er voldaan is aan een quest en toont deze daarna in het questoverzicht
        private void QuestChecker()
        {

            #region Quests eerste shopitem
            if (aantalBasic == 1 && !questBasic)
            {
                MessageBox.Show("Je hebt je eerste Basic Miner gekocht! Nu zullen de machines voor jou werken.");
                questBasic = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Basic Miner' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();

            }
            if (aantalAdvanced == 1 && !questAdvanced)
            {
                MessageBox.Show("Je hebt je eerste Advanced Miner gekocht! Een verbeterde versie van de Basic Miner waardoor je nog sneller BTC kan minen!");
                questAdvanced = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Advanced Miner' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalMiningRig == 1 && !questMining)
            {
                MessageBox.Show("Je hebt je eerste Mining Rig gekocht! Nu worden de Basic & Advanced miners geconnecteerd op een grid zodat er nog efficiënter gemined kan worden.\r\n");
                questMining = true;
                questBuilder.AppendLine("Je hebt je eerste 'Mining Rig' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalQuantum == 1 && !questQuantum)
            {
                MessageBox.Show("Je hebt je eerste Quantum Miner gekocht! De beste miner op de markt verkrijgbaar, nu in jouw bezit.");
                questQuantum = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Quantum Miner' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalClock == 1 && !questClock)
            {
                MessageBox.Show("Je hebt je eerste Overclocking Module gekocht! Alle miners kunnen nu meer power aan, wat betekent: MEER BITCOIN!");
                questClock = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Overclocking Module' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalCooler == 1 && !questCooler)
            {
                MessageBox.Show("Je hebt je eerste Cooling System gekocht! Dat werd tijd... Alle miners worden nu gekoeld met een state-of-the-art cooling system waardoor ze moeiteloos 24/7 kunnen minen.");
                questCooler = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Cooling System' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalSecurity == 1 && !questSecurity)
            {
                MessageBox.Show("Je hebt je eerste Security Protocol gekocht! Dankzij je toegevoegde security systeem hoeft het mining systeem geen CPU-power meer te verkwisten aan het tegenhouden van hackers, FULL POWER MINING!");
                questSecurity = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt je eerste 'Security Protocol' gekocht!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            #endregion
            #region Quests eerste bonus shopitem
            if (aantalBonusBasic == 2 && !questBasicBonus)
            {
                MessageBox.Show("Gefeliciteerd! Door deze aankoop worden alle Basic Miners nu dubbel zo waardevol.");
                questBasicBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Basic Miners'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusAdvanced == 2 && !questAdvancedBonus)
            {
                MessageBox.Show("Innovatie begint hier! Je hebt zojuist de sleutel tot geavanceerde beloningen ontgrendeld. Alle Advanced Miners zijn nu twee keer zo winstgevend.");
                questAdvancedBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Advanced Miners'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusMiningRig == 2 && !questMiningBonus)
            {
                MessageBox.Show("Je eerste aankoop van een Mining Bonus opent de poort naar een stroom van extra voordelen. Al je Mining Rigs produceren nu dubbele opbrengsten.");
                questMiningBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Mining Rigs'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusQuantum == 2 && !questQuantumBonus)
            {
                MessageBox.Show("Met je eerste Quantum Bonus-aankoop ontketen je nieuwe dimensies van beloningen. Alle Quantum Miners zijn nu dubbel zo waardevol.");
                questQuantumBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Quantum Miners'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusClock == 2 && !questClockBonus)
            {
                MessageBox.Show("Tijd is geld! Door je eerste Clock Bonus aan te schaffen, heb je de sleutel tot temporele rijkdom in handen. Alle Overclocking Modules verdubbelen in opbrengst.");
                questClockBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Overclocking Modules'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusCooler == 2 && !questCoolerBonus)
            {
                MessageBox.Show("Met je eerste Cooler Bonus geniet je van koelere winsten en extra's. Alle Cooling Systems brengen nu dubbel zoveel op.");
                questCoolerBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Cooling Systems'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBonusSecurity == 2 && !questSecurityBonus)
            {
                MessageBox.Show("Door je eerste Security Bonus te kopen, versterk je de verdedigingslinie van je crypto-rijk. Alle Security Protocols genereren nu dubbele opbrengsten.");
                questSecurityBonus = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een verdubbelaar gekocht voor de 'Security Protocols'");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            #endregion
            #region Quests aantal BTC
            if (aantalBTC >= 1000 && !questAantalBTC1000)
            {
                MessageBox.Show("1.000 BTC behaald! Je Bitcoin-avontuur begint vruchten af te werpen: 1.000 BTC!");
                questAantalBTC1000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt de kaap van 1.000 BTC bereikt!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBTC >= 100000 && !questAantalBTC100000)
            {
                MessageBox.Show("100.000 BTC behaald! Jij bent de koning van de blockchain met 100.000 BTC op zak.");
                questAantalBTC100000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt de kaap van 100.000 BTC bereikt!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBTC >= 1000000 && !questAantalBTC1000000)
            {
                MessageBox.Show("1.000.000 BTC behaald! Een miljoen digitale schatten veroverd - jij bent een Bitcoin-legende.");
                questAantalBTC1000000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt de kaap van 1 miljoen BTC bereikt!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalBTC >= 1000000000 && !questAantalBTC1000000000)
            {
                MessageBox.Show("1.000.000.000 BTC behaald! Je hebt de grens doorbroken: een miljard BTC, een digitale titan.");
                questAantalBTC1000000000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt de kaap van 1 miljard BTC bereikt!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            #endregion
            #region Quests aantal BTC per seconde
            if (passiefTotaal >= 20 && !questAantalBTCperSeconde20)
            {
                MessageBox.Show("20 BTC per seconde! Je Bitcoin-mijn draait als een goed geoliede machine: 20 BTC per seconde!");
                questAantalBTCperSeconde20 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een inkomen van 20 BTC/s behaald!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (passiefTotaal >= 100 && !questAantalBTCperSeconde100)
            {
                MessageBox.Show("100 BTC per seconde! In de snelheid van het digitale tijdperk, verzamel je moeiteloos 100 BTC per seconde, proficiat!");
                questAantalBTCperSeconde100 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een inkomen van 100 BTC/s behaald!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (passiefTotaal >= 1000 && !questAantalBTCperSeconde1000)
            {
                MessageBox.Show("1.000 BTC per seconde! Je bent als een digitale tovenaar, creërend en verzamelend met een snelheid van 1000 BTC per seconde.");
                questAantalBTCperSeconde1000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een inkomen van 1.000 BTC/s behaald!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (passiefTotaal >= 50000 && !questAantalBTCperSeconde50000)
            {
                MessageBox.Show("50000 BTC per seconde! Met een stormachtige snelheid van 50.000 BTC per seconde, ben je de koning van de Bitcoin-jungle.");
                questAantalBTCperSeconde50000 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt een inkomen van 50.000 BTC/s behaald!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            #endregion
            #region Quests Gouden BTC
            if (aantalGoldBTC == 1 && !questGoldBTC1)
            {
                MessageBox.Show($"Mooi gespot! Deze Golden BTC-munt levert je maar liefst {passiefTotaal * 900} BTC op!");
                questGoldBTC1 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je eerste gouden munt is gevangen!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalGoldBTC == 5 && !questGoldBTC5)
            {
                MessageBox.Show($"Dit is al je 5de gouden BTC-munt! Deze munt levert je {passiefTotaal * 900} BTC op!");
                questGoldBTC5 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt in totaal al 5 gouden munten gevangen!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            if (aantalGoldBTC == 10 && !questGoldBTC10)
            {
                MessageBox.Show($"Maar liefst al 10 gouden BTC-munten gevangen, wow! Deze munt levert je {passiefTotaal * 900} BTC op!");
                questGoldBTC10 = true;
                Quests.Visibility = Visibility.Visible;
                questBuilder.AppendLine("Je hebt in totaal al 10 gouden munten gevangen!");
                QuestHistoriek.Content = questBuilder.ToString();
            }
            #endregion
        }



        private void DpMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            masterGrid.Focus();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ShowStoreItemsBasedOnBTC();
            TooltipLoad();
            Timer_ms_Load();
            Timer_minuut_Load();
            StartFallingTimer();
            WrapItems.Visibility = Visibility.Collapsed;
            Shop.Visibility = Visibility.Collapsed;
        }

        //Weergeeft tooltip op de shop items
        private void TooltipLoad()
        {
            ToolTip TTbasic = new ToolTip();
            StringBuilder sbBasic = new StringBuilder();
            sbBasic.AppendLine($"Every basic miner will provide a passive income of {passiefBasic/aantalBasic} BTC every second");
            sbBasic.AppendLine($"Your basic miners currently provide {passiefBasic} BTC per second together");
            sbBasic.AppendLine($"Your basic miners currently have a multiplier of {aantalBonusBasic}");
            sbBasic.AppendLine($"In total, your basic miners have earned {opbrengstBasicOoit} BTC so far");

            TTbasic.Content = sbBasic.ToString();
            GridBasic.ToolTip = TTbasic;

            ToolTip TTadvanced = new ToolTip();
            StringBuilder sbAdvanced = new StringBuilder();
            sbAdvanced.AppendLine($"Every advanced miner will provide a passive income of {passiefAdvanced / aantalAdvanced} BTC every second");
            sbAdvanced.AppendLine($"Your advanced miners currently provide {passiefAdvanced} BTC per second together");
            sbAdvanced.AppendLine($"Your advanced miners currently have a multiplier of {aantalBonusAdvanced}");
            sbAdvanced.AppendLine($"In total, your advanced miners have earned {opbrengstAdvancedOoit} BTC so far"); 
            TTadvanced.Content = sbAdvanced.ToString();
            GridAdvanced.ToolTip = TTadvanced;

            ToolTip TTminingRig = new ToolTip();
            StringBuilder sbMiningRig = new StringBuilder();
            sbMiningRig.AppendLine($"Every Mining Rig will provide a passive income of {passiefMiningRig / aantalMiningRig} BTC every second");
            sbMiningRig.AppendLine($"Your Mining Rigs currently provide {passiefMiningRig} BTC per second together");
            sbMiningRig.AppendLine($"Your Mining Rigs currently have a multiplier of {aantalBonusMiningRig}");
            sbMiningRig.AppendLine($"In total, your Mining Rigs have earned {opbrengstMiningRigOoit} BTC so far"); 
            TTminingRig.Content = sbMiningRig.ToString();
            GridMiningRig.ToolTip = sbMiningRig;

            ToolTip TTquantum = new ToolTip();
            StringBuilder sbQuantum = new StringBuilder();
            sbQuantum.AppendLine($"Every Quantum Miner will provide a passive income of {passiefQuantum / aantalQuantum} BTC every second");
            sbQuantum.AppendLine($"Your Quantum Miners currently provide {passiefQuantum} BTC per second together");
            sbQuantum.AppendLine($"Your Quantum Miners currently have a multiplier of {aantalBonusQuantum}");
            sbQuantum.AppendLine($"In total, your Quantum Miners have earned {opbrengstQuantumOoit} BTC so far"); 
            TTquantum.Content = sbQuantum.ToString();
            GridQuantum.ToolTip = sbQuantum;

            ToolTip TTclock = new ToolTip();
            StringBuilder sbClock = new StringBuilder();
            sbClock.AppendLine($"Every Overclocking Module will provide a passive income of {passiefClock / aantalClock} BTC every second");
            sbClock.AppendLine($"Your Overclocking Modules currently provide {passiefClock} BTC per second together");
            sbClock.AppendLine($"Your Overclocking Modules currently have a multiplier of {aantalBonusClock}");
            sbClock.AppendLine($"In total, your Overclocking Modules have earned {opbrengstClockOoit} BTC so far"); 
            TTclock.Content = sbClock.ToString();
            GridClock.ToolTip = sbClock;

            ToolTip TTcooler = new ToolTip();
            StringBuilder sbCooler = new StringBuilder();
            sbCooler.AppendLine($"Every Cooling System will provide a passive income of {passiefCooler / aantalCooler} BTC every second");
            sbCooler.AppendLine($"Your Cooling Systems currently provide {passiefCooler} BTC per second together");
            sbCooler.AppendLine($"Your Cooling Systems currently have a multiplier of {aantalBonusCooler}");
            sbCooler.AppendLine($"In total, your Cooling Systems have earned {opbrengstCoolerOoit} BTC so far"); 
            TTcooler.Content = sbCooler.ToString();
            GridCooling.ToolTip = sbCooler;

            ToolTip TTsecurity = new ToolTip();
            StringBuilder sbSecurity = new StringBuilder();
            sbSecurity.AppendLine($"Every Security Protocol will provide a passive income of {passiefSecurity / aantalSecurity} BTC every second");
            sbSecurity.AppendLine($"Your Security Protocols currently provide {passiefSecurity} BTC per second together");
            sbSecurity.AppendLine($"Your Security Protocols currently have a multiplier of {aantalBonusSecurity}");
            sbSecurity.AppendLine($"In total, your Security Protocols have earned {opbrengstSecurityOoit} BTC so far"); 
            TTsecurity.Content = sbSecurity.ToString();
            GridSecurity.ToolTip = sbSecurity;

        }

        //Timer loaders
        private void Timer_ms_Load()
        {
            timer_ms.Interval = TimeSpan.FromMilliseconds(10);
            timer_ms.Tick += Timer_ms_Tick;
            timer_ms.Start();
        }
        private void Timer_minuut_Load()
        {
            timer_minuut.Interval = TimeSpan.FromMinutes(1);
            timer_minuut.Tick += Timer_minuut_Tick;
            timer_minuut.Start();
        }


    }
}
