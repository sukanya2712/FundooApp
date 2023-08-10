using BuisnessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.ReqModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using System;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : Controller
    {
        private ILabelBuisness labelBuisness;
        private ILogger<LabelController> logger;

        public LabelController(ILabelBuisness labelBuisness, ILogger<LabelController> logger)
        {
            this.labelBuisness = labelBuisness;
            this.logger = logger;
        }

        [HttpPost("AddLabel")]
        public ActionResult AddLabel(int noteId,int userId,LabelModel model)
        {
            try
            {
                
                var result = labelBuisness.AddLabel(noteId, userId,model);
                if(result != null)
                {
                    logger.LogInformation("label added");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "sucessful added label", Data = result });
                }
                else
                   return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "unsucessful enterd", Data = null });

            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        

        [HttpPost("GetLabelsById")]
        public ActionResult GetLabelsById(int noteId, int labelId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = labelBuisness.GetLabelsById(noteId,userID, labelId);
                if (result != null)
                {
                    logger.LogInformation("label displayed");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "sucessful displayed label", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "unsucessful", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

       

        [HttpPost("removeLabel")]
        public ActionResult removeLabel(int noteId, int labelId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = labelBuisness.GetLabelsById(noteId, userID, labelId);
                if (result != null)
                {
                    logger.LogInformation("Note added");
                    return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "remove label", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "unsucessful", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
