using LondonTowerLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour TestResult.xaml
    /// </summary>
    public partial class TestResult : Page, INotifyPropertyChanged
    {
        private TowerOfLondon tower;


        private double minimalMove;
        private double excesMove;
        private double upperStrat;
        private int totalTime;
        private double moyenneTime;
        private int TotalTimeTower;

        public TowerOfLondon Tower { get => tower; set => tower = value; }

        public double MinimalMove
        {
            get => minimalMove;
            set
            {
                minimalMove = value;
                OnPropertyChanged(nameof(MinimalMove));
            }
        }
        public double ExcesMove
        {
            get => excesMove; set
            {
                excesMove = value;
                OnPropertyChanged(nameof(ExcesMove));
            }
        }
        public double UpperStrat
        {
            get => upperStrat; set
            {
                upperStrat = value;
                OnPropertyChanged(nameof(UpperStrat));
            }
        }
        public int TotalTime
        {
            get => totalTime; set
            {
                totalTime = value;
                OnPropertyChanged(nameof(TotalTime));
            }
        }
        public double MoyenneTime
        {
            get => moyenneTime; set
            {
                moyenneTime = value;
                OnPropertyChanged(nameof(MoyenneTime));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TestResult(TowerOfLondon _tower)
        {
            tower = _tower;
            this.DataContext = this;
            InitializeComponent();
            dataGrid1.ItemsSource = tower.TrialList.Where(x=>x.TrialNumber !=0);
            ButNextPage.Click += ButNextPage_OnCick;
            SaveTest();
            DataCalcul();

        }

        private void ButNextPage_OnCick(object sneder, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.LoadingPage("Identification");
        }

        private void SaveTest()
        {
            string path = "./Tower" + tower.Level + ".txt";
            string str = JsonConvert.SerializeObject(tower, Formatting.Indented);
            File.WriteAllText(path, str);
        }

        private void DataCalcul()
        {
            IEnumerable<Trial> listtest = tower.TrialList.Where(x => x.TrialNumber != 0);

            MinimalMove = listtest.Sum(x => x.MinimalMoveExpect);

            ExcesMove = listtest.Sum(x => x.TryMoveMade)-MinimalMove;

            UpperStrat = Math.Floor(((listtest.Sum(x => x.TryMoveMade) / MinimalMove) - 1) * 100);
            
            TotalTime = listtest.Sum(x => x.TimeToResolve);

            MoyenneTime = Math.Floor((double)(TotalTime / 10));
        }


    }
}
