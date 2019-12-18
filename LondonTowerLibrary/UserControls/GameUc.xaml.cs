using LondonTowerLibrary.Event;
using LondonTowerLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LondonTowerLibrary.UserControls
{
    /// <summary>
    /// Logique d'interaction pour GameUc.xaml
    /// </summary>
    public partial class GameUc : UserControl
    {
        /// <summary>
        /// Nombre de Peg correspondant au choix de la difficulté (3,4 ou 5)
        /// </summary>
        private int nbrPeg;

        /// <summary>
        /// Entier permettant le calcule de la largeur des colonnes de la Grid
        /// </summary>
        private int sequence;

        /// <summary>
        /// Entier représentant le numero de colonne. Sert dans la fonction <c>Grid_OnClick()</c> 
        /// </summary>
        private int col;

        /// <summary>
        /// Booleen representant le 1er coup. Passe a false après le 1er mouvement dans la zone de travail
        /// </summary>
        private bool firstMove;

        /// <summary>
        /// Instance de MediaPlayer utiliser pour jouer un son lors d'une action impossible
        /// </summary>
        private MediaPlayer mp;

        /// <summary>
        /// Largeur en Pixel d'une colonne de la grid contenant les Pegs
        /// </summary>
        private double largeur;

        //private double largeurCol;

            /// <summary>
            /// Offset servant à l affichage correct des Beads en mouvement
            /// </summary>
        private double Offset;

        /// <summary>
        /// Distance en Pixel entre la bordure gauche de la colonne et l'Image du Peg
        /// </summary>
        private double Step;

        /// <summary>
        /// ViewBead ayant été sélectionné
        /// </summary>
        private ViewBead VBeadTempo;

        /// <summary>
        /// Grid de positionnement des Pegs sur les zone de travail et zone goal
        /// </summary>
        private Grid grid;

        /// <summary>
        /// Image du curseur de selection visible sous les Pegs dans la zone de travail
        /// </summary>
        private Image imgPointeur;

        /// <summary>
        /// ViewPeg utilisé dans la fonction <c>Grid_OnClick()</c>, servant a sauvegarder le Peg d'ou le Bead selectionné a été retiré
        /// </summary>
        private ViewPeg pegFrom;

        /// <summary>
        /// Object de Translation graphique 
        /// </summary>
        private TranslateTransform translate;

        /// <summary>
        /// Object de Translation graphique 
        /// </summary>
        private TranslateTransform translateViewBead;

        /// <summary>
        /// Object ViewTrial contenant toute les configurations de départ
        /// </summary>
        private ViewTrial VTrial;

        /// <summary>
        /// Liste des ViewPeg a affiché dans la Zone soit de travail soit dans la zone goal
        /// </summary>
        private List<ViewPeg> listPeg;

        /// <summary>
        /// Constructeur par defaut
        /// Initialise le placement vertical par defaut à Top,
        /// Initialise le MediaPlayer et le Booleen firstMove à true
        /// </summary>
        private GameUc()
        {
            InitializeComponent();
            mp = new MediaPlayer();
            firstMove = true;
            this.VerticalAlignment = VerticalAlignment.Top;
        }

        /// <summary>
        /// Constructeur paramétré appelant le constructeur par defaut
        /// Recuperation de la list des Bead grace au param UcGoal determinant si c'est la zone de travail ou la zone Goal
        /// Initialisation de la grid <c>InitGrid()</c>
        /// Creation du rendu visuel <c>CreateVisualElement()</c>
        /// Creation du curseur de selection si <paramref name="UcGoal"/> est a false, indiquant que l'UC reprensente la zone de travail
        /// </summary>
        /// <param name="_trial">ViewTrial du Trial (niveau) actuel </param>
        /// <param name="UcGoal">Boolean permettant de différencié la zone de travail et la zone Goal contenant le placement final des Bead</param>
        public GameUc(ViewTrial _trial, bool UcGoal) : this()
        {
            VTrial = _trial;
            VTrial.TrialComplet += TrialCompleteUc;
            HorizontalAlignment = HorizontalAlignment.Center;
            if (UcGoal)
            {              
                this.Margin = new Thickness(0, 120, 10, 0);
                listPeg = VTrial.ViewPegslistGoal;
            }
            else
            {
                this.Margin = new Thickness(10, 120, 0, 0);
                listPeg = VTrial.ViewPegsList;
            }

            nbrPeg = listPeg.Count();
            sequence = 546 / (nbrPeg + 1);
            //largeurCol = (566 - (sequence * (nbrPeg - 1) + 65)) / 2;
            largeur = (sequence * (nbrPeg - 1) + 65) / nbrPeg;

            InitGrid();
            CreateVisualElement();

            if (!UcGoal)
            {
                InitRectangleSelect();
                AddMouseEvent();
            }

        }
        /// <summary>
        /// Initialisation et placement de la Grid de travail
        /// creation des colonnes en fonction du nombre de Peg <c>nbrPeg</c> selectionné 
        /// </summary>
        private void InitGrid()
        {
            grid = new Grid();
            /*AFFICHAGE DU QUADRILLAGE DE LA GRID ET CONFIGURATION DE LA GRID*/
            grid.ShowGridLines = true;


            /*ENCRAGE ET AJOUT DE LA GRID AU CONTAINER*/
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Margin = new Thickness(0, 0, 0, 10);
            GridGame.Children.Add(grid);

            grid.Width = sequence * (nbrPeg - 1) + 65;
            grid.Height = 436;

            /*CREATION DES COLONNES DE LA GRID */
            for (int i = 0; i <= nbrPeg + 1; i++)
            {
                ColumnDefinition gridColm = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridColm);
            }
            grid.Background = new SolidColorBrush(Colors.Transparent);
            grid.ColumnDefinitions.ElementAt(0).Width = new GridLength(0);
            int counter = grid.ColumnDefinitions.Count;
            grid.ColumnDefinitions.ElementAt(counter - 1).Width = new GridLength(0);


        }
        /// <summary>
        /// Creation du Curseur sous la Zone de travail permettant les actions sur les Bead
        /// </summary>
        private void InitRectangleSelect()
        {
                      
            BitmapImage pointeur = new BitmapImage();
            string pathBoard = "pack://application:,,,/LondonTowerLibrary;component/Resources/Pointeur.png";
            pointeur.BeginInit();
            pointeur.UriSource = new Uri(pathBoard, UriKind.RelativeOrAbsolute);
            pointeur.EndInit();

            imgPointeur = new Image();
            imgPointeur.Source = pointeur;
            imgPointeur.Width = 86;
            imgPointeur.VerticalAlignment = VerticalAlignment.Bottom;
            imgPointeur.HorizontalAlignment = HorizontalAlignment.Left;
            imgPointeur.Stretch = Stretch.None;
            imgPointeur.Margin = new Thickness(0, 0, 0, 22);
            Grid.SetColumn(imgPointeur, 1);
            grid.Children.Add(imgPointeur);

            translate = new TranslateTransform();
            imgPointeur.RenderTransform = translate;

        }
        /// <summary>
        /// Creation des visuels Bead et Peg
        /// </summary>
        private void CreateVisualElement()
        {
            /*CREATION DES PIQUES ET DES BOULES*/
            foreach (ViewPeg _peg in listPeg)
            {
                grid.Children.Add(_peg);

                if (_peg.GetListViewBead().Count > 0)
                {
                    foreach (ViewBead vb in _peg.GetListViewBead())
                    {
                        grid.Children.Add(vb);
                    }
                }
            }
        }

        private void AddMouseEvent()
        {
            grid.MouseEnter += Grid_MouseEnter;
            grid.MouseLeave += Grid_MouseLeave;
            grid.MouseMove += Grid_MouseMove;
            grid.MouseDown += Grid_OnClick;
        }
        /// <summary>
        /// Gestion de l'action Cick dans la zone de travail :
        /// Determination de la colonne dans la grille
        /// Selection du Peg dans la colonne de l'action de la souris verification qu'il ne soit pas null<c>if (vpeg != null)</c>
        /// Verification de la selection d'une Bead ou non <c>if (VBeadTempo == null)</c>
        /// Si aucune Bead selectionné, selection de la bille supérieur SI présence d'une Bead
        /// SINON verif de la possibilité de posé la Bead <c>if (vpeg.CanAddViewBead() || vpeg == pegFrom)</c>
        /// SI ajout impossible de Bead sur le peg déclenchement d'un son erreur et mise a jour de la position graphique de la Bead
        /// </summary>
        /// <param name="sender">Correspond à la grid de la zone de travail</param>
        /// <param name="e">Evenement lié à la souris</para
        private void Grid_OnClick(object sender, MouseEventArgs e)
        {
            col = 1;
            Point position = Mouse.GetPosition(grid);
            double width = largeur;
            for (; width < position.X && col < nbrPeg; col++)
            {
                width += largeur;
            }
            if (firstMove)
            {
                VTrial.Trial.StartTrial();
                firstMove = false;
            }

            ViewPeg vpeg = grid.Children.OfType<ViewPeg>().FirstOrDefault(el => Grid.GetColumn(el) == col);
            if (vpeg != null)
            {

                if (VBeadTempo == null)
                {
                    VBeadTempo = vpeg.GetViewBeadUpper();
                    if (VBeadTempo != null)
                    {
                        pegFrom = vpeg;
                        VBeadTempo.SetImgUnCut();
                        VBeadTempo.Margin = new Thickness(0, 0, 0, 370);
                        translateViewBead = new TranslateTransform();
                        VBeadTempo.RenderTransform = translateViewBead;
                        if (Mouse.GetPosition(grid).X > Step - Offset && Mouse.GetPosition(grid).X < grid.Width - Step + Offset)
                        {
                            translateViewBead.X = Mouse.GetPosition(grid).X - (imgPointeur.Width / 2) - ((col - 1) * largeur);
                        }
                    }
                }
                else
                {
                    /*ajout possible au peg*/
                    if (vpeg.CanAddViewBead() || vpeg == pegFrom)
                    {
                        VBeadTempo.SetImgCut();
                        VTrial.MoveBead(pegFrom, vpeg);
                        grid.Children.Remove(VBeadTempo);
                        pegFrom = null;
                        VBeadTempo.RenderTransform = null;
                        VBeadTempo.Row = vpeg.GetListViewBead().Count - 1;
                        Grid.SetColumn(VBeadTempo, col);
                        grid.Children.Add(VBeadTempo);
                        VBeadTempo = null;
                    }
                    else /*ajout impossible au peg*/
                    {
                        mp.MediaFailed += (o, args) =>
                        {
                            MessageBox.Show("Media Failed!!");
                        };
                        mp.Open(new Uri(Environment.CurrentDirectory + @"/sound/Buzzer.mp3", UriKind.RelativeOrAbsolute));

                        mp.Play();
                        grid.Children.Remove(VBeadTempo);
                        VBeadTempo.RenderTransform = null;
                        translateViewBead = new TranslateTransform();
                        VBeadTempo.RenderTransform = translateViewBead;
                        translateViewBead.X = Mouse.GetPosition(grid).X - (imgPointeur.Width / 2) - ((col - 1) * largeur);
                        Grid.SetColumn(VBeadTempo, col);
                        grid.Children.Add(VBeadTempo);
                        VBeadTempo.Margin = new Thickness(0, 0, 0, 370);
                    }
                }
            }


        }
        /// <summary>
        /// Gestion du mouvement de la souris dans la Zone de travail
        /// SI une Bead est sélectionné ajustement de sa représentation graphique avec TranslateTransform
        /// </summary>
        /// <param name="sender">Correspond à la grid de la zone de travail</param>
        /// <param name="e">Evenement lié à la souris</param>
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Step = (grid.ColumnDefinitions[1].ActualWidth / 2);
            Offset = (grid.ColumnDefinitions[1].ActualWidth / 2) - imgPointeur.Width / 2;
            if (Mouse.GetPosition(grid).X > Step - Offset && Mouse.GetPosition(grid).X < grid.Width - Step + Offset)
            {
                translate.X = Mouse.GetPosition(grid).X - ((imgPointeur.Width / 2));
                if (VBeadTempo != null)
                {
                    translateViewBead.X = Mouse.GetPosition(grid).X - (imgPointeur.Width / 2) - ((col - 1) * largeur);
                }
            }
        }
        /// <summary>
        /// Detection de l'entré de la souris dans la Zone de travail
        /// </summary>
        /// <param name="sender">Correspond à la grid de la zone de travail</param>
        /// <param name="e">Evenement lié à la souris</param>
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.None;
        }
        /// <summary>
        /// Detection de la sortie de la souris de la Zone de travail
        /// </summary>
        /// <param name="sender">Correspond à la grid de la zone de travail</param>
        /// <param name="e">Evenement lié à la souris</param>
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Méthode permettant le desabonnement des evenements une fois le test terminé
        /// </summary>
        /// <param name="sender">ViewTrial contenant le est actuel</param>
        /// <param name="arg">Evenement prevenant la fin du test</param>
        private void TrialCompleteUc(object sender, TrialCompleteEvent arg)
        {
            grid.MouseEnter -= Grid_MouseEnter;
            grid.MouseLeave -= Grid_MouseLeave;
            grid.MouseMove -= Grid_MouseMove;
            grid.MouseDown -= Grid_OnClick;
        }

    }
}
