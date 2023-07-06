using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class CartService : ICartService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS = "http://localhost:5020/api/Carts";

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CheckOutCartAsync(int cartId)
        {
           try{
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}/checkout?cartId={cartId}",cartId);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCartItemFromCartAsync(int cartItemId)
        {
           try{

                var response = await _httpClient.DeleteAsync($"{BASE_ADDRESS}/cartItems?cartItemId={cartItemId}");

                if(response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Cart?> GetCartForUserAsync(int userId)
        {
           try{
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content=await response.Content.ReadAsStringAsync();

                    Cart cart=JsonConvert.DeserializeObject<Cart>(content);

                    return cart;
                }

                return null;
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsFromCartAsync(int cartId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/cartItems?cartId={cartId}");

                if(response.IsSuccessStatusCode)
                {
                    string content= await response.Content.ReadAsStringAsync();

                    IEnumerable<CartItem> cartItems=JsonConvert.DeserializeObject<IEnumerable<CartItem>>(content);

                    return cartItems;
                }

                return new List<CartItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CartItem>();

            }
        }

        public async Task<CartItem?> PostCartItemToCartAsync(CartItem cartItem)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}/cartItems", cartItem);

                if(response.IsSuccessStatusCode)
                {
                    return cartItem;
                }

                return null;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            
            }
        }

        
    }
}
