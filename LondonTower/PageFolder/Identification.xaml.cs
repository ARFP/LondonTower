using LondonTower.Modal;
using LondonTowerLibrary;
using LondonTowerLibrary.Button;
using LondonTowerLibrary.ViewModels;
using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour Identification.xaml
    /// </summary>
    public partial class Identification : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Visibility visibilityAdmin;
        public Visibility VisibilityAdmin
        {
            get { return visibilityAdmin; }
            set
            {
                if (visibilityAdmin != value)
                {
                    visibilityAdmin = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(VisibilityAdmin)));
                }
            }
        }

        public LondonTowerVM towerVM;

        /// <summary>
        /// Constructeur par défaut
        /// bloque la possibilité de naviguer de page en page via les touches du clavier
        /// Affiche l'icone de paramétrage uniquement si le lancement de l'application s'est fait en mode administrateur 
        /// Initialise les data context de la page d'iHM correspondante
        /// </summary>
        public Identification()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            WindowsIdentity userI = WindowsIdentity.GetCurrent();
            WindowsPrincipal role = new WindowsPrincipal(userI);

            if (role.IsInRole(WindowsBuiltInRole.Administrator))
            {
                VisibilityAdmin = Visibility.Visible;
            }
            else
            {
                VisibilityAdmin = Visibility.Hidden;
            }

            this.towerVM = new LondonTowerVM();

            this.InfoStack.DataContext = this.towerVM.Personne;
            this.TowerStack.DataContext = this.towerVM;
            this.LastNameBox.Focus();
        }
        /// <summary>
        /// Gestion d'event click sur le bouton pour lancer le test avec 3 tiges
        /// trigger la méthode <c>ChangePage(int)</c> et passe le nombre de tige en paramètre
        /// </summary>
        /// <param name="sender">le boutton qui a send l'event</param>
        /// <param name="e">le contexte de l'event</param>
        private void Button3peg_MouseUp(object sender, RoutedEventArgs e)
        {
            ChangePage(3);
        }
        /// <summary>
        /// Gestion d'event click sur le bouton pour lancer le test avec 4 tiges
        /// trigger la méthode <c>ChangePage(int)</c> et passe le nombre de tige en paramètre
        /// </summary>
        /// <param name="sender">le boutton qui a send l'event</param>
        /// <param name="e">le contexte de l'event</param>
        private void Button4peg_MouseUp(object sender, RoutedEventArgs e)
        {
            ChangePage(4);
        }
        /// <summary>
        /// Gestion d'event click sur le bouton pour lancer le test avec 5 tiges
        /// trigger la méthode <c>ChangePage(int)</c> et passe le nombre de tige en paramètre
        /// </summary>
        /// <param name="sender">le boutton qui a send l'event</param>
        /// <param name="e">le contexte de l'event</param>
        private void Button5peg_MouseUp(object sender, RoutedEventArgs e)
        {
            ChangePage(5);
        }

        /// <summary>
        /// Trigger le changement de page vers la démo du test avec le nombre de peg selectionné si PersonnVM est complet 
        /// et n'a aucune erreur et modifie le champ nombre de tige de LondonTowerVM, 
        /// ne fait rien sinon
        /// </summary>
        /// <param name="_nbpeg">nombre de tiges choisies</param>
        private void ChangePage( int _nbpeg)
        {
            if (!this.towerVM.Personne.HasErrors)
            {
                towerVM.NbPegs = _nbpeg;
                MainWindow parent = Window.GetWindow(this) as MainWindow;
                parent.LoadingPage("Demo", towerVM);

            }
        }

        /// <summary>
        /// Event sur les 3 textbox de date lors d'un changement de text. 
        /// -Refuse le changement si celui ci introduit autrechose qu'un chiffre
        /// -passe à la case de date suivante si le maximum de caractère dans cette textbox a déjà été entré
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Birthdate_TextChanged(object sender, TextCompositionEventArgs e)
        {

            var castedSender = sender as TextBox;
            Regex reg = new Regex("^[0-9]$");
            if (!reg.IsMatch(e.Text))
            {
                e.Handled = true;

            }
            if (castedSender.Text.Length == castedSender.MaxLength)
                ((UIElement)e.OriginalSource).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

        }

        /// <summary>
        /// trigger le pop up de l'ecran de configuration lors du mouse down sur le texblock contenant l'icone.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e"> context de levenement </param>
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Settings set = new Settings();
            set.ShowDialog();
            if (set.DialogResult == true)
            {
                Properties.Settings.Default.PathFirstSave = set.FirstSaveFolder;
                Properties.Settings.Default.PathSecondeSave = set.SecondeSaveFolder;
                Properties.Settings.Default.Save();
            }
        }


    }
}
