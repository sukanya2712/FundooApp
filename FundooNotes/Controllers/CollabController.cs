using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Models;
using CommonLayer.ReqModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : Controller
    {
        private ICollabBuisness collabBuisness;
        private ILogger<CollabController> logger;

        public CollabController(ICollabBuisness collabBuisness, ILogger<CollabController> logger)
        {
            this.collabBuisness = collabBuisness;
            this.logger = logger;
        }

        [HttpPost("AddCollaborator")]
        public ActionResult AddCollaborator( int noteId, string Email)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = collabBuisness.AddCollaborator(userID, noteId, Email);
                if (result != null)
                {
                    logger.LogInformation("collaborator added");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "sucessful added collabrator", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "unsucessful", Data = null });

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        
        }

        [HttpPost("DisplayCollaborator")]
       
        public ActionResult DisplayCollaborator()
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = collabBuisness.DisplayCollaborator(userID);
                if (result != null)
                {
                    logger.LogInformation("collab displayed");
                    return Ok(new ResponseModel<List<CollabEntity>>{ Success = true, Message = "sucessful displayed collabrator", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<List<CollabEntity>>{ Success = false, Message = "unsucessful", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpDelete("RemoveCollaboratorfromAllnotes")] 
        public ActionResult RemoveCollaboratorfromAllnotes() 
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = collabBuisness.RemoveCollab(userID);
                if (result != null)
                {
                    logger.LogInformation("collab removed");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "sucessful displayed collabrator", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "unsucessful", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpDelete("RemoveCollabratorfromnote")]
        public ActionResult RemoveCollabratorfromnote( int noteId, int collabId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = collabBuisness.RemoveCollabratorfromnote(userID, noteId, collabId);
                if (result != null)
                {
                    logger.LogInformation("collab removed");
                    return Ok(new ResponseModel<CollabEntity> { Success = true, Message = "sucessful displayed collabrator", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<CollabEntity> { Success = false, Message = "unsucessful", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }






        [HttpGet("GetAllNotesByCollab")]
        public ActionResult GetAllNotesByCollab(int collabId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                //string email = this.User.FindFirst("Email").Value; ;
                var result = collabBuisness.GetAllNotesByCollab(userID, collabId);
                if (result != null)
                {
                    logger.LogInformation("Note added");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "sucessful displayed collabrator", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<List<NoteEntity>>{ Success = false, Message = "unsucessful", Data = null });

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }


      
        [HttpGet("GetAllCollabbyNoteId")]
        public ActionResult GetAllCollabbyNoteId()
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                //string email = this.User.FindFirst("Email").Value; ;
                var result = collabBuisness.GetAllCollabbyNoteId(userID);
                if (result != null)
                {
                    logger.LogInformation("Note Displayed");
                    return Ok(new ResponseModel<List<CollabEntity>> { Success = true, Message = "sucessful displayed collas with noteId", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<List<CollabEntity>> { Success = false, Message = "unsucessful", Data = null });

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

    }


}
