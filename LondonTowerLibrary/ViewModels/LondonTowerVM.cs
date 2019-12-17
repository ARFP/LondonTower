using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LondonTowerLibrary.ViewModels
{
    /// <summary>
    /// Classe ViewModel de LondonTower
    /// Partie métier/ interaction avec L'IHM
    /// Contient les infos sur la partie en cours de LondonTower, ainsi qu'un viewmodel de Personne remplie avec les infos utilisateur
    /// </summary>
    public class LondonTowerVM :  INotifyPropertyChanged
    {

        #region Constructor
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public LondonTowerVM()
        {
            this.Personne = new PersonneVM(SetFormCompleted);
            this.VisualHelp = false;
            this.DateAndTime = new DateTime();
            RunTimer();
        }

        #endregion


        #region pegs
        /// <summary>
        /// Nombre de tiges sélectionné pour le test, valeur appartient à {3,4,5}
        /// </summary>
        public int NbPegs { get; set; }
        #endregion

        #region DateAndTime 
        /// <summary>
        /// DateTime prise au lancement du test.
        /// s'update toutes les minute via <c>RunTimer()</c> et notifie l'IHM de la modification via <c>PropertyChanged</c> 
        /// </summary>
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

        /// <summary>
        /// async Timer qui update DateAndTime tant que le test n'a pas été démarré. 
        /// </summary>
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
        /// <summary>
        /// PersonneVM contient tous les données de l'utilisateur passant le test. Trigger <c>PropertyChanged</c>
        /// </summary>
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
        /// <summary>
        /// Enregistre le choix de l'utilisateur pour l'utilisation des aides visuelles. 
        /// Les changements trigger PropertyChanged
        /// </summary>
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

        /// <summary>
        /// Fonction passée en argument lors de la création d'un PersonneVM. 
        /// Permet de retourner l'état actuel de PersonneVM au niveau des erreurs
        /// </summary>
        /// <param name="isOk"></param>
        public void SetFormCompleted(bool isOk)
        {
            FormCompleted = isOk;
        }

        /// <summary>
        /// Boolean pour l'état de complétude des infos utilisateurs. 
        /// Vaut true si tous les champs sont remplis et sans erreur, false sinon. 
        /// trigger PropertyChanged.
        /// </summary>
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
        /// <summary>
        /// Stocke le retour de l'utilisateur sur sa performance qui lui est demandé sur la page IHM "FeedBack".
        /// </summary>
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
        /// <summary>
        /// event implemente <c>INotifyPropertyChanged</c> 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Implicit Operators
        /// <summary>
        /// Implicit operator ViewModel -> Model
        /// </summary>
        /// <param name="ltvm"> ViewModel </param>
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

