using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface IReviewService
    {
        public Task<IEnumerable<Review>> GetReviewsForProductAsync(int productId);

        public Task<Review?> PostReviewForProductAsync(Review review);


        public Task<Review?> UpdateReviewForProductAsync(int reviewId, Review review);


    }
}
