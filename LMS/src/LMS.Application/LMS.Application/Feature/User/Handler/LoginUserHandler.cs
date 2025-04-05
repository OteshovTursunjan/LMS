using LMS.Application.DTO.User;
using LMS.Application.Feature.User.Command;
using LMS.Domain.Enum;
using LMS.Domain.Identity;
using LMS.Infrastructure.Repository;
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
        private readonly ITeacherRepository _teacherRepository;
        public LoginUserHandler(UserManager<ApplicationUser> userManager, ITeacherRepository teacherRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _teacherRepository = teacherRepository;
        }

        public async Task<ReturnRegisterModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.LoginUser.Email);
            var teacherlastname = await _teacherRepository.GetFirstAsync(u => u.LastName == user.LastName);
            var firstname = await _teacherRepository.GetFirstAsync(u => u.FirstName == user.FirstName);
            if (user == null)
            {
                return null;
            }

            bool checkPassword = await _userManager.CheckPasswordAsync(user, request.LoginUser.Password);

            if (teacherlastname != null && firstname != null)
            {


                var jwtSecrets = _configuration["JwtOptions:SecretKey"]
                    ?? throw new InvalidOperationException("JWT Secret Key is not configured");
                var jwtIssuers = _configuration["JwtOptions:Issuer"]
                    ?? throw new InvalidOperationException("JWT Issuer is not configured");
                var jwtAudiences = _configuration["JwtOptions:Audience"]
                    ?? throw new InvalidOperationException("JWT Audience is not configured");
                var jwtExpiryMinutess = _configuration["JwtOptions:ExpirationInMinutes"]
                    ?? throw new InvalidOperationException("JWT Expiration In Minutes is not configured");
                var claimss = new List<Claim>
        {
            new Claim(CustomClaim.Email, user.Email),
            new Claim(ClaimTypes.Role, "Teacher"),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
        };
                var authSigningKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecrets));
                var accessTokens = new JwtSecurityToken(
                    issuer: jwtIssuers,
                    audience: jwtAudiences,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtExpiryMinutess)),
                    claims: claimss,
                    signingCredentials: new SigningCredentials(authSigningKeys, SecurityAlgorithms.HmacSha256)
                );


                return new ReturnRegisterModel
                {
                    Email = request.LoginUser.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(accessTokens),
                    RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    DateTime = accessTokens.ValidTo,
                    LastName = user.LastName,
                    FirstName = user.FirstName
                };
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

