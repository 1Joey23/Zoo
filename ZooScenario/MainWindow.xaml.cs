using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using Foods;
using Microsoft.Win32;
using People;
using Reproducers;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The name of the auto-save file.
        /// </summary>
        private const string AutoSaveFileName = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }

        /// <summary>
        /// Adds an new animal to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;

            Animal animal = AnimalFactory.CreateAnimal(animalType, "Animal", 0, 0.0, Gender.Female);

            AnimalWindow window = new AnimalWindow(animal);

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                this.comoZoo.AddAnimal(animal);
            }
        }

        /// <summary>
        /// Has the specified guest adopt the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (animal != null && guest != null)
            {
                guest.AdoptedAnimal = animal;

                Cage cage = this.comoZoo.FindCage(animal.GetType());

                cage.Add(guest);

            }
            else
            {
                MessageBox.Show("Select a guest and an animal for that guest to adopt.");
            }
        }

        /// <summary>
        /// Adds a new guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest("Guest", 0,  0m, WalletColor.Black, Gender.Female, new Account());

            GuestWindow window = new GuestWindow(guest);

            window.ShowDialog();

            if (window.DialogResult == true)
            {
                try
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);

                    this.comoZoo.AddGuest(guest, ticket);

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Admits a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account ethelAccount = new Account();
            ethelAccount.AddMoney(30.00m);
            Guest guest = new Guest("Ethel", 42, 30.00m, WalletColor.Salmon, Gender.Female, ethelAccount);

            try
            {
                Ticket ticket = this.comoZoo.SellTicket(guest);

                this.comoZoo.AddGuest(guest, ticket);

            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Edits the animal on the double-click event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                AnimalWindow window = new AnimalWindow(animal);

                if (window.ShowDialog() == true)
                {

                }
            }
        }

        /// <summary>
        /// Births a pregnant animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            this.comoZoo.BirthAnimal();
        }

        /// <summary>
        /// Clears (resets) the window to an empty state.
        /// </summary>
        private void ClearWindow()
        {
            this.guestListBox.Items.Clear();
            this.animalListBox.Items.Clear();
        }

        /// <summary>
        /// Decreases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Has the guest feed an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest guest = this.guestListBox.SelectedItem as Guest;

                Animal animal = this.animalListBox.SelectedItem as Animal;

                animal.Food = new Food(0.5);

                Cage cage = this.comoZoo.FindCage(animal.GetType());

                cage.Add(animal);
                cage.Add(guest);
                cage.Add(animal.Food);

                if (guest != null && animal != null)
                {
                    guest.FeedAnimal(animal);

                }
                else
                {
                    MessageBox.Show("You must choose a guest and an animal to feed an animal.");
                }

                this.guestListBox.SelectedItem = guest;
                this.animalListBox.SelectedItem = animal;
            }
            catch
            {
                MessageBox.Show("You must create a zoo before feeding animals.");
            }
        }

        /// <summary>
        /// Edits the guest on the double-click event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                GuestWindow window = new GuestWindow(guest);

                if (window.ShowDialog() == true)
                {
 
                }
            }
        }

        /// <summary>
        /// Increases the birthing room temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You must create a zoo before changing the temperature.");
            }
        }

        /// <summary>
        /// Removes the selected animal from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected animal.
                    this.comoZoo.RemoveAnimal(animal);
                }
            }
            else
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        /// <summary>
        /// Removes the selected guest from the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Remove the selected animal.
                    this.comoZoo.RemoveGuest(guest);

                }
            }
            else
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        /// <summary>
        /// Saves the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a save file dialog and set its file filter
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            // If the dialog result is true
            if (dialog.ShowDialog() == true)
            {
                // Save the zoo, passing in the file name specified in the dialog
                this.SaveZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Saves the zoo to a file.
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);

            this.SetWindowTitle(fileName);
        }

        /// <summary>
        /// Sets the title of the window.
        /// </summary>
        /// <param name="fileName">The name of the current file.</param>
        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = string.Format("Object-Oriented Programming 2: Zoo [{0}]", Path.GetFileName(fileName));
        }

        /// <summary>
        /// Loads the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an open file dialog and set its file filter
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            // If the dialog result is true
            if (dialog.ShowDialog() == true)
            {
                // Clear the window
                this.ClearWindow();

                // Load the zoo, passing in the file name specified in the dialog
                this.LoadZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Loads the state of the zoo from a file.
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        /// <returns>A value indicating whether or not loading succeeded.</returns>
        private bool LoadZoo(string fileName)
        {
            bool result = true;
            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.AttachDelegates();
                this.SetWindowTitle(fileName);
            }
            catch
            {
                result = false;
                MessageBox.Show("File could not be loaded. Choose another file or restart.");     
            }

            return result;
        }

        /// <summary>
        /// Restarts the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            // Show a confirmation message asking if the user actually wants to start over; if they do...
            if (MessageBox.Show("Are you sure that you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Clear the window
                this.ClearWindow();

                this.Title = "Object-Oriented Programming 2: Zoo";

                // Create a new zoo
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
            }
        }

        /// <summary>
        /// Shows the cage of the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                CageWindow window = new CageWindow(cage);

                window.Show();
            }
            else
            {
                MessageBox.Show("Select an animal whose cage to show.");
            }
        }

        /// <summary>
        /// Has the specified guest un-adopt their adopted animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null && guest.AdoptedAnimal != null)
            {
                Cage cage = this.comoZoo.FindCage(guest.AdoptedAnimal.GetType());

                guest.AdoptedAnimal = null;

                cage.Remove(guest);

            }
            else
            {
                MessageBox.Show("Select a guest to unadopt their animal.");
            }
        }

        /// <summary>
        /// Auto-saves the zoo when the window closes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo(MainWindow.AutoSaveFileName);
        }

        /// <summary>
        /// This code runs when the window is loaded.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.LoadZoo(MainWindow.AutoSaveFileName))
            {
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
            }

            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));

            this.animalTypeComboBox.SelectedItem = AnimalType.Chimpanzee;

            this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));
        }

        /// <summary>
        /// Changes the move behavior of the specified animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void changeMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            object behaviorType = this.changeMoveBehaviorComboBox.SelectedItem;

            if (animal != null && behaviorType != null)
            {
                animal.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)behaviorType);
            }
            else
            {
                MessageBox.Show("Select an animal and a move behavior type to change its move behavior.");
            }
        }

        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (currentTemp, previousTemp) =>
            {
                // Set label's text.
                this.temperatureLabel.Content = string.Format("{0:0.0} °F", this.comoZoo.BirthingRoomTemperature);

                // Size temperature bar.
                this.temperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;

                // Calculate temperature bar's color level (from 0 to 255).
                double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                // Set temperature bar's color based upon the color level (red is directly proportional to color level; green and blue are inversely proportional).
                this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(colorLevel),
                    Convert.ToByte(255 - colorLevel),
                    Convert.ToByte(255 - colorLevel)));
            };

            this.comoZoo.OnAddGuest = guest => 
            { 
                this.guestListBox.Items.Add(guest);
                guest.OnTextChange += UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest = guest => 
            {
                this.guestListBox.Items.Remove(guest);
                guest.OnTextChange -= UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = animal => 
            {
                this.animalListBox.Items.Add(animal);
                animal.OnTextChangeAnimal += UpdateAnimalDisplay;
            };

            this.comoZoo.OnRemoveAnimal = animal =>
            {
                this.animalListBox.Items.Remove(animal);
                animal.OnTextChangeAnimal -= UpdateAnimalDisplay;
            };

            this.comoZoo.OnDeserialized();
        }

        private void UpdateGuestDisplay(Guest guest)
        {
            Dispatcher.Invoke(() =>
            {
                int index = this.guestListBox.Items.IndexOf(guest);
                if (index >= 0)
                {
                    // disconnect the guest
                    this.guestListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.guestListBox.Items.Insert(index, guest);

                    // re-select the guest
                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                }
            });
        }

        private void UpdateAnimalDisplay(Animal animal)
        {
            Dispatcher.Invoke(() =>
            {
                int index = this.animalListBox.Items.IndexOf(animal);
                if (index >= 0)
                {
                    // disconnect the guest
                    this.animalListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.animalListBox.Items.Insert(index, animal);

                    // re-select the guest
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                }
            });
        }
    }
}