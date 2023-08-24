using CommonLayer.Models;
using CommonLayer.ReqModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private FundooDBContext Context;
        private readonly IConfiguration configuration;

        public UserRepo(FundooDBContext Context, IConfiguration configuration)
        {
            this.Context = Context;
            this.configuration = configuration;

        }

        public UserEntity UserReg(RegModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                bool emailExists = Context.Users.Any(x => x.Email == registrationModel.Email);
                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                userEntity.Password = EncodePasswordToBase64(registrationModel.Password);
                userEntity.CreatedAt = DateTime.Now;
                userEntity.UpdatedAt = DateTime.Now;

                if (!emailExists)
                {
                    Context.Users.Add(userEntity);
                    Context.SaveChanges();
                    return userEntity;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                string EncodedPassword = EncodePasswordToBase64(loginModel.Password);
                var CheckEmailExist = Context.Users.FirstOrDefault(e => e.Email == loginModel.Email);
                if (CheckEmailExist != null)
                {
                    var PassWord = Context.Users.FirstOrDefault(e => e.Password == EncodedPassword);

                    if (PassWord != null)
                    {
                        var token = GenerateToken(CheckEmailExist.Email, CheckEmailExist.userID);
                        return token;
                    }
                    else
                    {
                        return null;
                    }

                }
                return null;
            }
            catch (Exception ex) {throw ex; }
        }

        private string GenerateToken(string Email, int ID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserID",ID.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ForgetPasswordModel UserForgotPassword(string email)
        {
            try
            {
                var result = Context.Users.Where(x => x.Email == email).FirstOrDefault();
                ForgetPasswordModel forgetPassWordModel = new ForgetPasswordModel();
                forgetPassWordModel.Email = result.Email;
                forgetPassWordModel.Token = GenerateToken(result.Email, result.userID);
                forgetPassWordModel.UserID = result.userID;
                return forgetPassWordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckEmail(string email)
        {
            try
            {
                bool emailexists = Context.Users.Any(x => x.Email == email);
                if (emailexists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResetPasswordModel ResetPassword( string email, ResetPasswordModel resetPassword)
        {
            try
            {
                if (resetPassword.ConfirmPassword.Equals(resetPassword.password))
                {
                    var result = Context.Users.Where(x => x.Email == email).FirstOrDefault();
                    result.Password = EncodePasswordToBase64(resetPassword.password);
                    Context.SaveChanges();
                }
                return resetPassword;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public UserEntity LoginObject(LoginModel loginModel)
        {
            try
            {
                string EncodedPassword = EncodePasswordToBase64(loginModel.Password);
                var CheckEmailExist = Context.Users.FirstOrDefault(e => e.Email == loginModel.Email && e.Password == EncodedPassword);
                if (CheckEmailExist != null)
                {
                    return CheckEmailExist;
                  
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
