namespace BoothItems
{
    public class WaterBottle : SoldItem
    {
        private int serialNumber;

        // Constructor to initialize the fields
        public WaterBottle(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        // Properties for accessing the private fields
        public int SerialNumber
        {
            get { return serialNumber; }
        }
    }

}
