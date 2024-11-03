﻿using DataAccessLayer.Data;
using EntityLayer.Interfaces;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly TaskDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        // Constructor with dependency injection for TaskDbContext and IConfiguration
        public UserRepo(TaskDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Login method with JWT token generation
        public string Login(Login login)
        {
            // Validate input
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.password))
                return "Invalid login credentials";

            // Fetch user details based on provided login credentials
            var user = _dbcontext.Users
                .FirstOrDefault(u => u.Email == login.Email && u.password == login.password);

            // If user is found, generate and return JWT token
            if (user != null)
            {
                var token =  GenerateJwtToken(user); // Token returned on successful login
                return token;
            }
            else
            {
                return "Invalid username or password";

            }
            
        }

        // Method to generate JWT token
        private string GenerateJwtToken(Users user)
        {
            var claims = new List<Claim>
            {
          new Claim("userId", user.userId.ToString()),
          new Claim(JwtRegisteredClaimNames.Email, user.Email),
        // Add other claims as needed
         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        // SignUp method to register a new user
        public object SignUp(Users users)
        {
            if (users == null) return "User details are required";

            _dbcontext.Users.Add(users);
            _dbcontext.SaveChanges();
            return "Account created successfully";
        }

        // Method to retrieve all users (useful for admin purposes or testing)
        public List<Users> GetAllUsers()
        {
            return _dbcontext.Users.ToList();
        }

        public Users GetUserByEmail(string email)
        {
            return _dbcontext.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool ResetPasswordWithToken(string email, string newPassword)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            user.password = newPassword;
            //user.PasswordResetToken = null;
            //user.TokenExpirationTime = null;
            _dbcontext.SaveChanges();
            return true;
        }

        public string GeneratePasswordResetToken(string email)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            var token = Guid.NewGuid().ToString();
            //user.PasswordResetToken = token;
            user.TokenExpirationTime = DateTime.Now.AddHours(1);

            _dbcontext.SaveChanges();
            return token;
        }

        public string GeneratePasswordResetOtp(string email)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();
            //user.PasswordResetToken = otp;
            user.TokenExpirationTime = DateTime.Now.AddMinutes(10); // Set a 10-minute expiration

            _dbcontext.SaveChanges();
            return otp;
        }

       
    }
}
