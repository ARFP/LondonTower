using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LondonTowerLibrary.Button
{
    /// <summary>
    /// Logique d'interaction pour WoodButtonUc.xaml
    /// </summary>
    public partial class WoodButtonUc : UserControl
    {
        public const string uriPart = "pack://application:,,,/LondonTowerLibrary;Component/Resources/";


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


        //public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(WoodButtonUc), new UIPropertyMetadata(false));
        public static readonly DependencyProperty MAGICEnabledProperty = DependencyProperty.Register("MAGICEnabled", typeof(bool), typeof(WoodButtonUc),
                         new FrameworkPropertyMetadata
                         {
                             DefaultValue = false,
                             DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                             BindsTwoWayByDefault = true
                         });


        #region Image Name
        public string ImgName
        {
            get { return (string)GetValue(ImgNameProperty); }
            set { SetValue(ImgNameProperty, value); }
        }
        public static readonly DependencyProperty ImgNameProperty = DependencyProperty.Register("ImgName", typeof(string), typeof(WoodButtonUc),
                 new FrameworkPropertyMetadata
                 {
                     DefaultValue = null,
                     DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                     BindsTwoWayByDefault = true
                 });
        #endregion

        public WoodButtonUc()
        {
            InitializeComponent();
        }


        private ImageSource IsEnabledImgSource;
        private ImageSource IsNotEnabledImgSource;

        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set
            {
                SetValue(ImgSourceProperty, value);
            }
        }
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

        public ImageSource SelectedImgSource
        {
            get { return (ImageSource)GetValue(SelectedImgSourceProperty); }
            set
            {
                SetValue(SelectedImgSourceProperty, value);

            }
        }
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

        public event RoutedEventHandler Click;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEnabled)
                Click?.Invoke(this, e);
        }



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

        private void AttributeImageSource(bool value)
        {
            SelectedImgSource = (value) ? IsEnabledImgSource : IsNotEnabledImgSource;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AttributeImageSource(MAGICEnabled);
        }

    }  
}
