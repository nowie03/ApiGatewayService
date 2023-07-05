using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetOrdersForUserAsync(int userId);

        public Task<Order?> PostOrderForUserAsync(Order order);

        public Task<Order?> PutOrderForUserAsync(int orderId, Order order);

        public Task<bool> DeleteOrderForUserAsync(int orderId);

        public Task<bool> CheckOutOrderAsync(int orderId);
    }
}
