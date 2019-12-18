using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.Button
{
    /// <summary>
    /// Logique d'interaction pour WoodButtonUc.xaml
    /// Bouton custom permetant d'utiliser une image de notre choix au lieu de l'object graphique par défaut.
    /// </summary>
    public partial class WoodButtonUc : UserControl
    {
        /// <summary>
        /// Defaut URI racine pour les ressources "image" utilisées. 
        /// </summary>
        public const string uriPart = "pack://application:,,,/LondonTowerLibrary;Component/Resources/";

        /// <summary>
        /// Partie propriété de la Dependency Property. Sert à accéder à la valeur IsEnabled du de l'objet Button  .
        /// </summary>
        public bool MAGICEnabled
        {
            get { return (bool)GetValue(MAGICEnabledProperty); }
            set
            {
                if (value != MAGICEnabled)
                {
                    SetValue(MAGICEnabledProperty, value);
                }
            }
        }

        /// <summary>
        ///  Partie déclaration de la dependency property MagicEnabled. 
        /// </summary>
        public static readonly DependencyProperty MAGICEnabledProperty = DependencyProperty.Register("MAGICEnabled", typeof(bool), typeof(WoodButtonUc),
                         new FrameworkPropertyMetadata
                         {
                             DefaultValue = false,
                             DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                             BindsTwoWayByDefault = true
                         });


        #region Image Name
        /// <summary>
        /// Partie propriété de la dependency property ImgName, sert à définir quelle image attribuer par défaut à l'objet.  
        /// </summary>
        public string ImgName
        {
            get { return (string)GetValue(ImgNameProperty); }
            set { SetValue(ImgNameProperty, value); }
        }

        /// <summary>
        /// Partie déclaration de la DependencyProperty de ImgName.
        /// </summary>
        public static readonly DependencyProperty ImgNameProperty = DependencyProperty.Register("ImgName", typeof(string), typeof(WoodButtonUc),
                 new FrameworkPropertyMetadata
                 {
                     DefaultValue = null,
                     DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                     BindsTwoWayByDefault = true
                 });
        #endregion
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public WoodButtonUc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Stocke l'URI à utiliser lors de l'event Hover lorsque MAGICEnabled vaut true. 
        /// Est set automatiquement on load à partir du paramètre ImgName et uriPart.
        /// </summary>
        private ImageSource IsEnabledImgSource;
        /// <summary>
        /// Stocke l'URI à utiliser lors de l'event Hover lorsque MAGICEnabled vaut false. 
        /// Est set automatiquement on load à partir du paramètre ImgName et uriPart.
        /// </summary>
        private ImageSource IsNotEnabledImgSource;

        /// <summary>
        /// Partie propriété de la dependency property ImgSource, sert à définir l'URI de l'image attribuée par défaut à l'objet.  
        /// </summary>
        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set
            {
                SetValue(ImgSourceProperty, value);
            }
        }
        /// <summary>
        /// Obsolete : Utiliser la DependencyProperty <c>ImgName</c> à la place pour set la source de l'image.
        /// Déclaration de la DependencyProperty pour la propriété ImgSource.         
        /// </summary>
        public static readonly DependencyProperty ImgSourceProperty = DependencyProperty.Register(
                "ImgSource",
                typeof(ImageSource),
                typeof(WoodButtonUc),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = null,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    BindsTwoWayByDefault = true
                });

        /// <summary>
        /// Déclaration de l'URI pour l'image qui sera affichée lors de l'even Hover. Est changé dynamiquement par AttributeImageSource 
        /// après vérification de la valeur de MAGICEnabled.
        /// </summary>
        public ImageSource SelectedImgSource
        {
            get { return (ImageSource)GetValue(SelectedImgSourceProperty); }
            set
            {
                SetValue(SelectedImgSourceProperty, value);

            }
        }
        /// <summary>
        /// Obsolete : Utiliser la DependencyProperty <c>ImgName</c> à la place pour set la source de l'image par défaut, l'URI de SelectedImgSource changera automatiquement.
        /// Pour que le changement automatique fonctionne, nécessité pour ces ressources d'avoir un template d'URI 
        /// nomdelimg.png (non hover)- nomdelimgok.png (hover- Enabled) - nomdelimgko.png (hover- disabled)
        /// </summary>
        public static readonly DependencyProperty SelectedImgSourceProperty = DependencyProperty.Register(
                "SelectedImgSource",
                typeof(ImageSource),
                typeof(WoodButtonUc),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = null,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    BindsTwoWayByDefault = true
                });


        /// <summary>
        /// Déclaration du RoutedEventHandler Click. 
        /// </summary>
        public event RoutedEventHandler Click;

        /// <summary>
        /// Appelée par l'evenement CLick sur le Button de l'UC.
        /// Si MAGICEnabled vaut true la méthode Invoke la méthode attribué au RoutedEventHandler Click. Ne fait rien si MAGICENabled vaut false. 
        /// </summary>
        /// <param name="sender">button sender</param>
        /// <param name="e">information sur l"event click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MAGICEnabled)
                Click?.Invoke(this, e);
        }


        /// <summary>
        /// Appelé par l'event Loaded de l'UC lui même.
        /// Set les URI d'images pour les images affichés par défaut + les deux possibilités d'affichage lors de l'event Hover. 
        /// Necessité pour ces ressources d'avoir un template d'URI nomdelimg.png (non hover)- nomdelimgok.png (hover- Enabled) - nomdelimgko.png (hover- disabled)
        /// Appelle la méthode AttributeImageSource pour la première attribution du status Is Enabled/Is Not Enabled de l'UC.
        /// </summary>
        /// <param name="sender">l'objet UC lui même</param>
        /// <param name="e">informations sur l'event Loaded</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ImgName))
            {
                ImgSource = new BitmapImage(new System.Uri(uriPart + ImgName + ".png"));
                IsEnabledImgSource = new BitmapImage(new System.Uri(uriPart + ImgName + "ok.png"));
                IsNotEnabledImgSource = new BitmapImage(new System.Uri(uriPart + ImgName + "ko.png"));
                AttributeImageSource(MAGICEnabled);
            }
        }
        /// <summary>
        /// swap l'image affichée par l'UC lors du hover (SelectedImgSource) entre les URL d'images IsEnabled ou IsNotEnabled en fonction du parametre boolean entré.
        /// </summary>
        /// <param name="value">boolean</param>
        private void AttributeImageSource(bool value)
        {
            SelectedImgSource = (value) ? IsEnabledImgSource : IsNotEnabledImgSource;
        }

        /// <summary>
        /// Call AttributeImageSource lorsque l'evenement MouseEnter est détecté au niveau de l'UC. Lui passe en paramètre l'état activé/Non activé de l'UC
        /// </summary>
        /// <param name="sender">UC sender</param>
        /// <param name="e">détails de l'event</param>
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AttributeImageSource(MAGICEnabled);
        }

    }  
}
