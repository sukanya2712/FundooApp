using CommonLayer.Models;
using CommonLayer.ReqModels;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(RegModel registrationModel);
        public string Login(LoginModel loginModel);

        public ForgetPasswordModel UserForgotPassword(string email);

        public bool CheckEmail(string email);

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPassword);
    }
}