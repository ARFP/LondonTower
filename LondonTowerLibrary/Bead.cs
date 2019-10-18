using System;

namespace LondonTowerLibrary
{
    [Serializable]
    public class Bead : IEquatable<Bead>
    {
        public ColorEnum ColorBead { get; set; }


        public Bead()
        {

        }
        public Bead(ColorEnum colorBead)
        {
            this.ColorBead = colorBead;
        }
               

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
