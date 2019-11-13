using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
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


namespace LondonTowerSetting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string firstSaveFolder;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string FirstSaveFolder
        {
            get { return firstSaveFolder; }
            set
            {
                firstSaveFolder = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstSaveFolder)));
            }
        }

        private string secondeSaveFolder;
        public string SecondeSaveFolder
        {
            get { return secondeSaveFolder; }
            set
            {
                secondeSaveFolder = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SecondeSaveFolder)));
            }
        }


        public MainWindow()
        {
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            //filemap.ExeConfigFilename = @"./LondonTower.exe.config";
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string localdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            filemap.RoamingUserConfigFilename = Path.Combine(appdata, @"LondonTower");
            filemap.LocalUserConfigFilename = Path.Combine(localdata, @"LondonTower","user.config");
            filemap.ExeConfigFilename = "./LondonToxer.exe.config";

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.PerUserRoamingAndLocal);


            FirstSaveFolder = LondonTower.Properties.Settings.Default.PathFirstSave;
            SecondeSaveFolder = LondonTower.Properties.Settings.Default.PathSecondeSave;
            //SecondeSaveFolder = config.AppSettings.Settings["PathSecondeSave"].Value;
            //SecondeSaveFolder = section.Settings["PathSecondeSave"].Value;
            InitializeComponent();

        }

        private void Save_Onclick(object sender, RoutedEventArgs e)
        {

        }
        private void Leave_Onclick(object sender, RoutedEventArgs e)
        {

        }
        private void Browse_Onclick(object sender, RoutedEventArgs e)
        {

        }

    }
}
