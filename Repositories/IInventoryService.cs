using ApiGatewayService.Models;
using ApiGatewayService.ResponseModels;

namespace ApiGatewayService.Repositories
{
    public interface IInventoryService
    {

        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<IEnumerable<ProductGetResponse>> GetProductsAsync(int limit, int skip);
        public Task<ProductGetResponse> GetProductAsync(int productId);
      


    }
}
