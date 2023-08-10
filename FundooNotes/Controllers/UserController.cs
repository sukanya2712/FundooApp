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
using Microsoft.Extensions.Logging;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBus _bus;
        private IUserBuisness buisness;
        private ILogger<NoteController> logger;

        public UserController(IUserBuisness buisness, IBus _bus, ILogger<NoteController> logger)
        {
            this.buisness = buisness;
            this._bus = _bus;
            this.logger=logger;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME /METHODNAME/METHODROUTE =>REGURL
        [Route("Register")]
        public ActionResult Registration(RegModel model) {
            try
            {
                var result = buisness.UserReg(model);
                if (result != null)
                {
                    logger.LogInformation("Registered succefully");
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "sucessful registered", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "unsucessful registered", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = buisness.Login(loginModel);
                if (result != null)
                {
                    logger.LogInformation("login succefully");
                    return Ok(new ResponseModel<string> { Success = true, Message = "sucessful login", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "unsucessful login", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpPost("forgot-password")]

        public async Task<IActionResult> UserForgotPassword(string email)
        {
            try
            {
                if (buisness.CheckEmail(email))
                {
                    Send send = new Send();
                    ForgetPasswordModel forgotPasswordModel = buisness.UserForgetPassword(email);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FundoNotesEmail_Queue");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { Success = true, Message = "email send succesfull", Data = email });
                }
                return BadRequest(new ResponseModel<string> { Success = true, Message = "email send succesfull", Data = email });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

        }

        [Authorize]
        [HttpPost("reset-password")]
        public ActionResult ResetUserPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                string Email = User.FindFirst(x => x.Type == "Email").Value;
                var result = buisness.ResetPassword(Email, resetPassword);
                if (result != null)
                {
                    logger.LogInformation("reset sucessfully");
                    return Ok(new ResponseModel<ResetPasswordModel> { Success = true, Message = "sucessful resetpassword", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<ResetPasswordModel> { Success = false, Message = "unsucessful resetpassword", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
            

        }

        
        [HttpPost("LoginUser")]
        public ActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                var result = buisness.LoginObject(loginModel);
                if (result != null)
                {
                    HttpContext.Session.SetInt32("userId", result.userID);
                    logger.LogInformation("login succefully");
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "sucessful login", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "unsucessful login", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
    }

