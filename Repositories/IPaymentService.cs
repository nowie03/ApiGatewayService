using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface IPaymentService
    {
        public Task<Payment?> GetPaymentForOrderAsync(int orderId);

        public Task<Payment?> UpdateStatusForPaymentAsync(int paymentId, Payment payment);
    }
}
