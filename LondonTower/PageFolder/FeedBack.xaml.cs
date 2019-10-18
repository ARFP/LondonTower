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
    /// Logique d'interaction pour FeedBack.xaml
    /// </summary>
    public partial class FeedBack : Page
    {
        CommandNavigate myCommand;
        public FeedBack()
        {
            InitializeComponent();
            this.DataContext = this;

            ButNextPage.Click += WoodButton_Click;

            myCommand = new CommandNavigate(this.NavigationService, this);

        }

        private void WoodButton_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Identification());
            MainWindow main = (MainWindow)Window.GetWindow(this);
            main.LoadingPage("TestResult");
        }

        public CommandNavigate MyNextCommand {
            get => myCommand;
            set => myCommand = value; }
    }
}
