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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LondonTower.Modal
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        FolderBrowserDialog folder;
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstSaveFolder;
        public string FirstSaveFolder
        {
            get { return firstSaveFolder; }
            set
            {
                if (firstSaveFolder != value)
                {
                    firstSaveFolder = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstSaveFolder)));
                }
            }
        }



        private string secondeSaveFolder;
        public string SecondeSaveFolder
        {
            get { return secondeSaveFolder; }
            set
            {
                if (secondeSaveFolder != value)
                {
                    secondeSaveFolder = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SecondeSaveFolder)));
                }
            }
        }
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            FirstSaveFolder = Properties.Settings.Default.PathFirstSave;
            SecondeSaveFolder = Properties.Settings.Default.PathSecondeSave;
        }

        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button but = (System.Windows.Controls.Button)sender;
            folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
             
                if(but.Tag.ToString() == "First")
                {
                    FirstSaveFolder = folder.SelectedPath;
                }
                else
                {
                    SecondeSaveFolder = folder.SelectedPath;
                }

            }
            
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Leave_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



    }
}
