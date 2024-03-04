using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private WalletColor color;

        /// <summary>
        /// The amount of money currently contained within the wallet.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// The wallet works from the moneyCollector.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(WalletColor color, IMoneyCollector moneyPocket)
        {
            this.color = color;
            this.moneyPocket = moneyPocket;
        }

        public decimal MoneyBalance
        {
            get 
            { 
                return this.moneyPocket.MoneyBalance; 
            }
        }

        public WalletColor Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Adds a specified amount of money to the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// Removes a specified amount of money from the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            moneyPocket.RemoveMoney(amount);

            return amount;
        }
    }
}