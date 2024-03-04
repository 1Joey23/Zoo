using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Animals;
using CagedItems;
using Zoos;
using Utilites;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml
    /// </summary>
    public partial class CageWindow : Window
    {
        private Cage cage;
        
        private Timer redrawTimer;
        public CageWindow(Cage cage)
        {
            this.cage = cage;
            InitializeComponent();
            this.redrawTimer = new Timer(100);
            this.redrawTimer.Elapsed += this.RedrawHandler;
            this.redrawTimer.Start();
        }

        /// <summary>
        /// Draws the animal in the cage window.
        /// </summary>
        private void DrawItem(ICageable item)
        {
            Viewbox viewBox = GetViewbox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);
            viewBox.HorizontalAlignment = HorizontalAlignment.Left;

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewbox.
                viewBox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewbox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Apply the ScaleTransform to the viewbox
                viewBox.RenderTransform = flipTransform;
            }

            this.cageGrid.Children.Add(viewBox);
        }

        private Viewbox GetViewbox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewbox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewbox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewbox.
            return finishedViewBox;
        }

        /// <summary>
        /// Runs on cageWindow loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllAnimals();
        }

        /// <summary>
        /// Redraw the animal on every timer click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                this.DrawAllAnimals();
            }));
        }

        private void DrawAllAnimals()
        {
            this.cageGrid.Children.Clear();
            foreach (ICageable a in this.cage.CagedItems)
            {
                DrawItem(a);
            }
        }
    }
}
