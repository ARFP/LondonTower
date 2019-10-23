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
        int nbrPeg;
        int sequence;
        int col;
        bool firstMove = true;


        double largeur;
        double largeurCol;

        double Offset;
        double Step;

        ViewBead VBeadTempo;

        Grid grid;
        Rectangle RectSelection;

        Image imgPointeur;

        private ViewPeg pegFrom;

        TranslateTransform translate;
        TranslateTransform translateViewBead;

        ViewTrial VTrial;
        List<ViewPeg> listPeg;

        //public delegate void EndTrial(object sender, EventArgs e);

        //public delegate void TrialCompleteEventHandler(object sender, TrialCompleteEvent arg);

        //public event TrialCompleteEventHandler TrialComplet;


        private GameUc()
        {
            InitializeComponent();
            this.VerticalAlignment = VerticalAlignment.Top;
            
        }

        //public GameUc(IList<Peg> _listpeg, bool UcGoal) : this()
        public GameUc(ViewTrial _trial, bool UcGoal) : this()
        {
            VTrial = _trial;
            VTrial.TrialComplet += TrialCompleteUc;

            if (UcGoal)
            {
                HorizontalAlignment = HorizontalAlignment.Right;
                this.Margin = new Thickness(0, 120, 10, 0);
                listPeg = VTrial.ViewPegslistGoal;
                
            }
            else
            {
                HorizontalAlignment = HorizontalAlignment.Left;
                this.Margin = new Thickness(10, 120, 0, 0);
                listPeg = VTrial.ViewPegsList;
            }

            nbrPeg = listPeg.Count();
            sequence = 546 / (nbrPeg+1);
            largeurCol = (566 - (sequence * (nbrPeg - 1) + 65)) / 2;
            largeur = (sequence * (nbrPeg - 1) + 65) / nbrPeg;

            InitGrid();
            CreateVisualElement();

            if (!UcGoal)
            {
                InitRectangleSelect();
                AddMouseEvent();
            }

        }

        private void InitGrid()
        {
            grid = new Grid();
            /*AFFICHAGE DU QUADRILLAGE DE LA GRID ET CONFIGURATION DE LA GRID*/
            //grid.ShowGridLines = true;


            /*ENCRAGE ET AJOUT DE LA GRID AU CONTAINER*/
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Margin = new Thickness(0, 0, 0, 10);
            GridGame.Children.Add(grid);

            grid.Width = sequence * (nbrPeg - 1) + 65;
            grid.Height = 436;

            /*CREATION DES COLONNES DE LA GRID */
            for(int i = 0; i <= nbrPeg + 1; i++)
            {
                ColumnDefinition gridColm = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridColm);
            }
            grid.Background = new SolidColorBrush(Colors.Transparent);
            grid.ColumnDefinitions.ElementAt(0).Width = new GridLength(0);
            int counter = grid.ColumnDefinitions.Count;
            grid.ColumnDefinitions.ElementAt(counter - 1).Width = new GridLength(0);



            /*CREATION DES COLONNES DE LIMITATION */

            //ColumnDefinition gridColumn2 = grid.ColumnDefinitions.First();
            //gridColumn2.Width = new GridLength();
            //gridColumn2 = grid.ColumnDefinitions.Last();
            //gridColumn2.Width = new GridLength();



            //ColumnDefinition gridColumn2 = new ColumnDefinition();
            //gridColumn2.Width = new GridLength();
            //gridColumn2.Name = "Column" + 0;
            //grid.ColumnDefinitions.Insert(0, gridColumn2);
            //gridColumn2 = new ColumnDefinition();
            //gridColumn2.Width = new GridLength();
            //gridColumn2.Name = "Column" + grid.ColumnDefinitions.Count;
            //grid.ColumnDefinitions.Insert(grid.ColumnDefinitions.Count, gridColumn2);

        }

        private void InitRectangleSelect()
        {
            /*CREATION DU POINTEUR SOUS LA ZONE DE TEST POUR LA SELECTION DE LA BILLE A BOUGER*/
            //RectSelection = new Rectangle();
            //RectSelection.Width = 86;
            //RectSelection.Height = 10;
            //RectSelection.HorizontalAlignment = HorizontalAlignment.Left;
            //RectSelection.VerticalAlignment = VerticalAlignment.Bottom;
            //RectSelection.Margin = new Thickness(0, 0, 0, 34);

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

            /*COLORATION DU RECTANGLE*/
            //SolidColorBrush brush = new SolidColorBrush();
            //brush.Color = Color.FromArgb(255, 0, 0, 0);
            //RectSelection.Fill = brush;

            /*AJOUT DE LA COLONNE DANS LAQUEL L OBJET SERA AJOUTER*/
            //Grid.SetColumn(RectSelection, 1);
            //grid.Children.Add(RectSelection);

            /*AJOUT DU TRANSLATETRANSFORM AU RENDERTRANSFORM DE L OBJET POUR GERER LE MOUVEMENT*/
            //translate = new TranslateTransform();
            //RectSelection.RenderTransform = translate;
            translate = new TranslateTransform();
            imgPointeur.RenderTransform = translate;

        }

        private void CreateVisualElement()
        {
            /*CREATION DES PIQUES ET DES BOULES*/
            foreach (ViewPeg _peg in listPeg)
            {
                grid.Children.Add(_peg);

                if (_peg.GetListViewBead().Count > 0)
                {
                    foreach(ViewBead vb in _peg.GetListViewBead())
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

        private void Grid_OnClick(object sender, MouseEventArgs e)
        {
            col = 1;
            Point position = Mouse.GetPosition(grid);
            double width = largeur;
            for(;width<position.X && col< nbrPeg; col++)
            {
                width += largeur;
            }
            if (firstMove)
            {
                VTrial.Trial.StartTrial();
                firstMove = false;
            }

            ViewPeg vpeg = grid.Children.OfType<ViewPeg>().FirstOrDefault(el => Grid.GetColumn(el) == col);
            if(vpeg != null)
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
                    if (vpeg.CanAddViewBead()|| vpeg == pegFrom)/*ajout possible au peg*/
                    {
                        VBeadTempo.SetImgCut();
                        VTrial.MoveBead(pegFrom, vpeg);
                        grid.Children.Remove(VBeadTempo);
                        //vpeg.AddViewBead( pegFrom.RemoveTopViewBead());
                        pegFrom = null;
                        VBeadTempo.RenderTransform = null;
                        VBeadTempo.Row = vpeg.GetListViewBead().Count-1;
                        Grid.SetColumn(VBeadTempo, col);
                        grid.Children.Add(VBeadTempo);
                        VBeadTempo = null;
                    }
                    else /*ajout impossible au peg*/
                    {
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

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Step = (grid.ColumnDefinitions[1].ActualWidth / 2);
            Offset = (grid.ColumnDefinitions[1].ActualWidth / 2) - imgPointeur.Width / 2;
            if (Mouse.GetPosition(grid).X > Step-Offset && Mouse.GetPosition(grid).X < grid.Width - Step + Offset)
            {
                translate.X = Mouse.GetPosition(grid).X - ((imgPointeur.Width / 2));
                if (VBeadTempo != null)
                {
                    translateViewBead.X = Mouse.GetPosition(grid).X - (imgPointeur.Width / 2) - ((col - 1) * largeur);
                }
            }
        }

        private void Grid_MouseEnter(object sender,MouseEventArgs e)
        {
            this.Cursor = Cursors.None;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void TrialCompleteUc(object sender, TrialCompleteEvent arg)
        {
            grid.MouseEnter -= Grid_MouseEnter;
            grid.MouseLeave -= Grid_MouseLeave;
            grid.MouseMove -= Grid_MouseMove;
            grid.MouseDown -= Grid_OnClick;
        }

    }
}
