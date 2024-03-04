namespace BoothItems
{
    public class Ticket : SoldItem
    {
        private bool isRedeemed;
        private int serialNumber;

        // Constructor to initialize the fields
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.isRedeemed = false;  // Initially set to false
            this.serialNumber = serialNumber;
        }

        // Properties for accessing the private fields
        public bool IsRedeemed
        {
            get { return isRedeemed; }
        }

        public int SerialNumber
        {
            get { return serialNumber; }
        }

        // Method to redeem the ticket
        public void Redeem()
        {
            isRedeemed = true;
        }
    }

}
