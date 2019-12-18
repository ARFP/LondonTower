using LondonTowerLibrary.Event;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace LondonTowerLibrary.ViewModels
{
    /// <summary>
    /// Classe représentant un Trial, contenant la liste des ViewPegs (representation graphique des pegs) des 2 zones
    /// </summary>
   // public class ViewTrial : NotifyPropertyChanged
    public class ViewTrial : NotifyPropertyChanged
    {
        /// <summary>
        /// Classe métié representant un niveau du test de london tower
        /// </summary>
        private Trial trial;

        /// <summary>
        /// Liste des ViewPegs de la zone de travail, là ou la personne bouge les Beads
        /// </summary>
        private List<ViewPeg> viewPegsList;

        /// <summary>
        /// Liste des ViewPegs de la zone Goal représentant le placement à réalisé
        /// </summary>
        private List<ViewPeg> viewPegslistGoal;

        /// <summary>
        /// Accesseur de viewPegsList Liste des ViewPegs de la zone de travail, là ou la personne bouge les Beads
        /// </summary>
        public List<ViewPeg> ViewPegsList { get => viewPegsList; set => viewPegsList = value; }

        /// <summary>
        /// Accesseur de viewPegslistGoal Liste des ViewPegs de la zone Goal représentant le placement à réalisé
        /// </summary>
        public List<ViewPeg> ViewPegslistGoal { get => viewPegslistGoal; set => viewPegslistGoal = value; }

        /// <summary>
        /// Accesseur de trial Classe métié representant un niveau du test de london tower
        /// </summary>
        public Trial Trial { get => trial; set => trial = value; }

        /// <summary>
        /// Delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public delegate void TrialCompleteEventHandler(object sender, TrialCompleteEvent arg);

        /// <summary>
        /// Evenement déclenché lorsque le Trial est complété.
        /// </summary>
        public event TrialCompleteEventHandler TrialComplet;


        /// <summary>
        /// Constructeur paramétré, abonnement au evenement déclenché par le trial <paramref name="_t"/>.
        /// Creation des listes de ViewPeg, representant graphiquement les pegs et leurs Beads associé
        /// </summary>
        /// <param name="_t">Trial representant le niveau actuel du test</param>
        /// <param name="_visualhelp">boolean de selection d'option à l'aide visuel</param>
        public ViewTrial(Trial _t, bool _visualhelp)
        {
            this.Trial = _t;
            Trial.OnComplete += Finish;
            Trial.TryMovemade_OnChange += GetTryMoveMade;

            ViewPegsList = ListViewPeg(Trial.PegList, _visualhelp);
            ViewPegslistGoal = ListViewPeg(Trial.PegListGoal, _visualhelp);

            Trial.StartTimeDisplayTrial();
        }

        /// <summary>
        /// Accesseur lecture/ecriture pour le numero de Trial, stocké dans <c>trial</c>
        /// </summary>
        public int GetTrialNumber
        {
            get { return this.trial.TrialNumber; }
            set { trial.TrialNumber = value; }
        }

        /// <summary>
        /// Accesseur lecture et declenchement INotifyPropertyChanged sur la propriété
        /// </summary>
        public int NbrMoveMade
        {
            get { return this.trial.TryMoveMade; }
            set
            {
                    OnProperty_Change(nameof(NbrMoveMade));
            }
        }

        /// <summary>
        /// Accesseur Lecture/Ecriture sur le nombre minimal de coup pour le trial <c>MinimalMoveExpect</c>
        /// </summary>
        public int NbrMoveExpect
        {
            get { return trial.MinimalMoveExpect; }
            set { trial.MinimalMoveExpect = value; }
            
        }

        /// <summary>
        /// Mouvement des Beads d'un Peg à l'autre avec mise a jour des View lié à ces classes
        /// </summary>
        /// <param name="_from">ViewPeg de depart de la ViewBead selectionné</param>
        /// <param name="_to">ViewPeg cible pour l'ajout de la ViewBead selectionné</param>
        public void MoveBead(ViewPeg _from, ViewPeg _to)
        {
            if(_from.CanRemoveViewBead() && _to.CanAddViewBead() || _from == _to)
            {
                this.trial.MoveBead(_from.Peg, _to.Peg);
                _to.AddViewBead(_from.RemoveTopViewBead());
            }
        }

        /// <summary>
        /// Creation de la liste des ViewPeg avec la creation des ViewBead  
        /// </summary>
        /// <param name="_listPeg"> liste de Peg contenue dans le trial actuel</param>
        /// <param name="_visualhelp">Boolean representant l'option d'aide visuel</param>
        /// <returns>Retourne la liste des ViewPeg et leur ViewBead associé, representation graphique des Pegs et Bead d'un trial</returns>
        private static List<ViewPeg> ListViewPeg(ObservableCollection<Peg> _listPeg, bool _visualhelp)
        {
            List<ViewPeg> listViewPeg = new List<ViewPeg>();
            int position;
            foreach (Peg p in _listPeg)
            {
                ViewPeg Vpeg = new ViewPeg(p, _visualhelp);
                position = _listPeg.Count - p.PegNumber + 1;
                Grid.SetColumn(Vpeg, position);
                if (Vpeg.GetListViewBead().Count > 0)
                {
                    foreach (ViewBead vb in Vpeg.GetListViewBead())
                    {
                        Grid.SetColumn(vb, position);
                    }
                }
                listViewPeg.Add(Vpeg);
            }
            return listViewPeg;
        }

        /// <summary>
        /// Declenchement de l'evenement TrialComplet. Propagation de l'evenement provenant du Trial <c>TrialCompleteEventHandler</c>
        /// indiquant la fin du trial actuel, faisant ainsi remonté l'evenement jusqu'a l'HIM
        /// </summary>
        /// <param name="sender">trial ayant déclenché l'evenement TrialCompleteEventHandler</param>
        /// <param name="e">Evenement déclenché par le trial</param>
        private void Finish(object sender, TrialCompleteEvent e)
        {
            if (e.Complete)
            {
                TrialComplet(this, e);
            }  
        }

        /// <summary>
        /// Méthode declencher lors de l'evenement TriMoveMadeChange mettant a jour NbrMoveMade avec la propriété <c>TryMoveMade</c>
        /// </summary>
        /// <param name="_propertyname">nom de la propriété declenchent l'evenement</param>
        private void GetTryMoveMade(string _propertyname)
        {
            if(_propertyname == nameof(trial.TryMoveMade))
            {
                NbrMoveMade = trial.TryMoveMade;
            }
        }


    }
}
