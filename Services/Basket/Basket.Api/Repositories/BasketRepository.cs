#region Using
using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
#endregion

namespace Basket.Api.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        #region CtorAndInjection
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        #endregion

        #region GetBasket
        public async Task<ShoppingCard> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket)) return null;

            return JsonConvert.DeserializeObject<ShoppingCard>(basket);
        }
        #endregion

        #region UpdateBasket
        public async Task<ShoppingCard> UpdateBasket(ShoppingCard updatedShoppingCard)
        {
            await _redisCache.SetStringAsync(updatedShoppingCard.UserName, JsonConvert.SerializeObject(updatedShoppingCard));

            return await GetBasket(updatedShoppingCard.UserName);
        }
        #endregion

        #region DeleteBasket
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
        #endregion
    }
}
