using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace LondonTowerLibrary.Button
{
    /// <summary>
    /// Logique d'interaction pour WoodRadioUC.xaml
    /// Radio custom permetant d'utiliser une image de notre choix au lieu de l'object graphique par défaut.
    /// </summary>
    public partial class WoodRadioUC : UserControl
    {
        /// <summary>
        /// Defaut URI racine pour les ressources "image" utilisées. 
        /// </summary>
        private const string uriPart = "pack://application:,,,/LondonTowerLibrary;Component/Resources/woodRadio";

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public WoodRadioUC()
        {
            InitializeComponent();
        }


        #region GroupName
        /// <summary>
        /// Partie propriété de la DependencyProperty GName.
        /// Permet de set le GroupName pour le  RadioButton
        /// </summary>
        public string GName
        {
            get { return (string)GetValue(GNameProperty); }
            set { SetValue(GNameProperty, this.MyRadio.GroupName = value); }
        }

        /// <summary>
        /// Dependency Property pour GName;
        /// Permet de set le GroupName pour le RadioButton
        /// </summary>
        public static readonly DependencyProperty GNameProperty = DependencyProperty.Register("GName", typeof(string), typeof(WoodRadioUC), new PropertyMetadata(null));
        #endregion

        #region Radio Value
        /// <summary>
        /// Set la valeur du RadioButton. DependencyProperty.
        /// </summary>
        public string RadValue
        {
            get { return (string)GetValue(RadValueProperty); }
            set
            {
                SetValue(RadValueProperty, value);
            }
        }
        /// <summary>
        /// Set la valeur du RadioButton.
        /// Declaration de la DepencyProperty
        /// </summary>
        public static readonly DependencyProperty RadValueProperty = DependencyProperty.Register(
            "RadValue", typeof(string), typeof(WoodRadioUC), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region Image Sources
        private ImageSource StoredImageSource;
        /// <summary>
        /// 
        /// </summary>
        public ImageSource SelectedImageSource
        {
            get { return (ImageSource)GetValue(SelectedImageSourceProperty); }
            set { SetValue(SelectedImageSourceProperty, value); }
        }
        public static readonly DependencyProperty SelectedImageSourceProperty = DependencyProperty.Register(
                "SelectedImageSource",
                typeof(ImageSource),
                typeof(WoodRadioUC),
                new UIPropertyMetadata(null));


        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set
            {
                SetValue(ImageSourceProperty, value);
                if (StoredImageSource == default)
                    StoredImageSource = value;
            }
        }
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                "ImageSource",
                typeof(ImageSource),
                typeof(WoodRadioUC),
                new UIPropertyMetadata(null));
        #endregion




        #region BEHAVIOURS
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SwapImage(true);
            WhyThou?.Invoke(this, e);
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SwapImage(false);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(RadValue))
            {
                SelectedImageSource = new BitmapImage(new Uri(uriPart + RadValue + "s.png"));
                ImageSource = new BitmapImage(new Uri(uriPart + RadValue + ".png"));
                StoredImageSource = ImageSource;
            }
        }
        private void SwapImage(bool SelectedState)
        {
            if (!String.IsNullOrEmpty(RadValue))
            {
                var img = new Image
                {
                    Source = (SelectedState) ? SelectedImageSource : StoredImageSource
                };
                ImageSource = img.Source;
            }
        }
        #endregion

        #region Foolish clicker!
        public delegate void WhyThouClikedThereFool(object sender, RoutedEventArgs e);
        public event WhyThouClikedThereFool WhyThou;
        public static readonly DependencyProperty WhyThouProperty = DependencyProperty.Register("WhyThou", typeof(Delegate), typeof(WoodRadioUC), new PropertyMetadata(null));
        #endregion
    }
}
