using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LondonTower
{
    public class CommandNavigate : ICommand
    {
        Page nextPage;
        NavigationService nav;

        public CommandNavigate(NavigationService _nav, Page _nextpage)
        {
            this.nav = _nav;
            this.nextPage = _nextpage;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.nav.Navigate(nextPage);
        }


    }
}
