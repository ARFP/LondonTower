using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.ViewModels
{
    public class ViewBead : Image, INotifyPropertyChanged
    {
        private int row;
        private Bead bead;

        private BitmapImage ImgCut;
        private BitmapImage ImgUnCut;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperty_Change(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Bead Bead { get { return bead; } set { bead = value; } }

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
                //RefreshMargin();
            }
        }

        private ViewBead()
        {
            this.Stretch = System.Windows.Media.Stretch.None;
            this.PropertyChanged += RefreshMargin;
        }

        public ViewBead(Bead _bead) : this()
        {
            this.bead = _bead;
            
            //string pathCut = "bead_" + bead.ColorBead + "_Cut.png";
            //string pathUnCut = "bead_" + bead.ColorBead + ".png";

            string pathCut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + "_Cut.png";
            string pathUnCut = "pack://application:,,,/LondonTowerLibrary;component/Resources/bead_" + bead.ColorBead + ".png";

            ImgCut = new BitmapImage();
            ImgCut.BeginInit();
            ImgCut.UriSource = new Uri(pathCut, UriKind.RelativeOrAbsolute);
            ImgCut.EndInit();

            ImgUnCut = new BitmapImage();
            ImgUnCut.BeginInit();
            ImgUnCut.UriSource = new Uri(pathUnCut, UriKind.RelativeOrAbsolute);
            ImgUnCut.EndInit();

            this.Source = ImgCut;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

        }

        private void RefreshMargin(object sender, EventArgs e)
        {
            this.Margin = new System.Windows.Thickness(0, 0, 0, (Row * 55)+54);
        }

    }
}
