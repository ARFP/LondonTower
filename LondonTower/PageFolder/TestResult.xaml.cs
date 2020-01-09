using ClosedXML.Excel;
using LondonTowerLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour TestResult.xaml
    /// </summary>
    public partial class TestResult : Page, INotifyPropertyChanged
    {
        private LondonTowerLibrary.LondonTower tower;


        private double minimalMove;
        private double excesMove;
        private double upperStrat;
        private int totalTime;
        private double moyenneTime;
        private double totalTimeTower;

        /// <summary>
        /// Accesseur de l'instance de LondonTower
        /// </summary>
        public LondonTowerLibrary.LondonTower Tower { get => tower; set => tower = value; }

        /// <summary>
        /// Accesseur de minimalMove, Double representant la somme des coups minimum de chaque Trial
        /// </summary>
        public double MinimalMove
        {
            get => minimalMove;
            set
            {
                minimalMove = value;
                OnPropertyChanged(nameof(MinimalMove));
            }
        }

        /// <summary>
        /// Accesseur de excesMove, Double representant la somme du nombre de coup en plus par rapport au mouvement minimal prévu par Trial
        /// </summary>
        public double ExcesMove
        {
            get => excesMove; set
            {
                excesMove = value;
                OnPropertyChanged(nameof(ExcesMove));
            }
        }

        /// <summary>
        /// Accesseur de upperStrat, Double representant en pourcentage le nombre de coup en exces
        /// </summary>
        public double UpperStrat
        {
            get => upperStrat; set
            {
                upperStrat = value;
                OnPropertyChanged(nameof(UpperStrat));
            }
        }

        /// <summary>
        /// Accesseur totalTime, Entier representant le temps en seconde 
        /// la somme des temps de resolution des Trials, a partir du 1er mouvement jusqu a la resolution
        /// </summary>
        public int TotalTime
        {
            get => totalTime; set
            {
                totalTime = value;
                OnPropertyChanged(nameof(TotalTime));
            }
        }

        /// <summary>
        /// Accesseur de moyenneTime, Double représentant le temps moyen passé par Trial
        /// </summary>
        public double MoyenneTime
        {
            get => moyenneTime; set
            {
                moyenneTime = value;
                OnPropertyChanged(nameof(MoyenneTime));
            }
        }

        /// <summary>
        /// Accesseur de totalTimeTower, Double représentant la durée total du test London Tower, du lancement de la page 
        /// démonstration jusqu'a la validation du ressenti apres le test
        /// </summary>
        public double TotalTimeTower
        {
            get => totalTimeTower;
            set
            {
                totalTimeTower = value;
                OnPropertyChanged(nameof(TotalTimeTower));
            }
        }

        /// <summary>
        /// Implimentation de INotifyPropertyChanged, Evenement declenché lors d'un changement de propriété
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Méthode appelé lorsque l'evenement <c>PropertyChanged</c> est declénché lors d'un changement de propriété
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Constructeur paramétré initialisant l'ItemsSource de la datagrid, 
        /// appelant la fonction pour le formatage du nom fichier de sauvegarde, 
        /// appelant la fonction de calcule des résultat
        /// puis sauvegarde des resultats en async dans une task
        /// </summary>
        /// <param name="_tower"></param>
        public TestResult(LondonTowerLibrary.LondonTower _tower)
        {
            tower = _tower;
            this.DataContext = this;
            InitializeComponent();
            dataGrid1.ItemsSource = tower.TrialList.Where(x => x.TrialNumber != 0);
            ButNextPage.Click += ButNextPage_OnCick;
            ButQuit.Click += ButLeaveApp_OnClick;

            string path = FormatageNomFichier();

            DataCalcul();
            Task t = new Task( ()=> {
                SaveTest(path);
            });
            t.Start();

        }

        /// <summary>
        /// Gestion d'evenement au Click sur le bouton Home
        /// </summary>
        /// <param name="sender">Object declencheur de l'evenement</param>
        /// <param name="e">Evenement routé</param>
        private void ButNextPage_OnCick(object sneder, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.LoadingPage("Identification");
        }

        /// <summary>
        /// Gestion d'evenement au Click sur le bouton Quitter
        /// </summary>
        /// <param name="sender">Object declencheur de l'evenement</param>
        /// <param name="e">Evenement routé</param>
        private void ButLeaveApp_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.Close();
        }


        /// <summary>
        /// Fonction de sauvegarde des résultats en fichier EXCEL avec l'extension XLSX.
        /// Chargement d'un template, remplissage des cellules avec les résultats et enregistrement du fichier
        /// 3 Tentatives de sauvegardes différente, 
        /// 1ere en prenant le path de Properties.Settings.Default.PathFirstSave
        /// 2eme en prenant le path de Properties.Settings.Default.PathSecondeSave
        /// 3eme sauvegarde sur le bureau avec affichage message d'erreur
        /// </summary>
        /// <param name="_path">Chemin de sauvegarde du fichier xlsx</param>
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

            string pathSave;

            string FirstSave = Path.Combine(Properties.Settings.Default.PathFirstSave, "LondonTower");
            string SecondSave = Path.Combine(Properties.Settings.Default.PathSecondeSave, "LondonTower");

            try
            {
                if (!Directory.Exists(FirstSave))
                {
                    Directory.CreateDirectory(FirstSave);
                }
                pathSave = FirstSave;
                
            }
            catch (Exception e)
            {
                try
                {
                    if (!Directory.Exists(SecondSave))
                    {
                        Directory.CreateDirectory(SecondSave);
                    }
                    pathSave = SecondSave;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERREUR : Problème d'accès aux repertoire de sauvegarde\nVeuillez contacter votre " +
                        "Administrateur\n\n Sauvegarde du fichier sur le bureau");
                    pathSave = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

            }
            workbook.SaveAs(Path.Combine(pathSave,_path));
        }


        /// <summary>
        /// Fonction de récuperation et de calcul des résultats
        /// </summary>
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

        /// <summary>
        /// Fonction de formatage pour le nom du fichier XLSX sauvegarder
        /// </summary>
        /// <returns>String représentant le nom du fichier pour la sauvegarde</returns>
        private string FormatageNomFichier()
        {
            string datestr = tower.DateAndTime.Hour + "." + tower.DateAndTime.Month + "." + tower.DateAndTime.Year + " " + tower.DateAndTime.Hour + "h" + tower.DateAndTime.Minute;
            return (tower.Personn.LastName + " " + tower.Personn.FirstName + " Lv" + tower.Level + " " + datestr + ".xlsx");
        }


    }
}
