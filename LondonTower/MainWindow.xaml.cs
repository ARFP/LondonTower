using LondonTower.PageFolder;
using LondonTowerLibrary;
using LondonTowerLibrary.ViewModels;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LondonTower
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        TowerOfLondon tower;
        public MainWindow()
        {
            InitializeComponent();

            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            ShowsNavigationUI = false;

            //string path = Properties.Settings.Default.PathSecondeSave;


            string directorySave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestNeuro", "LondonTower2");
            var prov = Properties.Settings.Default.Providers;

            if (!Directory.Exists(directorySave))
            {
                Directory.CreateDirectory(directorySave);
            }


            
            Properties.Settings.Default.PathSecondeSave = directorySave;

            Properties.Settings.Default.Upgrade();
            //Properties.Settings.Default.Save();







        }
        public void InitTower(LondonTowerVM towerVM)
        {
            tower = FactoryLondonTower.GetTowerOfLondon(towerVM);
            
        }

        public void LoadingPage(string _nextPage, object sentback = null)
        {
            switch (_nextPage)
            {
                case "Identification":

                    this.Navigate(new Identification());
                    break;
                case "Demo":
                    this.Navigate(new FeedBack());
                    InitTower((LondonTowerVM)sentback);
                    //this.Navigate(new Demo(tower.GetNextTrial(), tower.VisualHelp));
                    break;
                case "Trial":
                    if (tower.HastNextTrial())
                    {
                        this.Navigate(new TrialPage(tower.GetNextTrial(), tower.VisualHelp));
                    }
                    else
                    {
                        this.LoadingPage("FeedBack");
                    }
                    break;
                case "FeedBack":
                    this.Navigate(new FeedBack());
                    break;
                case "TestResult":
                    this.tower.Fdback = sentback as string;
                    this.Navigate(new TestResult(tower));
                    break;

                default:
                    break;
            }

        }

        private void NavigationWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
