using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonTowerLibrary.Event
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {/// <summary>
    /// Implémentation de l'interface INotifyPropertyChanged
    /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperty_Change(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
