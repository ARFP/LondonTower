using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LondonTowerLibrary.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UCResultGlobal.xaml
    /// </summary>
    public partial class UCResultGlobal : UserControl
    {/// <summary>
    /// UserControl servant à l'affichage des informations lié au test LondonTower passé par l'utilisateur.
    /// Tel que les temps, les nombres de coups etc....
    /// </summary>
        public UCResultGlobal()
        {
            InitializeComponent();
        }
    }
}
