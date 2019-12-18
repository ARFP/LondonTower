using LondonTowerLibrary;
using LondonTowerLibrary.Event;
using LondonTowerLibrary.UserControls;
using LondonTowerLibrary.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Demo.xaml
    /// Page de demonstration de LondonTower
    /// </summary>
    public partial class Demo : Page
    {
        Trial trial;

        /// <summary>
        /// Constructeur parametré de la page de demonstration de LondonTower. 
        /// Initialise les 2 zone d'affichage, zone de travail et zone Goal.
        /// Initialisation 
        /// </summary>
        /// <param name="_t">Trial correspondant à la démo</param>
        /// <param name="_visualhelp">Booleen pour l'option d'aide visuel</param>
        public Demo(Trial _t, bool _visualhelp)
        {
            InitializeComponent();
            ButNextPage.Click += ButNextPage_Click;
            trial = _t;
            this.DataContext = trial;
            ViewTrial vtrailtest = new ViewTrial(_t, _visualhelp);
            vtrailtest.TrialComplet+= EnableNextPage;
            GameUc gameGoal = new GameUc(vtrailtest, true);
            GameUc gameTrial = new GameUc(vtrailtest, false);
            //GridWindow.Children.Add(gameGoal);
            //GridWindow.Children.Add(gameTrial);
            Grid.SetColumn(gameTrial, 0);
            Grid.SetColumn(gameGoal, 1);
            GridUc.Children.Add(gameGoal);
            GridUc.Children.Add(gameTrial);


            ConfigLabel();
        }

        /// <summary>
        /// Changement de page en cliquant sur la fleche 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButNextPage_Click(object sender, EventArgs e)
        {
            if (ButNextPage.MAGICEnabled)
            {
                MainWindow main = (MainWindow)Window.GetWindow(this);
                main.LoadingPage("Trial");
            }
        }

        /// <summary>
        /// Configuration des labels contenu dans la page
        /// </summary>
        private void ConfigLabel()
        {
            labelTitreInstru.Content = "Tour de Londres : Instructions";
            labelTextInstru.FontSize = 18;
            labelTextInstru.Text = "L’objectif de ce puzzle est de bouger les perles sur l’écran de gauche pour qu’elles se retrouvent dans l’arrangement voulu. " + "" +
                "Autrement dit, les perles doivent se retrouver dans les positions indiquées par l’écran « objectif final » à droite. " +
                "\nVous ne pouvez déplacer qu’une seule perle à la fois. " +
                "Vous devez respecter à la fois la position et la couleur des perles pour atteindre l’objectif demandé. " +
                "Essayez d’atteindre l’objectif en un minimum de coups. " +
                "Cliquez avec la souris en dessous de la perle que vous souhaitez déplacer, " +
                "puis déplacez-là jusqu’à sa tige de destination, puis cliquez de nouveau pour la déposer. " +
                "La perle descendra sur la tige. " +
                "\nAvez - vous des questions ? ";

        }

        /// <summary>
        /// Fonction déclenché par l'evenement TrialCompletEventHandler notifiant que le trial est réussi et que le passage a l'épreuve suivante est autorisé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableNextPage(object sender, TrialCompleteEvent e)
        {
            if (e.Complete)
            {
                ButNextPage.MAGICEnabled =true;
            }
        }

    }
}
