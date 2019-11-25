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

            // personne pre remplie 
            this.towerVM.Personne.LastName = "Hawking";
            this.towerVM.Personne.FirstName = "Stephen";
            this.towerVM.Personne.Month = "01";
            this.towerVM.Personne.Day = "08";
            this.towerVM.Personne.Year = "1942";

            //fin personne pre remplie 
            this.InfoStack.DataContext = this.towerVM.Personne;
            this.TowerStack.DataContext = this.towerVM;
            this.LastNameBox.Focus();
        }
        private void Button3peg_MouseUp(object sender, RoutedEventArgs e)
        {
            towerVM.NbPegs = 3;
            ChangePage();
        }

        private void Button4peg_MouseUp(object sender, RoutedEventArgs e)
        {
            towerVM.NbPegs = 4;
            ChangePage();
        }

        private void Button5peg_MouseUp(object sender, RoutedEventArgs e)
        {
            towerVM.NbPegs = 5;
            ChangePage();

        }
        private void ChangePage()
        {
            if (!this.towerVM.Personne.HasErrors)
            {
                MainWindow parent = Window.GetWindow(this) as MainWindow;
                parent.LoadingPage("Demo", towerVM);

            }
        }

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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

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
