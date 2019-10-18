using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LondonTowerLibrary.Button
{
    /// <summary>
    /// Logique d'interaction pour WoodButtonUc.xaml
    /// </summary>
    public partial class WoodButtonUc : UserControl
    {
        public WoodButtonUc()
        {
            InitializeComponent();
        }
        
        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceDependencyProperty); }
            set { SetValue(ImgSourceDependencyProperty, value);}
        }


        public static readonly DependencyProperty ImgSourceDependencyProperty = DependencyProperty.Register(
            "ImgSource", typeof(ImageSource), typeof(WoodButtonUc), new UIPropertyMetadata(null));


        public event RoutedEventHandler Click;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Click!= null)
            {
                Click(this, e);
            }
        }

    }  
}
