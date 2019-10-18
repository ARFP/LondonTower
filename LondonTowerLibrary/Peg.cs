using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace LondonTowerLibrary
{
    [Serializable]
    public class Peg : IEquatable<Peg>
    {
        private int pegNumber;
        private ObservableCollection<Bead> beadList;


        public int PegNumber { get => pegNumber; set => pegNumber = value; }
        public ObservableCollection<Bead> BeadList {
            get {
                return beadList;
            }
            set {
                beadList = value;
            }
        }

        public event NotifyCollectionChangedEventHandler Changed
        {
            add
            {
                beadList.CollectionChanged += value;
            }
            remove
            {
                beadList.CollectionChanged -= value;
            }
        }

        public Peg()
        {
            BeadList = new ObservableCollection<Bead>();
        }

        public Peg(int _PegNumber) : this()
        {
            this.PegNumber = _PegNumber;

        }

        public bool AddBead(Bead _b)
        {
            if (BeadList.Count < this.PegNumber)
            {
                BeadList.Add(_b);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Bead RemoveTopBead()
        {
            Bead b = BeadList.LastOrDefault();

            if (CanRemove() && b != null)
            {
                BeadList.Remove(b);
                return b;
            }
            return null;

        }

        public bool CanAdd()
        {
            if (BeadList.Count < this.PegNumber)
            {
                return true;
            }
            return false;
        }

        public bool CanRemove()
        {
            if (BeadList.Count > 0)
            {
                return true;
            }
            return false;
        }



        public bool Equals(Peg other)
        {
            for (int i = 0; i < BeadList.Count; i++)
            {
                if (other.BeadList == null|| other.BeadList.Count!= this.BeadList.Count ||!this.BeadList[i].Equals(other.BeadList[i]) )
                {
                    return false;
                }
            }
            return true;
        }

    }
}
