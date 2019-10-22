using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.ViewModels
{
    public class ViewPeg : Image
    {
        private Peg peg;
        private List<ViewBead> ListViewBead;

        public Peg Peg
        {
            get { return peg; }
            set { peg = value; }
        }

        private ViewPeg()
        {
            ListViewBead = new List<ViewBead>();
        }

        public ViewPeg(Peg _peg, bool _visualHelp) : this()
        {
            peg = _peg;
            BitmapImage bp = new BitmapImage();
            string path2 = "pack://application:,,,/LondonTowerLibrary;component/Resources/peg" + peg.PegNumber + ".png";
            bp.BeginInit();
            bp.UriSource = new Uri(path2, UriKind.RelativeOrAbsolute);
            bp.EndInit();
            this.Source = bp;
            this.Stretch = System.Windows.Media.Stretch.None;
            this.Margin = new System.Windows.Thickness(0, 0, 0, 54);
            this.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            CreateListViewBead(_visualHelp);

        }

        public ViewBead GetViewBeadUpper()
        {
            if (this.ListViewBead.Count > 0)
            {
                //return ListViewBead[ListViewBead.Count-1];
                return ListViewBead.LastOrDefault();
            }
            return null; 
        }

        public int PegNumber()
        {
            return peg.PegNumber;
        }

        public void AddViewBead(ViewBead _vbead)
        {
                this.ListViewBead.Add(_vbead);
        }

        public ViewBead RemoveTopViewBead()
        {
            ViewBead lastBead = ListViewBead.LastOrDefault();
            if (lastBead != null)
            {
                this.ListViewBead.Remove(lastBead);
                return lastBead;
            }
            return null;
        }

        public List<ViewBead> GetListViewBead()
        {
            return this.ListViewBead;
        }

        private void CreateListViewBead(bool _visualhelp)
        {
            for(int i=0; i<peg.BeadList.Count; i++)
            {
                ViewBead vb = new ViewBead(peg.BeadList[i], _visualhelp);
                vb.Row = i;
                this.ListViewBead.Add(vb);
            }
        }

        public bool CanAddViewBead()
        {
            return this.peg.CanAdd();
        }
        public bool CanRemoveViewBead()
        {
            return this.peg.CanRemove();
        }
    }
}
