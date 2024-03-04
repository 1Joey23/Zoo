using BoothItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;
using Animals;

namespace People
{
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The price for a "bottoh o wottah".
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The moneycollector to handle the moneybox.
        /// </summary>
        private IMoneyCollector moneyBox;

        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;

            // For loop to make tickets.
            for (int i = 0; i < 5; i++)
            {
                Ticket ticket = new Ticket(ticketPrice, i, 0.01);
                Items.Add(ticket);
            }

            // For loop to create water bottles.
            for (int i = 0; i < 5; i++)
            {
                WaterBottle waterBottle = new WaterBottle(waterBottlePrice, i, 1.0);
                Items.Add(waterBottle);
            }
        }

        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Get the ticket price.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return ticketPrice;
            }
        }

        /// <summary>
        /// Get the waterbottle price.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return waterBottlePrice;
            }
        }

        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        public decimal RemoveMoney(decimal amount) 
        {
            this.moneyBox.RemoveMoney(amount);
            return amount;
        }

        /// <summary>
        /// Sell the ticket to the guest.
        /// </summary>
        /// <param name="payment">The guest's payment</param>
        /// <returns>The paid ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Ticket ticket = null;

            try
            {
                if (payment >= 15.00m)
                {
                    ticket = (Ticket)Attendant.FindItems(this.Items, typeof(Ticket));
                }

                if (ticket != null)
                {
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found", ex);
            }
            return ticket;  // Implicitly return null if payment is insufficient
        }

        /// <summary>
        /// Sell the water bottle to the guest.
        /// </summary>
        /// <param name="payment">The cost of the water bottle.</param>
        /// <returns>The paid water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            WaterBottle waterBottle = null;
            try
            {
                if (payment >= 3.00m)
                {
                    waterBottle = (WaterBottle)Attendant.FindItems(this.Items, typeof(WaterBottle));
                }

                if (waterBottle != null)
                {
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException(ex.Message);
            }
            return waterBottle;
        }
    }
}
