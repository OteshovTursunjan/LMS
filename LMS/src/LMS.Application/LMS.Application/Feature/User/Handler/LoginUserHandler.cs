using LMS.Application.DTO.User;
using LMS.Application.Feature.User.Command;
using LMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.User.Handler
{
    public  class LoginUserHandler : IRequestHandler<LoginUserCommand , ReturnRegisterModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public LoginUserHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ReturnRegisterModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.LoginUser.Email);
            if (user == null)
            {
                return null;
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, request.LoginUser.Password);
            if (!checkPassword)
            {
                return null;
            }

            var jwtSecret = _configuration["JwtOptions:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured");
            var jwtIssuer = _configuration["JwtOptions:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured");
            var jwtAudience = _configuration["JwtOptions:Audience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured");
            var jwtExpiryMinutes = _configuration["JwtOptions:ExpirationInMinutes"]
                ?? throw new InvalidOperationException("JWT Expiration In Minutes is not configured");
            var claims = new List<Claim>
        {
            new Claim(CustomClaim.Email, user.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
        };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var accessToken = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtExpiryMinutes)),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );



            return new ReturnRegisterModel
            {
                Email = request.LoginUser.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateTime = accessToken.ValidTo,
                LastName = user.LastName,
                FirstName = user.FirstName
            };
        }
    }
 }

