using BoothItems;
using Foods;
using MoneyCollectors;
using System.Collections.Generic;
using VendingMachines;
using Reproducers;
using System.Security.Principal;
using Accounts;
using System;
using CagedItems;
using Animals;
using Utilites;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// Allow the guest to hold onto the items from booths.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// Gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The guests checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        public Guest(string name, int age, IMoneyCollector checkingAccount ,decimal moneyBalance, WalletColor walletColor, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.wallet = new Wallet(walletColor, new MoneyPocket());
            this.wallet.AddMoney(moneyBalance);
            this.bag = new List<Item>();
            this.gender = gender;
            this.checkingAccount = checkingAccount;
            this.XPosition = 0;
            this.YPosition = 0;
        }

        /// <summary>
        /// Get the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                this.age = value;
            }
        }

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
        /// Gets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
            set
            {
                this.checkingAccount = value;
            }
        }

        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }
        }

        public Animal AdoptedAnimal
        {
            get;

            set;
        }

        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        public int XPosition
        {
            get;
            protected set;
        }

        public int YPosition
        {
            get;
            protected set;
        }

        public HorizontalDirection XDirection
        {
            get;
            protected set;
        }

        public VerticalDirection YDirection
        {
            get;
            protected set;
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
        public void FeedAnimal(IEater eater, VendingMachine animalSnackMachine)
        {
            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price *= 10);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Make the guest visit the information booth.
        /// </summary>
        /// <param name="informationBooth">The information booth.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // The map process. (Doesn't do anything with resulting map.)
            Map map = informationBooth.GiveFreeMap();
            this.bag.Add(map);

            // The Coupon Book process. (Doesn't do anything with resulting coupon book.)
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// Make the guest visit the ticket booth.
        /// </summary>
        /// <param name="ticketBooth"> The ticket booth</param>
        /// <returns>The guests ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            try
            {
                // The ticket process
                decimal ticketPrice = ticketBooth.TicketPrice;
                if (this.wallet.MoneyBalance < ticketPrice)
                {
                    this.WithdrawMoney(ticketPrice *= 2);
                }

                decimal amountRemovedTicket = wallet.RemoveMoney(ticketPrice);
                Ticket ticket = ticketBooth.SellTicket(amountRemovedTicket);

                // The "Bottoh o wottoh" process.
                decimal waterPrice = ticketBooth.WaterBottlePrice;
                if (this.wallet.MoneyBalance < waterPrice)
                {
                    this.WithdrawMoney(waterPrice *= 2);
                }
                decimal amountRemovedWater = wallet.RemoveMoney(waterPrice);
                WaterBottle water = ticketBooth.SellWaterBottle(amountRemovedWater);
                this.bag.Add(water);
                return ticket;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }

        /// <summary>
        /// Remove money from checking into the guests wallet.
        /// </summary>
        /// <param name="amount">Amount in $.</param>
        public void WithdrawMoney(decimal amount)
        {
            this.checkingAccount.RemoveMoney(amount);
            this.wallet.AddMoney(amount);
            this.ToString();
        }

        /// <summary>
        /// Override the string to output the guest stats.
        /// </summary>
        /// <returns>The guest stats.</returns>
        public override string ToString()
        {
            return $"Name: Age [$MoneyBalance / $Account]\n{name}: {age} [${wallet.MoneyBalance} / ${checkingAccount.MoneyBalance}]" +
                $" \n Adopted Animal: {this.AdoptedAnimal}";
        }
    }
}