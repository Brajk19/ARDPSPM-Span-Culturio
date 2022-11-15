using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> CreateUser(RegisterUserDto user);
        Task<UserDto> GetUserByUsername(string username);
        Task<bool> Login(UserDto user, string password);
    }
}
