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
        private string pathCut;
        private string pathUncut;

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
            }
        }

        private ViewBead()
        {
            this.Stretch = System.Windows.Media.Stretch.None;
            this.PropertyChanged += RefreshMargin;
        }

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
        public void SetImgCut()
        {
            this.Source = ImgCut;
        }
        public void SetImgUnCut()
        {
            this.Source = ImgUnCut;
        }

        private void RefreshMargin(object sender, EventArgs e)
        {
            this.Margin = new System.Windows.Thickness(0, 0, 0, (Row * 55)+54);
        }

    }
}
