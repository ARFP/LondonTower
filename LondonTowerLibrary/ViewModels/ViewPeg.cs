using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.ViewModels
{
    /// <summary>
    /// Classe ViewPeg representation graphique d'un Peg dans sa position et hauteur
    /// Heritage de la classe Image la classe ViewPeg étant une simple représentation graphique
    /// </summary>
    public class ViewPeg : Image
    {
        /// <summary>
        /// Liste des ViewBead, representation graphique des Beads empilé sur le Peg
        /// </summary>
        private List<ViewBead> ListViewBead;
        /// <summary>
        /// Classe metié representant un pique/tour sur le jeu London Tower où l'on doit empilé des Beads/perles
        /// </summary>
        private Peg peg;
        public Peg Peg
        {
            get { return peg; }
            set { peg = value; }
        }

        /// <summary>
        /// Constructeur par défaut en privé pour forcer la construction avec le constructeur paramétré.
        /// Initialisation de la list de ViewBead.
        /// </summary>
        private ViewPeg()
        {
            ListViewBead = new List<ViewBead>();
        }

        /// <summary>
        /// Appel du constructeur par defaut pour l'initialisation de de la liste de ViewBead.
        /// Initialisation de l'image source de la classe.
        /// Positionnement du Peg dans son environnement.
        /// </summary>
        /// <param name="_peg"></param>
        /// <param name="_visualHelp"></param>
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
        /// <summary>
        /// Fonction pour selectionné le ViewBead le plus haut graphiquement (derniere place dans la liste)
        /// </summary>
        /// <returns>si la list ListViewBead est vide retourne null, sinon retourne le dernier ViewBead de cette liste</returns>
        public ViewBead GetViewBeadUpper()
        {
            if (this.ListViewBead.Count > 0)
            {               
                return ListViewBead.LastOrDefault();
            }
            return null;
        }
        /// <summary>
        /// Recupere le numero du Peg stocké dans la classe 
        /// </summary>
        /// <returns>retourne le numero du peg</returns>
        public int PegNumber()
        {
            return peg.PegNumber;
        }
        /// <summary>
        /// Ajout d'un ViewBead à la liste ListViewBead
        /// </summary>
        /// <param name="_vbead">ViewBead à ajouter</param>
        public void AddViewBead(ViewBead _vbead)
        {
            this.ListViewBead.Add(_vbead);
        }

        /// <summary>
        /// Supprime le dernier ViewBead dans la liste ListViewBead
        /// </summary>
        /// <returns>retourne le ViewBead supprimé si la Liste n'est pas vide, sinon retourne null</returns>
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
        /// <summary>
        /// Retourne la liste de ViewBead
        /// </summary>
        /// <returns> retourne ListViewBead </returns>
        public List<ViewBead> GetListViewBead()
        {
            return this.ListViewBead;
        }

        /// <summary>
        /// Création des ViewBead, représentation graphique des Beads empilés sur le Peg
        /// </summary>
        /// <param name="_visualhelp">Booléen représentant l'option d'affichage des aides visuels, false pour aucune aide
        /// true pour l affichage de la premiere lettre de la couleur sur le Bead</param>
        private void CreateListViewBead(bool _visualhelp)
        {
            for (int i = 0; i < peg.BeadList.Count; i++)
            {
                ViewBead vb = new ViewBead(peg.BeadList[i], _visualhelp);
                vb.Row = i;
                this.ListViewBead.Add(vb);
            }
        }

        /// <summary>
        /// Verification de la possibilité d'ajouter un Bead sur le Peg
        /// </summary>
        /// <returns>Retourne le résultat de l'appel la fonction <c>CanAdd() qui retourne un boolean s'il est 
        /// possible d'ajouter un Bead a la liste</c></returns>
        public bool CanAddViewBead()
        {
            return this.peg.CanAdd();
        }

        /// <summary>
        /// Verification de la possibilité d'enlever un Bead sur le Peg
        /// </summary>
        /// <returns>Retourne le résultat de l'appel de la fonction <c>CanRemove()</c> du peg qui retourne 
        /// un boolean s'il est possible de supprimé un Bead de la liste</returns>
        public bool CanRemoveViewBead()
        {
            return this.peg.CanRemove();
        }
    }
}
