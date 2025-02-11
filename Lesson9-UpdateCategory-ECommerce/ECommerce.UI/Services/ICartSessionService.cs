using ECommerce.Entities.Concrete;

namespace ECommerce.UI.Services
{
    public interface ICartSessionService
    {
        Cart? GetCart();
        void SetCart(Cart cart);
    }
}
