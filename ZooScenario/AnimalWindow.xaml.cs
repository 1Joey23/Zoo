using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml
    /// </summary>
    public partial class AnimalWindow : Window
    {
        private Animal animal;
        public AnimalWindow(Animal animal)
        {
            try 
            { 
                InitializeComponent();
                this.animal = animal;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
            this.nameTextBox.Text = this.animal.Name;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                animal.Name = this.nameTextBox.Text;
                this.okButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.okButton.IsEnabled = false;
            }
        }

        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                animal.Age = int.Parse(this.ageTextBox.Text);
                this.okButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.okButton.IsEnabled = false;
            }
        }

        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                animal.Weight = double.Parse(this.weightTextBox.Text);
                this.okButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.okButton.IsEnabled = false;
            }
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gender gender = (Gender)this.genderComboBox.SelectedItem;
            animal.Gender = gender;
        }

        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            if (animal.Gender == Gender.Female)
            {
                animal.MakePregnant();
                if (animal.IsPregnant == true)
                {
                    this.makePregnantButton.IsEnabled = false;
                    this.pregnancyStatusLabel.Content = "Yes";
                }
            }
        }
    }
}
