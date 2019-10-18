using LondonTowerLibrary;
using LondonTowerLibrary.Button;
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
        public Identification()
        {

            InitializeComponent();
            this.DataContext = new Personn();

            var now = DateTime.Now;
            this.DateText.Text = now.ToString("MM/dd/yyyy");
            this.HourText.Text = now.ToString("HH:mm");
            button3peg.Click += Button_OnClick;
            button4peg.Click += Button_OnClick;
            button5peg.Click += Button_OnClick;
        }

        private void BirthdateText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(BirthdateText.Text, @"^\d{2}$") || Regex.IsMatch(BirthdateText.Text, @"^\d{2}/\d{2}$"))
            {
                BirthdateText.Text += "/";
                BirthdateText.CaretIndex = BirthdateText.Text.Length;
            }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Window.GetWindow(this);
            WoodButtonUc buttonSend = (WoodButtonUc)sender;
            int lvl = int.Parse(buttonSend.Tag.ToString());
            main.InitTower(lvl);
        }

    }
}
