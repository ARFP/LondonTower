using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace LondonTowerLibrary
{/// <summary>
/// Classe Metier Peg representant un pique dans le jeu LondonTower
/// </summary>
    [Serializable]
    public class Peg : IEquatable<Peg>
    {

        private int pegNumber;
        /// <summary>
        /// Entier representant le numero du Peg pour le placement.
        /// Placement de droite a gauche, du plus petit (Peg 1) au plus grand (Peg 3 ou 4 ou 5 suivant le nombre de Peg choisi)
        /// </summary>
        public int PegNumber { get => pegNumber; set => pegNumber = value; }


       
        private ObservableCollection<Bead> beadList;
        /// <summary>
        /// Liste de Bead empilé sur le Peg
        /// </summary>
        public ObservableCollection<Bead> BeadList {
            get {
                return beadList;
            }
            set {
                beadList = value;
            }
        }

        public Peg()
        {
            BeadList = new ObservableCollection<Bead>();
        }

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="_PegNumber">le numero du Peg dans la zone</param>
        public Peg(int _PegNumber) : this()
        {
            this.PegNumber = _PegNumber;

        }

        /// <summary>
        /// Ajout d'un Bead à la liste <c>BeadList</c> après verification de la possibilité d'ajout
        /// </summary>
        /// <param name="_b">Bead à ajouter a la liste de Bead</param>
        /// <returns>True si ajout effectué, False dans le cas contraire</returns>
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

        /// <summary>
        /// Retrait d'un Bead de la liste <c>BeadList</c> àmres verification de la possibilité
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Verifie la possibilité d'ajouter un Bead a la liste
        /// </summary>
        /// <returns>True si ajout possible, False si ajout impossible</returns>
        public bool CanAdd()
        {
            if (BeadList.Count < this.PegNumber)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifie la possibilité d'enlever un Bead à la liste
        /// </summary>
        /// <returns>True si ajout possible, False si ajout impossible</returns>
        public bool CanRemove()
        {
            if (BeadList.Count > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Implémentation de l'interface IEquatable<Peg> permettant de définir les critère d'égalité
        /// les critères d'égalité sont la taille de la liste <c>BeadList.Count</c> et l'égalité des Beads à chaque position 
        /// </summary>
        /// <param name="other">Peg à comparer avec l'instance actuelle</param>
        /// <returns>retourne TRUE si les 2 Pegs sont identiques, sinon retourne FALSE</returns>
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
