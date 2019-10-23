using LondonTower.PageFolder;
using LondonTowerLibrary;
using LondonTowerLibrary.ViewModels;
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

             //tower = FactoryLondonTower.InitialiseTowerOfLondon();

        }
        public void InitTower(LondonTowerVM towerVM)
        {
            tower = FactoryLondonTower.GetTowerOfLondon(towerVM);
            
            //LoadingPage("Demo");
            //LoadingPage("TestResult");
            //LoadingPage("TestResult");
            
        }

        public void LoadingPage(string _nextPage, object sentback = null)
        {
            switch (_nextPage)
            {
                case "Identification":
                    this.Navigate(new Identification());
                    break;
                case "Demo":
                    InitTower((LondonTowerVM)sentback);
                    this.Navigate(new Demo(tower.GetNextTrial(), tower.VisualHelp));
                    //this.Navigate(new TestResult(tower));
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

        //private void MainWindow_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    if (e.Content != null)
        //    {

        //    }
        //}
    }
}
