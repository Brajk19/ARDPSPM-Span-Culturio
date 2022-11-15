using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Span.Culturio.Api.Data;
using Span.Culturio.Api.Helper;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Service.User
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(RegisterUserDto user)
        {
            var userEntity = _mapper.Map<Data.Entity.User>(user);
            PasswordHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            _context.Users.Add(userEntity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                return null; // unique constraint violation
            }
            

            var userDto = _mapper.Map<UserDto>(userEntity); 
            return userDto;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var users = await _context.Users.FindAsync(id);
            var userDto = _mapper.Map<UserDto>(users);

            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            var user = await _context.Users.Where(x => x.Username.Equals(username)).FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<bool> Login(UserDto user, string password)
        {
            var userEntity = await _context.Users.FindAsync(user.Id);
            if (userEntity is not null && PasswordHelper.VerifyPasswordHash(password, userEntity.PasswordHash, userEntity.PasswordSalt))
            {
                return true;
            }

            return false;
        }
    }
}
