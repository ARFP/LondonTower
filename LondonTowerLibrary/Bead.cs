using System;

namespace LondonTowerLibrary
{/// <summary>
/// Classe Metier Bead representant une perle pour le test LondonTower
/// </summary>
    [Serializable]
    public class Bead : IEquatable<Bead>
    {
        /// <summary>
        /// Couleur de Bead provenant d'un Enum <c>ColorEnum</c>
        /// </summary>
        public ColorEnum ColorBead { get; set; }

        /// <summary>
        /// Constructeur paramétré 
        /// </summary>
        /// <param name="colorBead">Couleur de Bead</param>
        public Bead(ColorEnum colorBead)
        {
            this.ColorBead = colorBead;
        }

        /// <summary>
        /// Implementation de l'interface <c>IEquatable<Bead></c> servant au définir le critère d'égalité entre 2 Bead
        /// </summary>
        /// <param name="other">Bead à comaprer avec l'instance actuelle</param>
        /// <returns>TRUE si les 2 bead on la même couleur, False sinon</returns>
        public bool Equals(Bead other)
        {
            if (this.ColorBead == other.ColorBead)
            {
                return true;
            }
            return false;
        }
    }
}
