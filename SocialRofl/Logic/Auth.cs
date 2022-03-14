using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialRofl.Data;
using SocialRofl.Exceptions;
using SocialRofl.Models;
using SocialRofl.Models.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialRofl.Logic
{

    public class Auth
    {
        private UserManager<User> _userManager;
        private DataContext _db;
        private IConfiguration _config;

        public Auth(UserManager<User> userManager, DataContext db, IConfiguration config)
        {
            _userManager = userManager;
            _db = db;
            _config = config;
        }

        public void Create(Register model)
        {
            if (_db.Users.Any(x => x.UserName == model.UserName))
            {
                throw new UserAlreadyExistsException("Username already taken");
            }
            _userManager.CreateAsync(new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedTime = DateTime.Now,
                UserName = model.UserName
            }, model.Password).Wait();
        }

        public LoginResult Login(string userName, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                throw new BadUserPasswordException("Password and username doesn't match");
            }
            if (_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                return new LoginResult { Token = GenerateToken(user) };
            }
            throw new BadUserPasswordException("Password and username doesn't match");
        }

        public string GenerateToken(User user)
        {
            var mySecret = _config.GetValue<string>("SecurityKey");
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}