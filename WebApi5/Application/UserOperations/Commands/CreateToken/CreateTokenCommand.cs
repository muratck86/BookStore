using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi5.DbOperations;
using WebApi5.TokenOperations;
using WebApi5.TokenOperations.Models;

namespace WebApi5.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public CreateTokenModel Model { get; set; }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(user => 
                user.Email == Model.Email && user.Password == Model.Password);
            if(user != null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Eposta veya şifre hatalı.");

        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}