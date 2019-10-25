using LondonTowerLibrary.Event;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace LondonTowerLibrary.ViewModels
{
    public class ViewTrial : NotifyPropertyChanged
    {
        private Trial trial;

        //private bool TrialStart;
        private List<ViewPeg> viewPegsList;
        private List<ViewPeg> viewPegslistGoal;

        public List<ViewPeg> ViewPegsList { get => viewPegsList; set => viewPegsList = value; }
        public List<ViewPeg> ViewPegslistGoal { get => viewPegslistGoal; set => viewPegslistGoal = value; }
        public Trial Trial { get => trial; set => trial = value; }

        public delegate void TrialCompleteEventHandler(object sender, TrialCompleteEvent arg);

        public event TrialCompleteEventHandler TrialComplet;

        public ViewTrial(Trial _t, bool _visualhelp)
        {
            this.Trial = _t;
            Trial.OnComplete += Finish;
            Trial.TryMovemade_OnChange += GetTryMoveMade;

            ViewPegsList = ListViewPeg(Trial.PegList, _visualhelp);
            ViewPegslistGoal = ListViewPeg(Trial.PegListGoal, _visualhelp);

            Trial.StartTimeDisplayTrial();
        }

        //public bool TrialSolve
        //{
        //    get
        //    {
        //        return Trial.TrialSolve;
        //    }
        //    set
        //    {
        //        Trial.TrialSolve = value;
        //        Trial.EndTrial();
        //        OnProperty_Change("TrialSolve");
        //    }
        //}

        public int GetTrialNumber
        {
            get { return this.trial.TrialNumber; }
            set { trial.TrialNumber = value; }
        }

        public int NbrMoveMade
        {
            get { return this.trial.TryMoveMade; }
            set
            {
                    OnProperty_Change(nameof(NbrMoveMade));
            }
        }

        public int NbrMoveExpect
        {
            get { return trial.MinimalMoveExpect; }
            set { trial.MinimalMoveExpect = value; }
            
        }

        public void MoveBead(ViewPeg _from, ViewPeg _to)
        {
            if(_from.CanRemoveViewBead() && _to.CanAddViewBead() || _from == _to)
            {
                this.trial.MoveBead(_from.Peg, _to.Peg);
                _to.AddViewBead(_from.RemoveTopViewBead());
            }
        }

        private static List<ViewPeg> ListViewPeg(ObservableCollection<Peg> _listPeg, bool _visualhelp)
        {
            List<ViewPeg> listViewPeg = new List<ViewPeg>();
            int position;
            foreach (Peg p in _listPeg)
            {
                ViewPeg Vpeg = new ViewPeg(p, _visualhelp);
                position = _listPeg.Count - p.PegNumber + 2;
                Grid.SetColumn(Vpeg, position);
                if (Vpeg.GetListViewBead().Count > 0)
                {
                    foreach (ViewBead vb in Vpeg.GetListViewBead())
                    {
                        Grid.SetColumn(vb, position );
                    }
                }
                listViewPeg.Add(Vpeg);
            }
            return listViewPeg;
        }

        private void Finish(object sender, TrialCompleteEvent e)
        {
            if (e.Complete)
            {
                TrialComplet(this, e);
            }  
        }

        private void GetTryMoveMade(string _propertyname)
        {
            if(_propertyname == nameof(trial.TryMoveMade))
            {
                NbrMoveMade = trial.TryMoveMade;
            }
        }


    }
}
