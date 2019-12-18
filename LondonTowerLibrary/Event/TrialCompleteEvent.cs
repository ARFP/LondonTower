using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonTowerLibrary.Event
{
    /// <summary>
    /// Evenement Custom servant à prevenir par evenement que le Trial est résolu
    /// </summary>
    public class TrialCompleteEvent : EventArgs
    {
        private bool complete;

        public TrialCompleteEvent(bool _complete)
        {
            this.complete = _complete;
        }

        public bool Complete
        {
            get
            {
                return complete;
            }
        }

    }
}
