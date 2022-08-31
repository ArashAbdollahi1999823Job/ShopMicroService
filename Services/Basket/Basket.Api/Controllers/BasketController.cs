#region Using
using System.Net;
using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace Basket.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        #region CtorAndInjection
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        #endregion

        #region GetBasket
        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCard),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCard(userName)); //<= if not exist any basket by this userName create new one
        }
        #endregion

        #region UpdateBasket
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCard), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCard>> UpDateBasket([FromBody] ShoppingCard updatedShoppingCard)
        {
            return
                Ok(await _basketRepository.UpdateBasket(
                    updatedShoppingCard)); //<=Get new instance of shoppingCard and update old shoppingCard then returned UpdatedOne #endregion

        }
        #endregion

        #region DeleteBasket
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
        #endregion
    }
}
