using System;
using System.Windows;
using System.Windows.Controls;
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// The guest with which to interact.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">The guest with which to interact.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the dialog result to true.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Initializes the window's controls.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.nameTextBox.Text = this.guest.Name;
            this.genderComboBox.SelectedItem = this.guest.Gender;
            this.ageTextBox.Text = this.guest.Age.ToString();
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.Color;
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.moneyAmountComboBox.Items.Add(1);
            this.moneyAmountComboBox.Items.Add(5);
            this.moneyAmountComboBox.Items.Add(10);
            this.moneyAmountComboBox.Items.Add(20);
            this.moneyAmountComboBox.SelectedItem = this.moneyAmountComboBox.Items[0];
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.accountComboBox.Items.Add(1);
            this.accountComboBox.Items.Add(5);
            this.accountComboBox.Items.Add(10);
            this.accountComboBox.Items.Add(20);
            this.accountComboBox.Items.Add(50);
            this.accountComboBox.Items.Add(100);
            this.accountComboBox.SelectedItem = this.accountComboBox.Items[0];
        }

        /// <summary>
        /// Sets the guest's name.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets the guest's age.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds money to the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.AddMoney(decimal.Parse(this.moneyAmountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes money from the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void subtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.RemoveMoney(decimal.Parse(this.moneyAmountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Sets the guest's gender.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// Sets the guest's wallet color.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Wallet.Color = (WalletColor)this.walletColorComboBox.SelectedItem;
        }

        /// <summary>
        /// Removes money from the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void subtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.RemoveMoney(decimal.Parse(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Adds money from to the guest's checking account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.AddMoney(decimal.Parse(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }
    }
}