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
        /// Set la valeur du RadioButton ET le nom du radiobutton utilisé pour construire l'URI. DependencyProperty.
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
        /// <summary>
        ///  Stockage de l'URI de l'image affiché par défaut (lorsque le radiobutton est unchecked).
        /// </summary>
        private ImageSource StoredImageSource;
        /// <summary>
        /// URI de l'image affiché lors de l'event hover et lorsque le radiobutton est checked; DependencyProperty
        /// </summary>
        public ImageSource SelectedImageSource
        {
            get { return (ImageSource)GetValue(SelectedImageSourceProperty); }
            set { SetValue(SelectedImageSourceProperty, value); }
        }
        /// <summary>
        /// Obsolete, donner à l'image un nom identique à sa value et attribuer directement le nom via la dependencyProperty <c>RadValue</c>;
        /// URI de l'image affiché lors de l'event hover et lorsque le radiobutton est checked;
        /// DependencyProperty
        /// </summary>
        public static readonly DependencyProperty SelectedImageSourceProperty = DependencyProperty.Register(
                "SelectedImageSource",
                typeof(ImageSource),
                typeof(WoodRadioUC),
                new UIPropertyMetadata(null));

        /// <summary>
        /// URI de l'image affichée actuellement;
        /// DependencyProperty
        /// </summary>
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
        /// <summary>
        /// Obsolete, donner à l'image un nom identique à sa value et attribuer directement le nom via la dependencyProperty <c>RadValue</c>;
        /// URI de l'image affichée actuellement;
        /// DependencyProperty
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                "ImageSource",
                typeof(ImageSource),
                typeof(WoodRadioUC),
                new UIPropertyMetadata(null));
        #endregion




        #region BEHAVIOURS
        /// <summary>
        /// Event déclanché lorsque le radiobutton est coché
        /// </summary>
        /// <param name="sender">UC sender</param>
        /// <param name="e">Event déclanché lorsque le radiobutton est décoché</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SwapImage(true);
            WhyThou?.Invoke(this, e);
        }
        /// <summary>
        /// Event déclanché lorsque le radiobutton est décoché
        /// </summary>
        /// <param name="sender">UC sender</param>
        /// <param name="e">paramètres de l'event Unchecked</param>
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SwapImage(false);
        }
        /// <summary>
        /// Appelé lorsque l'UC est chargé;
        /// Construit les URI pour l'image affichée actuellement ainsi que l'image lorsque le radiobutton est checked et Hover, et stocke l'image par défaut
        /// </summary>
        /// <param name="sender">UC sender</param>
        /// <param name="e"> info event Loaded</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(RadValue))
            {
                SelectedImageSource = new BitmapImage(new Uri(uriPart + RadValue + "s.png"));
                ImageSource = new BitmapImage(new Uri(uriPart + RadValue + ".png"));
                StoredImageSource = ImageSource;
            }
        }
        /// <summary>
        /// Méthode appelée lorsque le radiobutton est check ou uncheck;
        /// Change en conséquence l'URI utilisé pour ImageSource (URI de l'image affichée) entre SelectedImageSource (Checked) et StoredImageSource (unchecked)
        /// </summary>
        /// <param name="SelectedState"> Status IsChecked</param>
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
        /// <summary>
        /// Declaration du delegate servant en DependencyProperty;
        /// Utilisé pour attribuer un comportement à l'event Click de l'UC 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void WhyThouClikedThereFool(object sender, RoutedEventArgs e);
        /// <summary>
        /// Event Delegate de type WhyThouClikedThereFool;
        /// Utilisé pour attribuer un comportement à l'event Click de l'UC 
        /// </summary>
        public event WhyThouClikedThereFool WhyThou;
        /// <summary>
        ///  Dependency property prenant un delegate de signature void method (object, RoutedEventsArgs);
        ///  Utilisé pour attribuer un comportement à l'event Click de l'UC 
        /// </summary>
        public static readonly DependencyProperty WhyThouProperty = DependencyProperty.Register("WhyThou", typeof(Delegate), typeof(WoodRadioUC), new PropertyMetadata(null));
        #endregion
    }
}
