using ECommerce.Entities.Concrete;
using ECommerce.UI.Consts;
using ECommerce.UI.Extentions;

namespace ECommerce.UI.Services
{
    public class CartSessionService : ICartSessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CartSessionService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Cart? GetCart()
        {
            Cart cartToCheck = _contextAccessor.HttpContext.Session.GetObject<Cart>(WebUIConsts.CartName);
            if(cartToCheck == null)
            {
                _contextAccessor.HttpContext.Session.SetObject(WebUIConsts.CartName, new Cart());
                cartToCheck = _contextAccessor.HttpContext.Session.GetObject<Cart>(WebUIConsts.CartName);
            }
            return cartToCheck;
        }

        public void SetCart(Cart cart)
        {
            _contextAccessor.HttpContext.Session.SetObject(WebUIConsts.CartName, cart);
        }
    }
}
