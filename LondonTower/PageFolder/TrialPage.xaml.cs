using LondonTowerLibrary;
using LondonTowerLibrary.Event;
using LondonTowerLibrary.UserControls;
using LondonTowerLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour TrialPage.xaml
    /// </summary>
    public partial class TrialPage : Page
    {

        private Trial trial;
        GameUc gameTrial ;
        GameUc gameGoal ;



        public TrialPage(Trial _t, bool _visualhelp)
        {
            InitializeComponent();
            ButNextPage.Click += ButNextPage_Click;
            trial = _t;
            //this.DataContext = trial;
            //this.Resources["MyContent"]=trial;
            
            ViewTrial vtrailtest = new ViewTrial(_t, _visualhelp);
            this.DataContext = vtrailtest;
            vtrailtest.TrialComplet += EnableNextPage;
            gameGoal = new GameUc(vtrailtest, true);
            gameTrial = new GameUc(vtrailtest, false);
            GridWindow.Children.Add(gameGoal);
            GridWindow.Children.Add(gameTrial);
            
        }

        private void EnableNextPage(object sender, TrialCompleteEvent e)
        {
            if(e.Complete)
            {
                ResultUc.Visibility = Visibility.Visible;
                ButNextPage.MAGICEnabled = true;
            }
        }

        private void ButNextPage_Click(object sender, EventArgs e)
        {
            if (ButNextPage.MAGICEnabled)
            {
                MainWindow main = (MainWindow)Window.GetWindow(this);
                main.LoadingPage("Trial");
            }

        }

    }
}
