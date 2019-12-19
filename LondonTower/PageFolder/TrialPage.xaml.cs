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
        private GameUc gameTrial ;
        private GameUc gameGoal ;


        /// <summary>
        /// Constructeur paramétré instanciant la page et les UserControls ainsi que leur ajout a la page
        /// </summary>
        /// <param name="_t">Trial actuel contenant les listes des Pegs zone de travail et zone goal</param>
        /// <param name="_visualhelp">Boolean pour l'option d'aide visuel</param>
        public TrialPage(Trial _t, bool _visualhelp)
        {
            InitializeComponent();
            ButNextPage.Click += ButNextPage_Click;
            trial = _t;
            
            ViewTrial vtrailtest = new ViewTrial(_t, _visualhelp);
            this.DataContext = vtrailtest;
            vtrailtest.TrialComplet += EnableNextPage;
            gameGoal = new GameUc(vtrailtest, true);
            gameTrial = new GameUc(vtrailtest, false);
            Grid.SetColumn(gameTrial, 0);
            Grid.SetColumn(gameGoal, 1);
            GridUc.Children.Add(gameGoal);
            GridUc.Children.Add(gameTrial);
            
        }

        /// <summary>
        /// Foction declenché par l'evenement TrialComplet de ViewTrial informant la résolution du Trial, 
        /// permettant l'affichage du bouton Fleche pour le changement de page
        /// </summary>
        /// <param name="sender">ViewTrial ayant déclencher l evenement</param>
        /// <param name="e">Evenement custom pour la resolution du Trial</param>
        private void EnableNextPage(object sender, TrialCompleteEvent e)
        {
            if(e.Complete)
            {
                ResultUc.Visibility = Visibility.Visible;
                ButNextPage.MAGICEnabled = true;
            }
        }

        /// <summary>
        /// Déclenché lors du click sur le bouton <c>WoodButtonUc</c>
        /// appelle la fonction de mainwindow pour passer à la page suivante si <c>ButNextPage.MAGICEnabled</c> est true
        /// </summary>
        /// <param name="sender">Bouton declencheur de l eventement</param>
        /// <param name="e">Evenement declenché</param>
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
