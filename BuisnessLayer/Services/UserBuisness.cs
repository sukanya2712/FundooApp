using BuisnessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Services;
using CommonLayer.ReqModels;
using CommonLayer;

namespace BuisnessLayer.Services
{
    public class UserBuisness : IUserBuisness
    {
        private IUserRepo user;

        public UserBuisness(IUserRepo user)
        {
            this.user = user;
        }
        public UserEntity UserReg(RegModel registrationModel)

        {
            return user.UserReg(registrationModel);
        }

        public string Login(LoginModel loginModel)
        {
           return user.Login(loginModel);
        }


        public bool CheckEmail(string email)
        {
            return user.CheckEmail(email);
        }

        public ForgetPasswordModel UserForgetPassword(string email)
        {
            return user.UserForgotPassword(email);
        }

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPassword)
        {
            return user.ResetPassword(email, resetPassword);
        }

        public UserEntity LoginObject(LoginModel loginModel)
        {
            return user.LoginObject(loginModel);
        }
    }
}
