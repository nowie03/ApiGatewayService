using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class ReviewService : IReviewService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS = "http://localhost:5030/api/Reviews";
        public ReviewService(HttpClient httpClient )
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId)
        {
            try {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}?productID={productId}");

                if(response.IsSuccessStatusCode)
                {
                    string content=await response.Content.ReadAsStringAsync();

                    IEnumerable<Review> reviews=JsonConvert.DeserializeObject<IEnumerable<Review>>(content);

                    return reviews;
                }

                return Enumerable.Empty<Review>();
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<Review>();

            }
        }

        public  async Task<Review?> PostReviewForProductAsync(Review review)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}", review);

                if(response.IsSuccessStatusCode)
                {
                    return review;
                }
                return null;
            }
            catch(Exception ex) { 

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Review?> UpdateReviewForProductAsync(int reviewId, Review review)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BASE_ADDRESS}/{reviewId}", review);

                if (response.IsSuccessStatusCode)
                {
                    return review;
                }
                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
