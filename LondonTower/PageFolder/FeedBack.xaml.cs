
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
        CommandNavigate myCommand;
        string fback;
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

            main.LoadingPage("TestResult", fback);
        }

        public CommandNavigate MyNextCommand {
            get => myCommand;
            set => myCommand = value; }
        private void WoodRadioUC_WhyThou(object sender, RoutedEventArgs e)
        {
            WoodRadioUC radio = (WoodRadioUC)sender;
            this.fback = radio.RadValue ;
            this.ButNextPage.MAGICEnabled = true;
        }
    }
}
