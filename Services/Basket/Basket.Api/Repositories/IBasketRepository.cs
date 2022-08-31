#region Using
using Basket.Api.Entities;
using StackExchange.Redis;

#endregion
namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        #region CRUD
        Task<ShoppingCard> GetBasket(string userName);
        Task<ShoppingCard> UpdateBasket(ShoppingCard updatedShoppingCard);
        Task DeleteBasket(string userName);
        #endregion
    }
}
