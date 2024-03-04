using System;

namespace BoothItems
{
    public class Map : Item
    {
        private DateTime dateIssued;

        // Constructor to initialize the fields
        public Map(double weight, DateTime dateIssued)
            : base(weight)
        {
            this.dateIssued = dateIssued;
        }

        // Properties for accessing the private fields
        public DateTime DateIssued
        {
            get { return dateIssued; }
        }
    }

}
