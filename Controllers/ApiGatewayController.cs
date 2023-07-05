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
        private IAuthenticationService _authenticationService;
        private IInventoryService _inventoryService;
        private IReviewService _reviewService;
        private IOrderService _orderService;

        public ApiGatewayController( IAuthenticationService authenticationService, IInventoryService inventoryService, IReviewService reviewService, IOrderService orderService = null)
        {

            _authenticationService = authenticationService;
            _inventoryService = inventoryService;
            _reviewService = reviewService;
            _orderService = orderService;
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


    }
}
