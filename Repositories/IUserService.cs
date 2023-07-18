using ApiGatewayService.Models;

namespace ApiGatewayService.Repositories
{
    public interface IUserService
    {
        public Task<User?> GetUserAsync(int userId);
        public Task<User?> CreateUserAsync(User user);
        public Task<IEnumerable<UserAddress>> GetAddressesOfUserAsync(int userId);

        public Task<UserAddress?> PostUserAddressOfUserAsync(UserAddress userAddress);

        public Task<bool> DeleteUserAddressOfUserAsync(int userAddressId);
    }
}
