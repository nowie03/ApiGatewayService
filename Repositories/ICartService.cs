using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface ICartService
    {
        public Task<Cart?> GetCartForUserAsync(int userId);

        public Task<IEnumerable<CartItem>> GetCartItemsFromCartAsync(int cartId);

      
        public Task<CartItem?> PostCartItemToCartAsync(CartItem cartItem);
        public Task<bool> CheckOutCartAsync(int cartId);

        public Task<bool> DeleteCartItemFromCartAsync(int cartItemId);  
    }
}
