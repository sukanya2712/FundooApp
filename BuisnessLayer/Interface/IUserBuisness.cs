using CommonLayer.Models;
using CommonLayer.ReqModels;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Interface
{
    public interface IUserBuisness
    {
        public UserEntity UserReg(RegModel registrationModel);
        public string Login(LoginModel loginModel);

        public ForgetPasswordModel UserForgetPassword(string email);
        public bool CheckEmail(string email);

        public ResetPasswordModel ResetPassword(string email, ResetPasswordModel resetPassword);

        public UserEntity LoginObject(LoginModel loginModel);
    }


}