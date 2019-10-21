using LondonTowerLibrary;
using LondonTowerLibrary.Button;
using LondonTowerLibrary.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LondonTower.PageFolder
{
    /// <summary>
    /// Logique d'interaction pour Identification.xaml
    /// </summary>
    public partial class Identification : Page
    {

        public LondonTowerVM towerVM;
        public Identification()
        {
            InitializeComponent();

            
            

            this.towerVM = new LondonTowerVM();

            // personne pre remplie 
            this.towerVM.Personne.LastName = "Toto";
            this.towerVM.Personne.FirstName = "Titi";
            this.towerVM.Personne.Month = "12";
            this.towerVM.Personne.Day = "31";
            this.towerVM.Personne.Year = "2000";

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


        //private void Button_OnClick(object sender, RoutedEventArgs e)
        //{
        //    MainWindow main = (MainWindow)Window.GetWindow(this);
        //    WoodButtonUc buttonSend = (WoodButtonUc)sender;
        //    int lvl = int.Parse(buttonSend.Tag.ToString());
        //    main.InitTower(lvl);
        //}

    }
}
