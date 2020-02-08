using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Surfprize.DAL;
using Surfprize.Entity;
using Surfprize.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Globalization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Surfprize.Models.Account;
using Surfprize.Core;

namespace Surfprize.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserService userService;

        public AccountController(IServiceScopeFactory scopeFactory,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService
            )
            : base(scopeFactory, unitOfWork, httpContextAccessor)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userService.GetByEmailAsync(model.Email);
                if (user != null)
                {
                    bool isCorrectPassword = PasswordHasher.VerifyHashedPassword(user.Password, model.Password);
                    if (isCorrectPassword)
                    {
                        DateTime now = DateTime.UtcNow;

                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, now.Second.ToString(), ClaimValueTypes.Integer64),
                            new Claim("UserId", user.UserId.ToString()),
                            new Claim("ExpireDate", now.Add(TimeSpan.FromDays(1)).ToString(CultureInfo.InvariantCulture)),
                            new Claim("Email", model.Email),
                            new Claim("Role", user.Role.ToString())
                        };

                        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                        byte[] key = Encoding.ASCII.GetBytes("mysupersecret_secretkey!WB");
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        SecurityToken SecurityToken = tokenHandler.CreateToken(tokenDescriptor);
                        string token = tokenHandler.WriteToken(SecurityToken);

                        return ApiResult(new SignInResponseModel { Token = token });
                    }
                    return Error("Error", "Incorrect password!");
                }
                return Error("Error", "Incorrect Email!");
            }

            return Error(ModelState);
        }
    }
}
