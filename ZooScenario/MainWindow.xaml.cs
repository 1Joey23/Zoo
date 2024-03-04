using System.Windows;
using People;
using Animals;
using Zoos;
using System;
using Reproducers;
using BoothItems;
using MoneyCollectors;
using BirthingRooms;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Controls;
using Accounts;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
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
        /// Runs the admit guest process with the ticket booth.
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Guest ethel = new Guest("Ethel", 42, new Account(), 30.00m, WalletColor.Salmon, Gender.Female);  // Didn't realize Ethel was a girl name haha.
            this.comoZoo.AddGuest(ethel, this.comoZoo.SellTicket(ethel));
            this.PopulateGuestListBox();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Make greg feed the ostrich.
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = (Animal)animalListBox.SelectedItem;

            Guest guest = (Guest)guestListBox.SelectedItem;

            if (guest != null && animal != null)
            {
                guest.FeedAnimal(animal, this.comoZoo.AnimalSnackMachine);
                this.PopulateAnimalListBox();
                this.PopulateGuestListBox();
            }
            else
            {
                MessageBox.Show("You must choose a guest and an animal to feed the animal.");
            }
            
        }

        /// <summary>
        /// Increase temp in birthing room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
                ConfigureBirthingRoomControls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Decrease temp in birthing room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               this.comoZoo.BirthingRoomTemperature--;
               ConfigureBirthingRoomControls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Sets the temperature.
        /// </summary>
        private void ConfigureBirthingRoomControls()
        {
            this.temperatureLabel.Content = ($"{this.comoZoo.BirthingRoomTemperature:0.0} °F");

            this.temperatureBorder.Height = this.comoZoo.BirthingRoomTemperature * 2;

            double colorLevel = ((this.comoZoo.BirthingRoomTemperature - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

            this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                Convert.ToByte(colorLevel),
                Convert.ToByte(255 - colorLevel),
                Convert.ToByte(255 - colorLevel)));
        }

        /// <summary>
        /// Populate the list of animals into the xaml popup.
        /// </summary>
        private void PopulateAnimalListBox()
        {
            ListBox animalListBox = (ListBox)FindName("animalListBox");

            animalListBox.ItemsSource = null;

            // Get animals from the zoo
            IEnumerable<Animal> animals = comoZoo.Animals;

            // Assign the zoo's Animals property as the ItemsSource
            animalListBox.ItemsSource = animals;

        }

        /// <summary>
        /// Populate the list of guests into the xaml popup.
        /// </summary>
        private void PopulateGuestListBox()
        {
            ListBox guestListBox = (ListBox)FindName("guestListBox");

            guestListBox.ItemsSource = null;

            // Get animals from the zoo
            IEnumerable<Guest> guests = comoZoo.Guests;

            // Assign the zoo's Animals property as the ItemsSource
            guestListBox.ItemsSource = guests;
        }

        /// <summary>
        /// Creates the comozoo on window loadup and finished the + and - button control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.comoZoo = Zoo.NewZoo();
            this.ConfigureBirthingRoomControls();
            this.PopulateAnimalListBox();
            this.PopulateGuestListBox();
            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
        }

        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;
                Animal animal = AnimalFactory.CreateAnimal(animalType, "NAME", 10, 40.0, Gender.Female);
                
                AnimalWindow AW = new AnimalWindow(animal);
                
                if (AW.ShowDialog() == true)
                {
                    this.comoZoo.AddAnimal(animal);
                    PopulateAnimalListBox();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal type must be selected before adding an animal to the zoo");
            }
        }

        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account guestChecking = new Account();
                Guest guest = new Guest("gName", 0, guestChecking, 0.0m, WalletColor.Black, Gender.Male);

                GuestWindow GW = new GuestWindow(guest);

                if (GW.ShowDialog() == true)
                {
                    this.comoZoo.AddGuest(guest, comoZoo.SellTicket(guest));
                    PopulateGuestListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A guest wallet color must be selected before adding a guest to the zoo");
            
                throw new NullReferenceException(ex.Message);
            }
        }

        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null) 
            { 
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes){
                    this.comoZoo.RemoveAnimal(animal);
                    PopulateAnimalListBox();
                }
            }
            else
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    this.comoZoo.RemoveGuest(guest);
                    if (guest.AdoptedAnimal != null)
                    {
                        Animal animal = guest.AdoptedAnimal;
                        Cage cage = this.comoZoo.FindCage(animal.GetType());
                        cage.RemoveAnimal(guest);
                    }
                    PopulateGuestListBox();
                }
            }
            else
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        /// <summary>
        /// Edit the guest on double click in animal list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animalListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            AnimalWindow aw = new AnimalWindow(animal);
            aw.ShowDialog();
            this.PopulateAnimalListBox();
        }

        /// <summary>
        /// Edit the guest on double click in guest list box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guestListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            GuestWindow gw = new GuestWindow(guest);
            gw.ShowDialog();
            this.PopulateGuestListBox();
        }

        /// <summary>
        /// Show the cage window on button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Type type = animal.GetType();
                Cage cage = this.comoZoo.FindCage(type);

                CageWindow cw = new CageWindow(cage);
                cw.Show();
            }
            else
            {
                MessageBox.Show("You must select an animal to show the animal.");
            }
        }

        /// <summary>
        /// Let the guest temporairly adopt an animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            guest.AdoptedAnimal = animal;
            Cage cage = this.comoZoo.FindCage(animal.GetType());
            cage.AddAnimal(guest);
            PopulateGuestListBox();
        }

        /// <summary>
        /// Remove the adopted animal from the guest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = guest.AdoptedAnimal;
            Cage cage = this.comoZoo.FindCage(animal.GetType());
            cage.RemoveAnimal(guest);
            guest.AdoptedAnimal = null; // The guest threw out the animal or somethin.
            PopulateGuestListBox();
        }
    }
}