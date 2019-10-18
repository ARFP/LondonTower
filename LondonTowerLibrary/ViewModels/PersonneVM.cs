using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LondonTowerLibrary.ViewModels
{
    public class PersonneVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Constructor 
        public PersonneVM(Personn person)
        {
            this.BirthDate = person.DayofBirth;
            this.FirstName = person.FirstName;
            this.Genre = person.Genre;
            this.LastName = person.LastName;
            this.errorList = new Dictionary<string, List<string>>();
        }

        #endregion

        #region LastName
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
                    GetErrorForLastName(value).ContinueWith(task =>
                    {
                        lock (errorList)
                        {
                            errorList["LastName"] = task.Result;
                            ErrorsChanged(this, new DataErrorsChangedEventArgs("LastName"));
                        }
                    });
                }
            }
        }

        Task<List<string>> GetErrorForLastName(string value)
        {

            return Task.Factory.StartNew<List<string>>(() => 
            {
                return null;
            });

        }

        #endregion

        #region BirthDate
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("BirthDate"));
                    GetErrorForBirthDate(value).ContinueWith(task =>
                    {
                        lock (errorList)
                        {
                            errorList["BirthDate"] = task.Result;
                            ErrorsChanged(this, new DataErrorsChangedEventArgs("BirthDate"));
                        }
                    });
                }
            }
        }
        Task<List<string>> GetErrorForBirthDate(DateTime value)
        {
            return Task.Factory.StartNew<List<string>>(()=> 
            {
                List<string> listError = new List<string>();
                var now = DateTime.Now;
                var age = now.Year - value.Year;
                 age = (value.Date > now.AddYears(-age)) ? --age : age;
                if (age > 120 || age < 0)
                      listError.Add(" La date de naissance n'est pas valide");
                return listError;
            });
        }
        #endregion

        #region Genre
        private Genre genre;
        public Genre Genre
        {
            get { return genre; }
            set
            {
                if (value != genre)
                {
                    genre = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Genre"));
                    GetErrorForGenre(value).ContinueWith(task =>
                    {
                        lock (errorList)
                        {
                            errorList["Genre"] = task.Result;
                            ErrorsChanged(this, new DataErrorsChangedEventArgs("Genre"));
                        }
                    });
                }
            }
        }
        Task<List<string>> GetErrorForGenre(Genre value)
        {
            return Task.Factory.StartNew<List<string>>(()=> { return null;});
        }
        #endregion

        #region FirstName
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
                    GetErrorForFirstName(value).ContinueWith(task =>
                    {
                        lock (errorList)
                        {
                            errorList["FirstName"] = task.Result;
                            ErrorsChanged(this, new DataErrorsChangedEventArgs("FirstName"));
                        }
                    });
                }
            }
        }
        Task<List<string>> GetErrorForFirstName(string value)
        {
            return Task.Factory.StartNew<List<string>>(() =>
            {
                return null;
            });
        }

        #endregion

        #region ErrorHandling
        public bool HasErrors
        {
            get
            {
                bool iHavErrors = false;
                foreach (string key in errorList.Keys)
                {
                    if (errorList[key] != null)
                    {
                        iHavErrors = true;
                        break;
                    }
                }
                return iHavErrors;
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
        private Dictionary<String, List<String>> errorList;

        public IEnumerable GetErrors(string propertyName)
        {
            lock (errorList)
            {
                if (errorList.ContainsKey(propertyName))
                {
                    return errorList[propertyName].ToList();
                }
                return null;
            }
        }

        #endregion


        #region NotifyChanges Event Handler
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        public override string ToString()
        {
            return LastName + " " + FirstName + ", " + Genre + ", est né le " + BirthDate.ToString();
        }


    }



}
