using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// A bag for holding the guest's items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// The checking account for collecting money.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        [NonSerialized]
        private Action<Guest> onTextChange;

        private Animal adoptedAnimal;

        [NonSerialized]
        // The time it takes the guest to get the vending machine food and realize the animal is hungry.
        private Timer feedTimer;

        private bool isActive;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">The gender of the guest.</param>
        /// <param name="checkingAccount">The account for collecting money.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.bag = new List<Item>();
            this.checkingAccount = checkingAccount;
            this.checkingAccount.OnBalanceChange += HandleBalanceChange;
            this.gender = gender;
            this.name = name;
            this.wallet = new Wallet(walletColor, new MoneyPocket());
            this.wallet.OnBalanceChange += HandleBalanceChange;

            // Add money to wallet.
            this.wallet.AddMoney(moneyBalance);
            this.XPosition = 0;
            this.YPosition = 0;

            this.CreateTimers();
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 120.");
                }

                this.age = value;
                if (OnTextChange != null)
                {
                    OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the guest's adopted animal.
        /// </summary>
        public Animal AdoptedAnimal 
        {
            get
            {
                return this.adoptedAnimal;
            }
            set
            {
                // If there was an adopted animal and it has an OnHunger delegate, clear it
                if (this.adoptedAnimal != null && this.adoptedAnimal.OnHunger != null)
                {
                    this.adoptedAnimal.OnHunger = null;
                }

                // Set the adopted animal to the new value
                this.adoptedAnimal = value;

                // If the newly adopted animal exists, attach the HandleAnimalHungry method to its OnHunger delegate
                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger += HandleAnimalHungry;
                }

                // Raise the OnTextChange event if it's not null
                if (OnTextChange != null)
                {
                    OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets the proportion at which to display the guest.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets or sets the guest's gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new ArgumentException("Names can contain only upper- and lowercase letters A-Z and spaces.");
                }

                this.name = value;
                if (OnTextChange != null)
                {
                    OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets the resource key of the guest.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the guest's wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the horizontal position of the guest.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the vertical position of the guest.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the horizontal direction of the guest.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the vertical direction of the guest.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets the percentage of weight gained when consuming food.
        /// </summary>
        public double WeightGainPercentage
        {
            get
            {
                return 0.0;
            }
        }

        public Action<Guest> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }
            set
            {
                this.onTextChange = value;
            }
        }

        public HungerState HungerState
        {
            get;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        /// <param name="animalSnackMachine">The animal snack machine from which to buy food.</param>
        public void FeedAnimal(IEater eater)
        {
            // Get the vending machine
            VendingMachine animalSnackMachine = GetVendingMachine();

            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            // Check if guest has enough money on hand and withdraw from account if necessary.
            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 10);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        public Func<VendingMachine> GetVendingMachine
        {
            get;
            set;
        }

        public Action<ICageable> OnImageUpdate
        {
            get;
            set;
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            string result = string.Format("{0}: {1} [${2} / ${3}]", this.Name, this.Age, this.Wallet.MoneyBalance, this.CheckingAccount.MoneyBalance);

            if (this.AdoptedAnimal != null)
            {
                result += ", " + this.AdoptedAnimal.Name;
            }

            return result;
        }

        /// <summary>
        /// Visits the information booth to obtain a coupon book and a map.
        /// </summary>
        /// <param name="informationBooth">The booth to visit.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // Get map.
            Map map = informationBooth.GiveFreeMap();

            // Get coupon book.
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();

            // Add items to bag.
            this.bag.Add(map);
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// Visits the booth to purchase a ticket and a water bottle.
        /// </summary>
        /// <param name="ticketBooth">The booth to visit.</param>
        /// <returns>A purchased ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            if (this.wallet.MoneyBalance < ticketBooth.TicketPrice)
            {
                this.WithdrawMoney(ticketBooth.TicketPrice * 2);
            }

            // Take ticket money out of wallet.
            decimal ticketPayment = this.wallet.RemoveMoney(ticketBooth.TicketPrice);

            // Buy ticket.
            Ticket ticket = ticketBooth.SellTicket(ticketPayment);

            if (this.wallet.MoneyBalance < ticketBooth.WaterBottlePrice)
            {
                this.WithdrawMoney(ticketBooth.WaterBottlePrice * 2);
            }

            // Take water bottle money out of wallet.
            decimal waterPayment = this.wallet.RemoveMoney(ticketBooth.WaterBottlePrice);

            // Buy water bottle.
            WaterBottle bottle = ticketBooth.SellWaterBottle(waterPayment);

            // Add water bottle to bag.
            this.bag.Add(bottle);

            return ticket;
        }

        /// <summary>
        /// Withdraws money from the checking account and puts it into the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to withdraw.</param>
        public void WithdrawMoney(decimal amount)
        {
            decimal retrievedAmount = this.checkingAccount.RemoveMoney(amount);

            this.wallet.AddMoney(retrievedAmount);
        }

        public void HandleAnimalHungry()
        {
            this.feedTimer.Start();
        }

        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            this.FeedAnimal(this.adoptedAnimal);
            this.feedTimer.Stop();
        }

        private void CreateTimers()
        {
            // The hunger timer 5 sec.
            this.feedTimer = new Timer(5000);
            this.feedTimer.Elapsed += HandleReadyToFeed;
        }

        /// <summary>
        /// Creates timers when the guest is deserialized.
        /// </summary>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        private void HandleBalanceChange()
        {
            if (OnTextChange != null)
            {
                OnTextChange(this);
            }
        }
    }
}