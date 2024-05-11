using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Animals;
using CagedItems;
using Foods;
using Utilities;
using Zoos;
using System.Linq;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class CageWindow : Window
    {
        /// <summary>
        /// The window's cage.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">The window's cage.</param>
        public CageWindow(Cage cage)
        {
            this.InitializeComponent();
            this.cage = cage;
            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate ()
                    {
                        int zIndex = 0;

                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                this.cageGrid.Children.Remove(v);
                                break;
                            }
                            zIndex++;
                        }

                        // Draw the item
                        if (item.IsActive == true)
                        {
                            this.DrawItem(item, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
        }

        /// <summary>
        /// Draws the item to the screen.
        /// </summary>
        /// <param name="item">The item to draw.</param>
        public void DrawItem(ICageable item, int zIndex)
        {
            if (item is Food)
            {
                (item as Food).ResourceKey = "KangarooFood";
            }

            // Gets the viewBox.
            Viewbox viewBox = this.GetViewBox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);

            // Aligns the view box to the top left of the grid.
            viewBox.HorizontalAlignment = HorizontalAlignment.Left;
            viewBox.VerticalAlignment = VerticalAlignment.Top;

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

            TransformGroup transformGroup = new TransformGroup();

            // If the animal is unconscious, make it look like they are unconscious.
            if(item.HungerState == HungerState.Unconscious)
            {
                // Create a new SkewTransform and set its AngleX to 30 degrees in the direction the cageable is facing.
                SkewTransform unconsciousSkew = new SkewTransform();
                unconsciousSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;

                // Add the SkewTransform to the transform group.
                transformGroup.Children.Add(unconsciousSkew);

                // Add a new ScaleTransform of 75% of width, 50% of height to the transform group.
                transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));

                // Apply all transforms in the transform group to the viewbox.
                viewBox.RenderTransform = transformGroup;
            }

            viewBox.Tag = item;

            // Add the viewbox to the grid.
            this.cageGrid.Children.Insert(zIndex, viewBox);
        }

        /// <summary>
        /// Gets a view box with set positioning.
        /// </summary>
        /// <param name="maxXPosition">The maximum position on the x-axis.</param>
        /// <param name="maxYPosition">The maximum position on the y-axis.</param>
        /// <param name="xPosition">The item's current position on the x-axis.</param>
        /// <param name="yPosition">The item's current position on the y-axis.</param>
        /// <param name="resourceKey">The key that defines the animal to be drawn.</param>
        /// <param name="displayScale">Changes the default size based on it's scale.</param>
        /// <returns>The view box that will display the current item.</returns>
        private Viewbox GetViewBox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            // Create a canvas of the item to draw
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
        /// Draws all animals in the cage window.
        /// </summary>
        private void DrawAllItems()
        {
            // Clear the grid of pass animal positions being displayed
            this.cageGrid.Children.Clear();

            int zIndex = 0;
            // Draw all the items in the cage window
            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex++));
        }

        /// <summary>
        /// Handles the event of the redraw timer going off.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(delegate()
                {
                    this.DrawAllItems();
                }));
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// This code runs when the window is loaded.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        private void window_Closed(object sender, EventArgs e)
        {
            // This will help with serialization by removing any references the delegate might have to the window's Update method.
            this.cage.OnImageUpdate = null;
        }
    }
}