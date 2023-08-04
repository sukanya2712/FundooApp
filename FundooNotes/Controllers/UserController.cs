using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Models;
using CommonLayer.ReqModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using RepositoryLayer.Entity;
using System.Threading.Tasks;
using System;
using MassTransit;
using Microsoft.AspNetCore.Authorization;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBus _bus;
        private IUserBuisness buisness;

        public UserController(IUserBuisness buisness, IBus _bus)
        {
            this.buisness = buisness;
            this._bus = _bus;

        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME /METHODNAME/METHODROUTE =>REGURL
        [Route("Register")]
        public ActionResult Registration(RegModel model) {
            var result = buisness.UserReg(model);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "sucessful registered", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "unsucessful registered", Data = null });
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel loginModel)
        { var result = buisness.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "sucessful login", Data = result });
            } else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "unsucessful login", Data = null });
            }

        }

        [HttpPost("forgot-password")]

        public async Task<IActionResult> UserForgotPassword(string email)
        {
           
                if (buisness.CheckEmail(email))
                {
                    Send send = new Send();
                    ForgetPasswordModel forgotPasswordModel = buisness.UserForgetPassword(email);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FundoNotesEmail_Queue");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { Success = true, Message= "email send succesfull", Data = email });
                }
                return BadRequest(new ResponseModel<string> { Success = true, Message = "email send succesfull", Data = email });
            
        }

        [Authorize]
        [HttpPost("reset-password")]
        public ActionResult ResetUserPassword(ResetPasswordModel resetPassword)
        {
            string Email = User.FindFirst(x => x.Type == "Email").Value;
            var result = buisness.ResetPassword(Email, resetPassword);
            if (result != null)
            {
                return Ok(new ResponseModel<ResetPasswordModel> { Success = true, Message = "sucessful login", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<ResetPasswordModel> { Success = false, Message = "unsucessful login", Data = null });
            }

        }
    }
    }

