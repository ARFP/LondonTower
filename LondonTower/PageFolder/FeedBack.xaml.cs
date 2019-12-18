
using LondonTowerLibrary.Button;
using System.Windows;
using System.Windows.Controls;


namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour FeedBack.xaml
    /// </summary>
    public partial class FeedBack : Page
    {
        /// <summary>
        /// récupère la valeur de 1 à 10 du ressenti de l'utilisateur. Elle sera renvoyé à la mainwindow qui l'attribuera à londontowerVM.
        /// </summary>
        string fback;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public FeedBack()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// Déclenché lors du click sur le bouton <c>WoodButton</c>
        /// appelle la fonction de mainwindow pour passer à la page suivante si <c>ButNextPage.MAGICEnabled</c> est true
        /// </summary>
        /// <param name="sender">bouton sender</param>
        /// <param name="e">informations sur l'evenement</param>
        private void WoodButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.ButNextPage.MAGICEnabled)
            {
                MainWindow main = (MainWindow)Window.GetWindow(this);
                main.LoadingPage("TestResult", fback);
            }
        }

        /// <summary>
        /// Refèrence la DependencyProperty de woodradioUC
        /// Set la propriété MagicEnabled de WoodRadioUC sur true si l'utilsiateur a coché l'un des boutons du radiobutton.
        /// Set la propriété fback  sur la valeur choisie par l'utilisateur dans le radiobutton. 
        /// </summary>
        /// <param name="sender">objet envoyant l'evenement</param>
        /// <param name="e">informations sur l'evenement</param>
        private void WoodRadioUC_WhyThou(object sender, RoutedEventArgs e)
        {
            WoodRadioUC radio = (WoodRadioUC)sender;
            this.fback = radio.RadValue;
            this.ButNextPage.MAGICEnabled = true;
        }
    }
}
