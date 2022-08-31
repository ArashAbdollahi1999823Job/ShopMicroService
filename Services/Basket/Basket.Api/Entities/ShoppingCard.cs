#region Using
#endregion

namespace Basket.Api.Entities
{
    public class ShoppingCard
    {
        #region Properties
        public string UserName { get; set; }
        #endregion

        #region Relation
        public List<ShoppingCardItem> ShoppingCardItems { get; set; } = new List<ShoppingCardItem>();
        #endregion

        #region TwoCtors
        public ShoppingCard()
        {
        }
        public ShoppingCard(string userName)
        {
            UserName = userName;
        }
        #endregion

        #region MethodTotalPrice
        public decimal totalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var shoppingCardItem in ShoppingCardItems)
                {
                    totalPrice += shoppingCardItem.Price * shoppingCardItem.Quantity;
                }
                return totalPrice;
            }
        }

        #endregion
    }
}
