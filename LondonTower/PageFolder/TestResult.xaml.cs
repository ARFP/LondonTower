using ClosedXML.Excel;
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
        private double totalTimeTower;

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
        public double TotalTimeTower
        {
            get => totalTimeTower;
            set
            {
                totalTimeTower = value;
                OnPropertyChanged(nameof(TotalTimeTower));
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
            dataGrid1.ItemsSource = tower.TrialList.Where(x => x.TrialNumber != 0);
            ButNextPage.Click += ButNextPage_OnCick;
            ButQuit.Click += ButLeaveApp_OnClick;

            string datestr = tower.DateAndTime.Hour + "." + tower.DateAndTime.Month + "." + tower.DateAndTime.Year + " " + tower.DateAndTime.Hour + "h" + tower.DateAndTime.Minute;
            string path = (tower.Personn.LastName + " " + tower.Personn.FirstName + " Lv" + tower.Level + " " + datestr + ".xlsx");


            DataCalcul();
            SaveTest(path);

        }

        private void ButNextPage_OnCick(object sneder, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.LoadingPage("Identification");
        }
        private void ButLeaveApp_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.Close();
        }

        private void SaveTest(string _path)
        {
            var workbook = new XLWorkbook("./Config/Template.xlsx");
            var worksheet = workbook.Worksheet(1);

            worksheet.Cell("C5").Value = tower.Personn.LastName;
            worksheet.Cell("C6").Value = tower.Personn.FirstName;
            worksheet.Cell("C7").Value = tower.Personn.DayofBirth;
            worksheet.Cell("C8").Value = tower.Personn.Genre;
            worksheet.Cell("C9").Value = tower.DateAndTime;
            worksheet.Cell("C10").Value = tower.Fdback;

            int row = 16;

            foreach (Trial _t in tower.TrialList)
            {
                if (_t.TrialNumber != 0)
                {
                    worksheet.Cell("C" + row).Value = _t.TryMoveMade;
                    worksheet.Cell("D" + row).Value = _t.MinimalMoveExpect;
                    worksheet.Cell("E" + row).Value = _t.TimeToResolve;
                    row++;
                }
            }

            worksheet.Cell("D31").Value = MinimalMove + " coups";
            worksheet.Cell("D32").Value = ExcesMove + " coups";
            worksheet.Cell("D33").Value = (UpperStrat + " %");
            worksheet.Cell("D34").Value = TotalTime + " secondes";
            worksheet.Cell("D35").Value = MoyenneTime + " secondes";
            worksheet.Cell("D36").Value = totalTimeTower + " secondes";


            //workbook.SaveAs(_path);

            //string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestLaurence", _path);


            //string directorySave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestLaurence");
            //Directory.CreateDirectory(directo);

            //if (!Directory.Exists(directorySave))
            //{
            //    Directory.CreateDirectory(directorySave);

            //}

            
            string p = @"P:\TestNeuro";
            string save = Path.Combine(p, "LondonTower", _path);
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);

            }

            workbook.SaveAs(save);

        }

        private void DataCalcul()
        {
            IEnumerable<Trial> listtest = tower.TrialList.Where(x => x.TrialNumber != 0);

            MinimalMove = listtest.Sum(x => x.MinimalMoveExpect);

            ExcesMove = listtest.Sum(x => x.TryMoveMade) - MinimalMove;

            UpperStrat = Math.Floor(((listtest.Sum(x => x.TryMoveMade) / MinimalMove) - 1) * 100);

            TotalTime = listtest.Sum(x => x.TimeToResolve);

            MoyenneTime = Math.Floor((double)(TotalTime / 10));

            TotalTimeTower = Math.Floor(( DateTime.Now-tower.DateAndTime).TotalSeconds);
        }


    }
}
