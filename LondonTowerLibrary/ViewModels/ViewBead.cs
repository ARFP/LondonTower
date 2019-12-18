using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.ViewModels
{    /// <summary>
     /// Classe ViewBead representation graphique d'une Bead (perle)
     /// Heritage de la classe Image la classe ViewBead étant une simple représentation graphique
     /// </summary>
    public class ViewBead : Image, INotifyPropertyChanged
    {
        private int row;
        private Bead bead;

        private BitmapImage ImgCut;
        private BitmapImage ImgUnCut;
        private string pathCut;
        private string pathUncut;

        /// <summary>
        /// Evenement implemente <c>INotifyPropertyChanged</c> 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperty_Change(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Objet Bead representation metier d'une perle
        /// </summary>
        public Bead Bead {
            get { return bead; }
            set { bead = value; }
        }

        /// <summary>
        /// Entier représentant sa position sur une colonne.
        /// Empilement de bas en haut
        /// </summary>
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                this.row = value;
                OnProperty_Change("Row");
            }
        }
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        private ViewBead()
        {
            this.Stretch = System.Windows.Media.Stretch.None;
            this.PropertyChanged += RefreshMargin;
        }

        /// <summary>
        /// Constructeur paramétré appelant le constructeur par defaut.
        /// Initialisation de l'image source grace au <paramref name="_bead"/> passé en paramètre.
        /// Ancrage vertical en bas <c>VerticalAlignment.Bottom</c>
        /// </summary>
        /// <param name="_bead">Instance d'objet Bead representant un Bead, une perle</param>
        /// <param name="_visualHelp">Boolean d'option d'aide visuel</param>
        public ViewBead(Bead _bead, bool _visualHelp) : this()
        {
            this.bead = _bead;

            if (_visualHelp)
            {
                 pathCut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + "_Cut_vh.png"; ;
                pathUncut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + "_vh.png"; ;
            }
            else
            {
                 pathCut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + "_Cut.png";
                pathUncut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + ".png";
            }
            
            ImgCut = new BitmapImage();
            ImgCut.BeginInit();
            ImgCut.UriSource = new Uri(pathCut, UriKind.RelativeOrAbsolute);
            ImgCut.EndInit();

            ImgUnCut = new BitmapImage();
            ImgUnCut.BeginInit();
            ImgUnCut.UriSource = new Uri(pathUncut, UriKind.RelativeOrAbsolute);
            ImgUnCut.EndInit();

            this.Source = ImgCut;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

        }

        /// <summary>
        /// Chargement de l'image source avec l'image Bead coupé en son sommet (Bead empilé sur Peg)
        /// </summary>
        public void SetImgCut()
        {
            this.Source = ImgCut;
        }

        /// <summary>
        /// Chargement de l'image source avec l'image Bead non coupé en son sommet (Bead hors Peg)
        /// </summary>
        public void SetImgUnCut()
        {
            this.Source = ImgUnCut;
        }

        /// <summary>
        /// Update de la position vertical par rapport au bas de la zone.
        /// Déclenché par le changement de <c>Row</c>
        /// </summary>
        /// <param name="sender">Objet déclencheur de l'evenement <c>Row</c></param>
        /// <param name="e">Evenement declencher</param>
        private void RefreshMargin(object sender, EventArgs e)
        {
            this.Margin = new System.Windows.Thickness(0, 0, 0, (Row * 55)+54);
        }

    }
}
