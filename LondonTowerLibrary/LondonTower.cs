using System;
using System.Collections.Generic;

namespace LondonTowerLibrary
{
    [Serializable]
    public class LondonTower
    {
        
        private int level;
       
        private List<Trial> trialList;
        private int trialNumber;
        private Personn personn;
        private bool visualHelp;
        private DateTime dateAndTime;
        private string fdback;

        /// <summary>
        /// Entier représentant le niveau de difficulté correspondant aux nombres de Pegs (3,4 ou 5)
        /// </summary>
        public int Level { get => level; set => level = value; }

        /// <summary>
        /// Liste contenant tous les Trials d'un niveau de difficulté
        /// </summary>
        public List<Trial> TrialList { get => trialList; set => trialList = value; }

        /// <summary>
        /// Object contenant les informations de l'utilisateurs actuel
        /// </summary>
        public Personn Personn { get => personn; set => personn = value; }

        /// <summary>
        /// Booleen représentant l'option d'aide visuel modifiant le rendu visuel des Beads 
        /// </summary>
        public bool VisualHelp { get => visualHelp; set => visualHelp = value; }

        /// <summary>
        /// Date et Heure du lancement du test London Tower
        /// </summary>
        public DateTime DateAndTime { get => dateAndTime; set => dateAndTime = value; }

        /// <summary>
        /// String contenant le ressenti de l'utilisateur à la fin du test LondonTower
        /// </summary>
        public string Fdback { get => fdback; set => fdback = value; }

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public LondonTower()
        {
            trialNumber = 0;
        }

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="_level">Entier correspondant au nombre de Peg pour le test (3,4 ou5)</param>
        /// <param name="_trialList">Liste des 11 Trial (demonstration comprise) pour le test LondonTower</param>
        /// <param name="_personn">Instance de Personn contenant les informations de l'utilisateur</param>
        public LondonTower(int _level, List<Trial> _trialList, Personn _personn) : this()
        {
            this.level = _level;
            this.trialList = _trialList;
            this.Personn = _personn;
        }

        /// <summary>
        /// Verifie la présence d'un Trial suivant et le renvoi
        /// </summary>
        /// <returns>retourne le Trial suivant, ou null si pas de suivant</returns>
        public Trial GetNextTrial()
        {
            if (HastNextTrial())
            {
                Trial t = trialList[trialNumber];
                trialNumber++;
                return t;
            }
            return null;
        }

        /// <summary>
        /// Verifie la présence d'un niveau suivant
        /// </summary>
        /// <returns>Booleen à True s'il y a un Trial suivant, False si non</returns>
        public bool HastNextTrial()
        {
            if (trialList.Count > this.trialNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
