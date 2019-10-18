using LondonTower.PageFolder;
using LondonTowerLibrary;
using System;
using System.Windows;
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
            ShowsNavigationUI = false;

             //tower = FactoryLondonTower.InitialiseTowerOfLondon();

        }
        public void InitTower(int _level)
        {
            tower = FactoryLondonTower.GetTowerOfLondon(_level);
            LoadingPage("Demo");
            //LoadingPage("TestResult");
            
        }

        public void LoadingPage(string _nextPage)
        {
            switch (_nextPage)
            {
                case "Identification":
                    this.Navigate(new Identification());
                    break;
                case "Demo":
                    this.Navigate(new Demo(tower.GetNextTrial()));
                    break;
                case "Trial":
                    if (tower.HastNextTrial())
                    {
                        this.Navigate(new TrialPage(tower.GetNextTrial()));
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
                    this.Navigate(new TestResult(tower));
                    break;

                default:
                    break;
            }

        }

        //private void MainWindow_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    if (e.Content != null)
        //    {

        //    }
        //}
    }
}
