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
        /// <summary>
        /// Point d'entrée du programmes 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            ShowsNavigationUI = false;
        }

        /// <summary>
        /// Creation de TowerOfLondon avec la pattern Factory 
        /// </summary>
        /// <param name="towerVM">View Model London Tower contenant les paramètre du test LondonTower </param>
        public void InitTower(LondonTowerVM towerVM)
        {
            tower = FactoryLondonTower.GetTowerOfLondon(towerVM);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_nextPage"></param>
        /// <param name="sentback"></param>
        public void LoadingPage(string _nextPage, object sentback = null)
        {
            switch (_nextPage)
            {
                case "Identification":

                    this.Navigate(new Identification());
                    break;
                case "Demo":
                    //this.Navigate(new FeedBack());
                    InitTower((LondonTowerVM)sentback);
                    this.Navigate(new Demo(tower.GetNextTrial(), tower.VisualHelp));
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
