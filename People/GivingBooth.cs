using BoothItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class GivingBooth : Booth
    {
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            // Create coupon books with unique timestamps within the loop
            for (int i = 0; i < 5; i++)
            {
                DateTime expirationDate = DateTime.Now.AddYears(1);
                double weight = 0.8; // Assuming weight is a decimal
                CouponBook couponBook = new CouponBook(DateTime.Now, expirationDate, weight);
                Items.Add(couponBook);
            }

            // Create maps with unique timestamps within the loop
            for (int i = 0; i < 10; i++)
            {
                Map map = new Map(0.5, DateTime.Now); // Assuming weight is a decimal
                Items.Add(map);
            }
        }



        /// <summary>
        /// Give the guest a coupon book.
        /// </summary>
        /// <returns>The free coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
                CouponBook couponBook = (CouponBook)Attendant.FindItems(this.Items, typeof(CouponBook));
                return couponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }

        /// <summary>
        /// Give the guest a free map.
        /// </summary>
        /// <returns> The map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                Map map = (Map)Attendant.FindItems(this.Items, typeof(Map));
                return map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException(ex.Message);
            }
        }
    }
}
