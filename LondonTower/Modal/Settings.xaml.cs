using LondonTowerLibrary.Button;
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
    /// Fenetre Modal pour la configuration des chemins de sauvegarde des fichiers EXCEL lors de la fin d'un test
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Fenetre de dialogue pour choisir un dossier de sauvegarde
        /// </summary>
        FolderBrowserDialog folder;
        /// <summary>
        /// Implémentation de l'interface INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Chemin de sauvegarde principale
        /// </summary>
        private string firstSaveFolder;
        /// <summary>
        /// Accesseur de firstSaveForlder correspondant au Chemin de sauvegarde principale
        /// </summary>
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

        /// <summary>
        /// Chemin de sauvegarde secondaire
        /// </summary>
        private string secondeSaveFolder;
        /// <summary>
        /// Accesseur de secondeSaveForlder correspondant au chemin de sauvegarde secondaire
        /// </summary>
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
        /// <summary>
        /// Gestion d'evenement au Click sur un bouton ouvrant la fenetre de choix de dossier
        /// </summary>
        /// <param name="sender">objet declencheur de l'evenement</param>
        /// <param name="e">Evenement routé</param>
        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            WoodButtonUc but = (WoodButtonUc)sender;
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

        /// <summary>
        /// Gestion d'evenement au Click sur le bouton Sauvegarder
        /// </summary>
        /// <param name="sender">Object declencheur de l'evenement</param>
        /// <param name="e">Evenement routé</param>
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Gestion d'evenement au Click sur le bouton Annuler
        /// </summary>
        /// <param name="sender">Object declencheur de l'evenement</param>
        /// <param name="e">Evenement routé</param>
        private void Leave_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



    }
}
