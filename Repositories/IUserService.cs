using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface IUserService
    {
        public Task<IEnumerable<UserAddress>> GetAddressesOfUserAsync(int userId);

        public Task<UserAddress?> PostUserAddressOfUserAsync(UserAddress userAddress);

        public Task<bool> DeleteUserAddressOfUserAsync(int userAddressId);
    }
}
