using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using ApiGatewayService.RequestModels;
using ApiGatewayService.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IInventoryService _inventoryService;
        private readonly IReviewService _reviewService;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public ApiGatewayController( IAuthenticationService authenticationService
            , IInventoryService inventoryService
            , IReviewService reviewService
            , IOrderService orderService
            ,ICartService cartService
            ,IPaymentService paymentService
            ,IUserService userService)
            
        {

            _authenticationService = authenticationService;
            _inventoryService = inventoryService;
            _reviewService = reviewService;
            _orderService = orderService;
            _cartService = cartService;
            _paymentService= paymentService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {

            var response = await _authenticationService.Login(request);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<User?>> Signup(User user)
        {

            var response = await _authenticationService.SignUp(user);

            if(response==null)return BadRequest();

            return Ok(response);
        }


        [HttpGet]
        [Route("products/category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var response =await _inventoryService.GetCategoriesAsync();

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IEnumerable<ProductGetResponse>>> GetProducts(int limit,int skip)
        {
            var response = await _inventoryService.GetProductsAsync(limit,skip);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("product")]
        public async Task<ActionResult<ProductGetResponse>> GetProduct(int id)
        {
            var response = await _inventoryService.GetProductAsync(id);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpGet]
        [Route("reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int productId)
        {
            var response = await _reviewService.GetReviewsForProductAsync(productId) ;

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("reviews")]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            var response = await _reviewService.PostReviewForProductAsync(review);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpPut]
        [Route("reviews")]
        public async Task<ActionResult<Review>> PutReview(int reviewId,Review review)
        {
            var response = await _reviewService.UpdateReviewForProductAsync(reviewId,review);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int userId)
        {
            var response = await _orderService.GetOrdersForUserAsync(userId);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("orders")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var response = await _orderService.PostOrderForUserAsync(order);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("order/checkout")]
        public async Task<ActionResult<bool>> CheckoutOrder(int orderId)
        {
            var response = await _orderService.CheckOutOrderAsync(orderId);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpPut]
        [Route("orders")]
        public async Task<ActionResult<Review>> PutOrder(int orderId, Order Order)
        {
            var response = await _orderService.PutOrderForUserAsync(orderId, Order);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpDelete]
        [Route("orders")]
        public async Task<ActionResult<bool>>DeleteOrder(int orderId)
        {
            var response = await _orderService.DeleteOrderForUserAsync(orderId);

            if(response==false)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("cart")]
        public async Task<ActionResult<Cart>> GetCart(int userId)
        {
            var response=await _cartService.GetCartForUserAsync(userId);

            if (response == null)
            {
                return BadRequest(response);
            }

                return Ok(response);
        }

        [HttpGet]
        [Route("cart/cartItems")]
        public async Task<ActionResult<Cart>> GetCartItems(int cartId)
        {
            var response = await _cartService.GetCartItemsFromCartAsync(cartId);

            if (response == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("cart/checkout")]
        public async Task<ActionResult<bool>> CheckOutCart(int cartId)
        {
            var response=await _cartService.CheckOutCartAsync(cartId);

            if(response==false)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPost]
        [Route("cart")]
        public async Task<ActionResult<CartItem>> AddItemToCart(CartItem cartItem)
        {
           

            var response = await _cartService.PostCartItemToCartAsync(cartItem);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }


        [HttpDelete]
        [Route("carts")]
        public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId,int orderId)
        {

            //delete order then delete cartitem
             
            var orderDeletResponse= await _orderService.DeleteOrderForUserAsync(orderId);

            if (orderDeletResponse == false)
                return BadRequest(orderDeletResponse);
                
            var cartItemDeleteResponse =await _cartService.DeleteCartItemFromCartAsync(cartItemId);

            if (cartItemDeleteResponse == false)
                return BadRequest(cartItemDeleteResponse);

            return Ok(true);
        }

        [HttpGet]
        [Route("payments")]
        public async Task<ActionResult<Payment>> GetPaymentsForOrder(int orderId)
        {
            var response = await _paymentService.GetPaymentForOrderAsync(orderId);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPut]
        [Route("payments")]
        public async Task<ActionResult<Payment>> UpdatePayment(int paymentId,Payment payment)
        {
            var response=await _paymentService.UpdateStatusForPaymentAsync(paymentId, payment);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        [Route("address")]
        public async Task<ActionResult<IEnumerable<UserAddress>>>GetUserAddress(int userId)
        {
            var response=await _userService.GetAddressesOfUserAsync(userId);

            if(response==null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("address")]
        public async Task<ActionResult<UserAddress>> PostUserAddress(UserAddress userAddress)
        {
            var response = await _userService.PostUserAddressOfUserAsync(userAddress);

            if (response == null)
                return BadRequest();

            return Ok(userAddress);
        }

        [HttpDelete]
        [Route("address")]
        public async Task<ActionResult<bool>>DeleteUserAddress(int addressId)
        {
            var resposne = await _userService.DeleteUserAddressOfUserAsync(addressId);

            if (resposne == false)
                return BadRequest();

            return Ok(true);
        }
    }
}
