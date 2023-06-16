using AutoMapper;
using DictinoaryTemplate.Common.Infrastructure;
using DictinoaryTemplate.Common.Infrastructure.Exceptions;
using DictinoaryTemplate.Common.Models.Queries;
using DictinoaryTemplate.Common.Models.RequestModels;
using DictionaryTemplate.Api.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DictionaryTemplate.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository UserRepository;
        private readonly IMapper Mapper;
        private readonly IConfiguration Configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            UserRepository = userRepository;
            Mapper = mapper;
            Configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await UserRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found");

            var password = PasswordEncryptor.Encrypt(request.Password);

            if (dbUser.Password != password)
                throw new DatabaseValidationException("Password is wrong");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet");

            var result = Mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.EmailAddress),
                new Claim(ClaimTypes.Name, dbUser.UserName),
                new Claim(ClaimTypes.GivenName, dbUser.FirstName),
                new Claim(ClaimTypes.Surname, dbUser.LastName),
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);
            var expiry = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claims, 
                expires: expiry, 
                signingCredentials: credentials, 
                notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
