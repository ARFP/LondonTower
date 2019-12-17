using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LondonTowerLibrary.ViewModels
{
    /// <summary>
    /// Classe ViewModel de Personn
    /// Partie métier/ interaction avec L'IHM
    /// Contient les infos sur l'utilisateur actuel
    /// </summary>
    public class PersonneVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        #region Constructor 
        /// <summary>
        /// Constructeur paramétré 
        /// </summary>
        /// <param name="_IsCompleted"></param>
        public PersonneVM(ReturnForm _IsCompleted)
        {
            this.Genre = Genre.Homme;
            this.errorList = new Dictionary<string, List<string>>();
            this.birthDate = default;
            this.IsCompleted = _IsCompleted;
        }
        #endregion
        /// <summary>
        /// Délégué utilisé pour notifier LondonTowerVM qui contient PersonneVM de l'état des erreurs.
        /// </summary>
        /// <param name="IsOk"> True is aucune erreur et tous les champs sont remplus, False sinon</param>
        public delegate void ReturnForm(bool IsOk);
        public event ReturnForm IsCompleted;


        #region Age
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'INotifyPropertyChanged et INotifyDataErrorInfo, toutes les properties deleguent leur gestion à cette methode. 
        /// La région age n'est pas répercutée du coté du model, elle est uniquement présente coté VM pour des raisons pratiques lors des tests d'erreurs et pour l'affichage.
        /// </summary>
        private int age;
        public int Age
        {
            get { return age; }
            set { if (age != value) ThingsGotChanged(nameof(Age), this, age = value); }
        }
        #endregion

        #region LastName
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'INotifyPropertyChanged et INotifyDataErrorInfo, toutes les properties deleguent leur gestion à cette methode. 
        /// réflete la propriété du même nom coté model 
        /// </summary>
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { if (lastName != value) ThingsGotChanged(nameof(LastName), this, lastName = value); }
        }
        #endregion

        #region BirthDate
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// réflete la propriété du même nom coté model 
        /// </summary>
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { if (birthDate != value) ThingsGotChanged(nameof(BirthDate), this, birthDate = value); }
        }
        #endregion

        #region separated Birthdate
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// Séparation en jours/mois/années de la date pour l'affichage, la propriété n'est pas répercuté coté model, <c>Birthdate</c> qui est présent coté model est update à la place.
        /// partie jour.
        /// </summary>
        private string day;
        public string Day
        {
            get { return day; }
            set { if (day != value) ThingsGotChanged(nameof(Day), this, day = value); }

        }

        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// Séparation en jours/mois/années de la date pour l'affichage, la propriété n'est pas répercuté coté model, <c>Birthdate</c> qui est présent coté model est update à la place.
        /// partie mois.
        /// </summary>
        private string month;
        public string Month
        {
            get { return month; }
            set { if (month != value) ThingsGotChanged(nameof(Day), this, month = value); }
        }

        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// Séparation en jours/mois/années de la date pour l'affichage, la propriété n'est pas répercuté coté model, <c>Birthdate</c> qui est présent coté model est update à la place.
        /// partie année.
        /// </summary>
        private string year;
        public string Year
        {
            get { return year; }
            set { if (year != value) ThingsGotChanged(nameof(Day), this, year = value); }
        }
        #endregion


        #region Genre
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// réflete la propriété du même nom coté model 
        /// </summary>
        private Genre genre;
        public Genre Genre
        {
            get { return genre; }
            set { if (value != genre) ThingsGotChanged(nameof(Genre), this, genre = value); }
        }
        #endregion

        #region FirstName
        /// <summary>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// réflete la propriété du même nom coté model 
        /// </summary>
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { if (value != firstName) ThingsGotChanged(nameof(FirstName), this, firstName = value); }
        }
        #endregion

        #region ErrorHandling
        /// <summary>
        /// Fait partie d'<c>INotifyDataErrorInfo</c>
        /// <c>ThingsGotChanged</c> se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode. 
        /// </summary>
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

        /// <summary>
        /// Défaut eventhandler faisant partie  d'<c>INotifyDataErrorInfo</c>
        /// notifie un changement dans la liste d'erreur d' <c>errorList</c>, 
        /// appelé par <c>ThingsGotChanged</c>
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        /// <summary>
        /// liste  d'erreurs pour les différentes propriétés,
        /// le contenu est géré par ThingsGotChanged.
        /// </summary>
        private Dictionary<String, List<String>> errorList;

        /// <summary>
        /// Fait partie d'<c>INotifyDataErrorInfo</c>
        /// est appelée l' IHM pour récupérer une erreur pour un champ précis
        /// </summary>
        /// <param name="propertyName"> Nom de la propriétée que l'on teste pour vérifier si elle possède des erreurs</param>
        /// <returns>le texte associé à toute erreur stocké sous le nom de la propriété passé en paramêtre</returns>
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


        /// <summary>
        /// Se charge de la gestion d'<c>INotifyPropertyChanged</c> et <c>INotifyDataErrorInfo</c>, toutes les properties deleguent leur gestion à cette methode.
        /// Call <c>PropertyChanged</c>, puis call en async <c>GetErrorForProperty</c> uniquement pour les champs qui peuvent posséder des erreurs.
        /// Utilise une promise pour update errorlist et notify ErrorsChanged du résultat rendu par la task GetErrorForProperty
        /// </summary>
        /// <param name="propertyname"> Nom de la propriété qui a reçu un changement de valeur</param>
        /// <param name="sender"> la propriété changée qui s'est envoyée elle même en référence</param>
        /// <param name="value"> la nouvelle valeur de la propriété</param>
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


        /// <summary>
        /// Run en async
        /// chargé de tester la validité de la valeur contenu dans une propriété donnée
        /// le test est effectué sur les modifications de date, du prénom, du nom.
        /// le dictionnaire reste vide dans le cas où la valeur est valide.
        /// simple switchcase pour effectuer un test spécific à chaque propriété.
        /// Pour la date, le test est uniquement effectué si tous les champs <c>Day</c>, <c>Year</c>, <c>Month</c> sont remplis,
        /// et le champ <c>Birthdate</c>  et <c>Age</c> est update dans le cas où la date est une date valide.
        /// </summary> 
        /// <param name="value"> valeur de la propriété modifiée</param>
        /// <param name="property">nom de la propriété</param>
        /// <returns>un dictionnaire contenant la liste clé-> valeur (nom de la propriété -> string pour l'énnoncé de l'erreur)</returns>
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
        /// <summary>
        /// Implémente <c>INotifyPropertyChanged</c>
        /// event déclenché lors d'une modification de valeur, appelé directement par le setter des propriétés
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion



        #region Implicit Operators
        /// <summary>
        /// Implicit Operator ViewModel -> Model
        /// </summary>
        /// <param name="pvm">View Model pour personne</param>
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




    }



}
