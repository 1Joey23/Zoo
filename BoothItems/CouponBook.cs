using System;

namespace BoothItems
{
    public class CouponBook : Item
    {
        private DateTime dateMade;
        private DateTime dateExpired;

        // Constructor to initialize the fields
        public CouponBook(DateTime dateMade, DateTime dateExpired, double weight)
            : base(weight)
        {
            this.dateMade = dateMade;
            this.dateExpired = dateExpired;
        }

        // Properties for accessing the private fields
        public DateTime DateMade
        {
            get { return dateMade; }
        }

        public DateTime DateExpired
        {
            get { return dateExpired; }
        }
    }

}
