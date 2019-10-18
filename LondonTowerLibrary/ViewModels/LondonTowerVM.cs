using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace LondonTowerLibrary.ViewModels
{
    public class LondonTowerVM : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        #region Constructor
        public LondonTowerVM (TowerOfLondon tower)
        {
            this.Personne = new PersonneVM(tower.Personn);
            this.DateAndTime = DateTime.Now;
        }

        #endregion

        #region DateAndTime 
        private DateTime dateAndTime; 
        public DateTime DateAndTime
        {
            get { return dateAndTime; }
            set
            {
                if (value != dateAndTime)
                {
                    dateAndTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DateAndTime"));
                }
            }
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

        #region ErrorHandling
        public bool HasErrors
        {
            get
            {
                bool errors = false;

                return errors;
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private Dictionary<String, List<String>> errorList;

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region NotifyChanges Event Handler
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion



        public override string ToString()
        {
            return Personne.FirstName + " " + Personne.LastName + " a choisi les aides visuelles : " + VisualHelp.ToString();
        }
    }
}

