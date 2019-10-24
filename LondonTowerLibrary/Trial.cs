using LondonTowerLibrary.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LondonTowerLibrary
{
    [Serializable]
    public class Trial
    {
        private int trialNumber;
        private int minimalMoveExpect;
        private int tryMoveMade;
        private int timeToResolve;
        private int timeDisplayUntilResolve;
        
        private DateTime TimeFirstMoveTrial;
        private DateTime TimeToResolveTrial;
        private DateTime TimeDisplaytrial;

        private ObservableCollection<Peg> pegList;
        private ObservableCollection<Peg> pegListGoal;

        public ObservableCollection<Peg> PegListGoal { get => pegListGoal; set => pegListGoal = value; }
        public int MinimalMoveExpect { get => minimalMoveExpect; set => minimalMoveExpect = value; }     
        public int TrialNumber { get => trialNumber; set => trialNumber = value; }
        public int TimeToResolve { get => timeToResolve; set => timeToResolve = value; }
        public int TimeDisplayUntilResolve { get => timeDisplayUntilResolve; set => timeDisplayUntilResolve = value; }



        /*EVENEMENTS*/
        public delegate void TrialCompleteEventHandler(object sender, TrialCompleteEvent args);
        public delegate void TryMoveMadeChange(string _NameProperty);

        public event TrialCompleteEventHandler OnComplete;
        public event TryMoveMadeChange TryMovemade_OnChange;

        public int TryMoveMade {
            get { return tryMoveMade; }
            set {
                tryMoveMade = value;
                if (tryMoveMade > 0)
                {
                    TryMovemade_OnChange(nameof(TryMoveMade));
                }

            }
        }
        
        public ObservableCollection<Peg> PegList
        {
            get
            {
                return pegList;
            }
            set
            {
                pegList = value;
            }
        }



        public void MoveBead(Peg _from, Peg _to)
        {
            if (_to.CanAdd() && _from.CanRemove() || _from == _to)
            {
                Bead bd = _from.RemoveTopBead();
                _to.AddBead(bd);
                MoveMade();
            }
            this.FinishTrial();
        }

        public Trial(int trialNumber, int minimalMoveExpect) : this()
        {
            TrialNumber = trialNumber;
            MinimalMoveExpect = minimalMoveExpect;
        }

        public Trial()
        {
            TryMoveMade = 0;
        }

        public void StartTimeDisplayTrial()
        {
            TimeDisplaytrial = DateTime.Now;
        }

        public void StartTrial()
        {
            TimeFirstMoveTrial = DateTime.Now;
        }

        public void EndTrial()
        {
            TimeToResolveTrial = DateTime.Now;
        }

        public void AddGoalPeg(Peg _GoalPeg)
        {
            PegListGoal.Add(_GoalPeg);
        }

        public int GetTimeToResolve()
        {
            TimeSpan time = TimeToResolveTrial-TimeFirstMoveTrial;
            return (int)time.TotalSeconds;
        }

        public int GetTimeUntilLoad()
        {
            TimeSpan time = TimeToResolveTrial-TimeDisplaytrial;
            return (int)time.TotalSeconds;
        }

        public void MoveMade()
        {
            TryMoveMade++;
        }

        //public void FinishTrial(object sender, NotifyCollectionChangedEventArgs args)
        public void FinishTrial()
        {
            bool test = true;
            for (int i = 0; i < PegList.Count; i++)
            {
                if (!PegList[i].Equals(PegListGoal[i]))
                {

                    test = false;
                }
            }

            if (test)
            {
                EndTrial();
                this.TimeDisplayUntilResolve = GetTimeUntilLoad();
                this.TimeToResolve = GetTimeToResolve();

                OnComplete(this, new TrialCompleteEvent(true));
            }
        }
    }
}
