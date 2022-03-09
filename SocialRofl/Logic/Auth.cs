using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialRofl.Data;
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

        public RegisterResult Create(Register model)
        {
            var result = _userManager.CreateAsync(new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedTime = DateTime.Now,
                UserName = model.UserName
            }, model.Password).Result;
            return new RegisterResult { Success = result.Succeeded };
        }

        public LoginResult Login(string userName, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return new LoginResult { Success = false };
            }
            if (_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                return new LoginResult { Success = true, Token = GenerateToken(user) };
            }
            return new LoginResult { Success = false };
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