using LondonTowerLibrary.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LondonTowerLibrary
{/// <summary>
/// Classe Metier representant 1 niveau sur les 10 du London tower
/// </summary>
    [Serializable]
    public class Trial
    {
        /// <summary>
        /// Entier représentant le numero du trial actuel
        /// </summary>
        private int trialNumber;

        /// <summary>
        /// Entier représentant le nombre de coup minimal pour résoudre le Trial actuel
        /// </summary>
        private int minimalMoveExpect;

        /// <summary>
        /// Entier représentant le nombre de mouvement effectué
        /// </summary>
        private int tryMoveMade;

        /// <summary>
        /// Entier représentant le nombre de seconde écoulé entre le 1er mouvement et la résolution du Trial
        /// </summary>
        private int timeToResolve;

        /// <summary>
        /// Entier représentant le nombre de seconde écoulé entre le chargement du Trial et sa résolution
        /// </summary>
        private int timeDisplayUntilResolve;

        /// <summary>
        /// La date et heure du 1er mouvement effectué sur un Bead dans la zone de travail
        /// </summary>
        private DateTime TimeFirstMoveTrial;

        /// <summary>
        /// La date et heure à laquel le Trial a été résolu
        /// </summary>
        private DateTime TimeToResolveTrial;

        /// <summary>
        /// La date et heure à laquel le Trial a été chargé et affiché
        /// </summary>
        private DateTime TimeDisplaytrial;

        /// <summary>
        /// Liste des Pegs pour la zone de travail avec placement initial des Beads
        /// </summary>
        private ObservableCollection<Peg> pegList;

        /// <summary>
        /// Liste des Pegs pour la zone Goal avec placement final des Beads
        /// </summary>
        private ObservableCollection<Peg> pegListGoal;

        /// <summary>
        /// Accesseur de pegListGoal
        /// </summary>
        public ObservableCollection<Peg> PegListGoal { get => pegListGoal; set => pegListGoal = value; }

        /// <summary>
        /// Accesseur de minimalMoveExpect 
        /// </summary>
        public int MinimalMoveExpect { get => minimalMoveExpect; set => minimalMoveExpect = value; }

        /// <summary>
        /// Accesseur de trialNumber
        /// </summary>
        public int TrialNumber { get => trialNumber; set => trialNumber = value; }

        /// <summary>
        /// Accesseur de timeToResolve
        /// </summary>
        public int TimeToResolve { get => timeToResolve; set => timeToResolve = value; }

        /// <summary>
        /// Accesseur de timeDisplayUntilResolve
        /// </summary>
        public int TimeDisplayUntilResolve { get => timeDisplayUntilResolve; set => timeDisplayUntilResolve = value; }

        /// <summary>
        /// Accesseur de tryMoveMade
        /// Declenche l'evenement TryMoveMadeChange 
        /// </summary>
        public int TryMoveMade
        {
            get { return tryMoveMade; }
            set
            {
                tryMoveMade = value;
                if (tryMoveMade > 0)
                {
                    TryMovemade_OnChange(nameof(TryMoveMade));
                }

            }
        }

        /// <summary>
        /// Accesseur de pegList
        /// </summary>
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

        /// <summary>
        /// Delegate pour notifier au ViewTrial la résolution du Trial 
        /// </summary>
        /// <param name="sender">l'instance actuel de Trial</param>
        /// <param name="args">Evenement representant la reussite du test acceptant un booleen à True si le test est complété</param>
        public delegate void TrialCompleteEventHandler(object sender, TrialCompleteEvent args);
        public event TrialCompleteEventHandler OnComplete;

        /// <summary>
        /// Delegate pour notifier au ViewTrial qu'un mouvement a été realiser
        /// </summary>
        /// <param name="_NameProperty">Nom de la propriété declenchent l'evenement</param>
        public delegate void TryMoveMadeChange(string _NameProperty);
        public event TryMoveMadeChange TryMovemade_OnChange;

        /// <summary>
        /// Constructeur paramétré appelant le constructeur par defaut
        /// </summary>
        /// <param name="trialNumber">Entier correspondant au numéro du niveau actuel </param>
        /// <param name="minimalMoveExpect">Entier correspondant au nombre minimal de mouvement pour resoudre se Trial</param>
        public Trial(int trialNumber, int minimalMoveExpect) : this()
        {
            TrialNumber = trialNumber;
            MinimalMoveExpect = minimalMoveExpect;
        }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Trial()
        {
            TryMoveMade = 0;
        }

        /// <summary>
        /// Enregistrement de la Date et Heure du debut du Trial effectué au chargement de l'affichage
        /// </summary>
        public void StartTimeDisplayTrial()
        {
            TimeDisplaytrial = DateTime.Now;
        }

        /// <summary>
        /// Enregistrement de la date et heure du debut de la résolution du Trial, apres le 1er mouvement
        /// </summary>
        public void StartTrial()
        {
            TimeFirstMoveTrial = DateTime.Now;
        }

        /// <summary>
        /// Enregistrement de la date et heure à laquel le Trial a été résolu
        /// </summary>
        public void EndTrial()
        {
            TimeToResolveTrial = DateTime.Now;
        }

        /// <summary>
        /// Récuperation du temps écoulé entre <c>TimeFirstMoveTrial</c> date/heure du premier mouvement et <c>TimeToResolveTrial</c>
        /// date/heure de résolution du trial
        /// </summary>
        /// <returns>Temps écoulé en secondes</returns>
        public int GetTimeToResolve()
        {
            TimeSpan time = TimeToResolveTrial - TimeFirstMoveTrial;
            return (int)time.TotalSeconds;
        }

        /// <summary>
        /// Récuperation du temps écoulé entre <c>TimeDisplaytrial</c> date/heure du chargement de la page et <c>TimeToResolveTrial</c>
        /// date/heure de résolution du trial
        /// </summary>
        /// <returns>Temps écoulé en secondes</returns>
        public int GetTimeUntilLoad()
        {
            TimeSpan time = TimeToResolveTrial - TimeDisplaytrial;
            return (int)time.TotalSeconds;
        }

        /// <summary>
        /// Incrémentation du nombre de mouvement
        /// </summary>
        public void MoveMade()
        {
            TryMoveMade++;
        }

        /// <summary>
        /// Mouvement d'un Bead d'un Peg à un autre Peg puis appel de la fonction <c>FinishTrial()</c> pour la verification de résolution du Trial
        /// </summary>
        /// <param name="_from">Peg d'origine du Bead</param>
        /// <param name="_to">Peg cible du mouvement</param>
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

        /// <summary>
        /// Verification de la résolution du Trial.
        /// Si le Trial est résolu, sauvegarde des temps <c>TimeDisplayUntilResolve</c> resultat de la fonction <c>GetTimeUntilLoad()</c>
        /// et <c>TimeToResolve</c> resultat de la fonction <c>GetTimeToResolve()</c>.
        /// Declenchement de l'evenement TrialCompleteEventHandler pour notifier la résolue du Trial
        /// </summary>
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
