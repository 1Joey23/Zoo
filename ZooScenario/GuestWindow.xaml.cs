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
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {
        private Guest guest;

        public GuestWindow(Guest guest)
        {
            try
            {
                InitializeComponent();
                this.guest = guest;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.guest.Name;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.moneyAmountComboBox.Items.Add(1.00m);
            this.moneyAmountComboBox.Items.Add(5.00m);
            this.moneyAmountComboBox.Items.Add(10.00m);
            this.moneyAmountComboBox.Items.Add(20.00m);
            this.accountComboBox.Items.Add(1.00m);
            this.accountComboBox.Items.Add(5.00m);
            this.accountComboBox.Items.Add(10.00m);
            this.accountComboBox.Items.Add(20.00m);
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string value = this.nameTextBox.Text;
            if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
            {
                MessageBox.Show("Name must be alphatetical letters only without spaces. (i.e. name)");
                okButton.IsEnabled = false;
            }
            else // Runs if a valid name is entered.
            {
                guest.Name = value;
                okButton.IsEnabled = true;
            }
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gender gender = (Gender)this.genderComboBox.SelectedItem;
            guest.Gender = gender;
        }

        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int age = int.Parse(this.ageTextBox.Text);
            if (age >= 0 && age <= 120)
            {
                guest.Age = age;
                okButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("The age must be between 0 and 100, inclusive.");
                okButton.IsEnabled = false;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WalletColor walletColor = (WalletColor)this.walletColorComboBox.SelectedItem;
            guest.Wallet.Color = walletColor;
        }

        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amountToAdd = (decimal)this.moneyAmountComboBox.SelectedItem;
                guest.Wallet.AddMoney(amountToAdd);
                string formattedPrice = guest.Wallet.MoneyBalance.ToString("C");
                moneyBalanceLabel.Content = formattedPrice;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select an amount to add in the combo box before clicking the + or - button");
            }
        }

        private void subtractMoneyBalance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amountToRemove = (decimal)this.moneyAmountComboBox.SelectedItem;
                guest.Wallet.RemoveMoney(amountToRemove);
                string formattedPrice = guest.Wallet.MoneyBalance.ToString("C");
                moneyBalanceLabel.Content = formattedPrice;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select an amount to add in the combo box before clicking the + or - button");
            }
        }

        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amountToAdd = (decimal)this.accountComboBox.SelectedItem;
                guest.CheckingAccount.AddMoney(amountToAdd);
                string formattedPrice = guest.CheckingAccount.MoneyBalance.ToString("C");
                accountBalanceLabel.Content = formattedPrice;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select an amount to add in the combo box before clicking the + or - button");
            }
        }

        private void subtractAccountBalance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal amountToRemove = (decimal)this.accountComboBox.SelectedItem;
                guest.CheckingAccount.RemoveMoney(amountToRemove);
                string formattedPrice = guest.CheckingAccount.MoneyBalance.ToString("C");
                accountBalanceLabel.Content = formattedPrice;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select an amount to add in the combo box before clicking the + or - button");
            }
        }
    }
}
