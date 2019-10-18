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



        public TrialPage(Trial _t)
        {
            InitializeComponent();
            ButNextPage.Click += ButNextPage_Click;
            trial = _t;
            //this.DataContext = trial;
            //this.Resources["MyContent"]=trial;
            
            ViewTrial vtrailtest = new ViewTrial(_t);
            this.DataContext = vtrailtest;
            vtrailtest.TrialComplet += EnableNextPage;

            gameTrial = new GameUc(vtrailtest, false);
            gameGoal = new GameUc(vtrailtest, true);
            GridWindow.Children.Add(gameTrial);
            GridWindow.Children.Add(gameGoal);
        }

        private void EnableNextPage(object sender, TrialCompleteEvent e)
        {
            if(e.Complete)
            {
                ResultUc.Visibility = Visibility.Visible;
                ButNextPage.Visibility = Visibility.Visible;
            }
        }

        private void ButNextPage_Click(object sender, EventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.LoadingPage("Trial");
        }

    }
}
