using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LondonTowerLibrary.ViewModels
{
    public class LondonTowerVM :  INotifyPropertyChanged
    {

        #region Constructor
        public LondonTowerVM()
        {
            this.Personne = new PersonneVM(SetFormCompleted);
            this.VisualHelp = false;
            this.DateAndTime = new DateTime();
            RunTimer();
        }

        #endregion

        #region pegs
        public int NbPegs { get; set; }
        #endregion

        #region DateAndTime 
        private DateTime dateAndTime;
        public DateTime DateAndTime
        {
            get { return dateAndTime; }
            set
            {
                if (((DateTime)value).Minute != dateAndTime.Minute)
                {
                    dateAndTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DateAndTime"));
                }
            }
        }

        private void RunTimer()
        {
            Task.Run(async () =>
            {
                while (NbPegs == default)
                {
                    this.DateAndTime = DateTime.Now;
                    await Task.Delay(1000);
                }
            });
        }

        #endregion

        #region Personne
        private PersonneVM personne;
        public PersonneVM Personne
        {
            get { return personne; }
            set
            {
                if (value != personne)
                {
                    personne = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Personne"));
                }
            }
        }
        #endregion

        #region VisualHelp
        private Boolean visualHelp;
        public Boolean VisualHelp
        {
            get { return visualHelp; }
            set
            {
                if (value != visualHelp)
                {
                    visualHelp = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("VisualHelp"));
                }
            }
        }
        #endregion
        #region Everything is ok! 

        public void SetFormCompleted(bool isOk)
        {
            FormCompleted = isOk;
        }


        private bool formCompleted;
        public bool FormCompleted
        {
            get { return formCompleted; }
            set
            {
                if (formCompleted != value)
                {
                    formCompleted = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FormCompleted"));
                }
            }
        }



        #endregion
        #region feedback
        private Int16 feedback;
        public Int16 Feedback
        {
            get { return feedback; }
            set
            {
                if (value != feedback)
                {
                    feedback = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Feedback"));
                }
            }
        }
        #endregion 
        #region NotifyChanges Event Handler
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Implicit Operators
        public static implicit operator TowerOfLondon(LondonTowerVM ltvm)
        {
            return new TowerOfLondon
            {
                VisualHelp = ltvm.VisualHelp,
                DateAndTime = ltvm.DateAndTime,
                Personn = ltvm.Personne,
                Level = ltvm.NbPegs
            };
        }

        #endregion



    }
}

