using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LondonTowerLibrary.ViewModels
{
    public class PersonneVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Constructor 
        public PersonneVM(ReturnForm _IsCompleted)
        {
            this.Genre = Genre.Homme;
            this.errorList = new Dictionary<string, List<string>>();
            this.birthDate = default;
            this.IsCompleted = _IsCompleted;
        }
        #endregion

        public delegate void ReturnForm(bool IsOk);
        public event ReturnForm IsCompleted;

        // this region is only in dataview for convenience purpose / 
        #region Age 
        private int age;
        public int Age
        {
            get { return age; }
            set { if (age != value) ThingsGotChanged("Age", this, age = value); }
        }
        #endregion

        #region LastName
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { if (lastName != value) ThingsGotChanged("LastName", this, lastName = value); }
        }
        #endregion

        #region BirthDate
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { if (birthDate != value) ThingsGotChanged("BirthDate", this, birthDate = value); }
        }
        #endregion

        #region Genre
        private Genre genre;
        public Genre Genre
        {
            get { return genre; }
            set { if (value != genre) ThingsGotChanged("Genre", this, genre = value); }
        }
        #endregion

        #region FirstName
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { if (value != firstName) ThingsGotChanged("FirstName", this, firstName = value); }
        }
        #endregion

        #region ErrorHandling
        public bool HasErrors
        {
            get
            {
                bool iHavErrors = false;
                lock (errorList)
                {
                    if (String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(firstName) || birthDate == default)
                        iHavErrors = true;

                    foreach (string key in errorList.Keys)
                    {
                        if (errorList[key] != null)
                        {
                            iHavErrors = true;
                            break;
                        }
                    }
                }
                this.IsCompleted(!iHavErrors);
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
                    return errorList[propertyName];
                }
                return null;
            }
        }
        private void ThingsGotChanged(string propertyname, object sender, object value)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(propertyname));

            if (propertyname == "Day" || propertyname == "FirstName" || propertyname == "LastName")
                GetErrorForProperty(value, propertyname).ContinueWith(task =>
                {
                    lock (errorList)
                    {
                        errorList[propertyname] = (task.Result.ContainsKey(propertyname)) ? task.Result[propertyname] : null;
                        ErrorsChanged(sender, new DataErrorsChangedEventArgs(propertyname));
                    }
                });
        }
        Task<Dictionary<string, List<string>>> GetErrorForProperty(object value, string property)
        {
            return Task.Factory.StartNew<Dictionary<string, List<string>>>(() =>
            {
                var listOfStuff = new Dictionary<string, List<string>> { };
                switch (property)
                {
                    case "Day":
                        if (!string.IsNullOrEmpty(this.Day) && !string.IsNullOrEmpty(this.Month) && !string.IsNullOrEmpty(this.Year))
                        {
                            if (DateTime.TryParse(this.Day + "/" + this.Month + "/" + this.Year, out DateTime date))
                            {
                                var now = DateTime.Now;
                                this.Age = now.Year - date.Year;
                                this.Age = (date.Date > now.AddYears(-age)) ? --Age : Age;
                                if (age > 120 || age < 0)
                                    listOfStuff.Add("Day", new List<string> { " La date de naissance ne donne pas un âge valide" });
                                this.BirthDate = date;
                            }
                            else
                                listOfStuff.Add("Day", new List<string> { " La date de naissance n'est pas valide" });
                        }
                        else
                            listOfStuff.Add("Day", new List<string> { " La date de naissance n'est pas valide" });
                        break;
                    case "FirstName":
                        if (!Regex.IsMatch((string)value, @"^[a-zA-Zéèçëê\s\-]{2,}$"))
                            listOfStuff.Add("FirstName", new List<string> { "Le prénom ne peut contenir que des lettres, des espaces et des tirets" });
                        break;
                    case "LastName":
                        if (!Regex.IsMatch((string)value, @"^[a-zA-Zéèçëê\s\-]{2,}$"))
                            listOfStuff.Add("LastName", new List<string> { "Le nom ne peut contenir que des lettres, des espaces et des tirets" });
                        break;
                    default:
                        break;
                }
                return listOfStuff;
            });
        }
        #endregion


        #region NotifyChanges Event Handler
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion



        #region Implicit Operators
        public static implicit operator Personn(PersonneVM pvm)
        {
            return new Personn
            {
                LastName = pvm.LastName,
                FirstName = pvm.FirstName,
                Genre = pvm.Genre,
                DayofBirth = pvm.BirthDate
            };
        }
        #endregion


        #region separated Birthdate 
        private string day;
        public string Day
        {
            get { return day; }
            set { if (day != value) ThingsGotChanged("Day", this, day = value); }

        }
        private string month;
        public string Month
        {
            get { return month; }
            set { if (month != value) ThingsGotChanged("Day", this, month = value); }
        }

        private string year;
        public string Year
        {
            get { return year; }
            set { if (year != value) ThingsGotChanged("Day", this, year = value); }
        }
        #endregion


    }



}
