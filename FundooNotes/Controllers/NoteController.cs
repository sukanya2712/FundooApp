using Automatonymous.Binders;
using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer.Models;
using CommonLayer.ReqModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {


        private INoteBuisness noteBuisness;
        private ILogger<NoteController> logger;
        private readonly IDistributedCache _cache;

        public NoteController(INoteBuisness noteBuisness, ILogger<NoteController> logger, IDistributedCache _cache)
        {
            this.noteBuisness = noteBuisness;
            this.logger = logger;
            this._cache = _cache;
        }

        [HttpPost]
        //LOCALHOST CONTROLLERNAME /METHODNAME/METHODROUTE =>REGURL
        [Route("TakeNote")]
        public ActionResult TakeNote(NotesModel model)
        {
            try
            {
                //int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                int userID = (int)HttpContext.Session.GetInt32("userId");


                var result = noteBuisness.NoteReg(model, userID);

                if (result != null)
                {
                    logger.LogInformation("Note added");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "sucessful entered", Data = result });
                }
                else
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "unsucessful enterd", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;

            }

        }




        [HttpGet("GetAllTheNotes")]

        public ActionResult GetAllTheNotes()
        {
            try
            {
                var result = noteBuisness.GetAllNotes();
                if (result != null)
                {
                    logger.LogInformation("getting all the notes");
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "sucessful ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "unsucessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;

            }
        }



        [HttpDelete("DeleteAllNote")]
        public ActionResult DeleteAllNote()
        {
            try
            {
                var result = noteBuisness.DeleteNotes();
                if (result)
                {
                    logger.LogInformation("deleted succefully");
                    return Ok(new ResponseModel<bool> { Success = true, Message = "sucessful ", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "unsucessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;

            }

        }


        [HttpPatch("EditNote")]
        public ActionResult EditNote(int noteID, string takenoteDes)
        {
            try
            {
                string userId = User.FindFirst("userID").Value;
                int userID = Convert.ToInt32(userId);


                NoteEntity noteEntity = noteBuisness.EditNote(userID, noteID, takenoteDes);
                if (noteEntity != null)
                {
                    logger.LogInformation("Edited succefully");
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "note updated succesfully", Data = noteEntity });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "note updated unsuccesfully", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

        }

        [HttpPatch("EditNotes")]
        [Authorize]
        public ActionResult EditNotes(int noteID, NotesModel notesModel)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("userID").Value);
                var result = noteBuisness.EditNotes(noteID, userId, notesModel);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "note updated succesfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "note updated unsuccesfully", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

        }


        [HttpPost("TrashNotes")]
        public ActionResult TrashNotes(int noteID)
        {
            try
            {
                bool result = noteBuisness.TrashNote(noteID);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "note trashed", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "note untrashed", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

        }


        [HttpPut("ChangePin")]
        public ActionResult ChangePin(int noteId)
        {
            try
            {
                bool result = noteBuisness.ChangePin(noteId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Pin Changed", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Pin UnChanged", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }

        }

        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                NoteEntity noteEntity = noteBuisness.DeleteNote(noteId, userID);
                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Note Deleted", Data = noteEntity });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "not deleted", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpPut("ArchiveNote")]
        public ActionResult ArchiveNote(int noteId)
        {
            try
            {
                bool result = noteBuisness.Archive(noteId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "Note Archived", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note UnArchived", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }


        [HttpPatch("NoteColour")]
        public ActionResult NoteColour(int noteId, string usercolour)
        {
            try
            {
                var result = noteBuisness.ChangeColourNote(noteId, usercolour);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Colour changed", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Colour unchanged", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }


        [HttpPatch("ChangeReminder")]

        public ActionResult ChangeReminder(int noteId, DateTime reminder)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("userID").Value);
                var result = noteBuisness.reminder(noteId, reminder, userId);
                if (result == reminder)
                {
                    return Ok(new ResponseModel<DateTime> { Success = true, Message = "Reminder changed", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<DateTime> { Success = false, Message = "Reminder unchanged", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        [HttpPost("UploadImage")]

        public ActionResult UploadImage(string filePath, long notesId)
        {
            try
            {
                int userId = Convert.ToInt32(this.User.FindFirst("userID").Value);
                var result = noteBuisness.UploadImage(filePath, notesId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "image uploaded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "not uploaded", Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetAllNotesUsingaRedis")]
        public async Task<IActionResult> GetAllNotesUsingaRedis()
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedisNoteList = await _cache.GetAsync(CacheKey);
                if (RedisNoteList != null)
                {
                    logger.LogDebug("Getting list from redis cache");
                    var SerializedNotesList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerializedNotesList);
                }
                else
                {
                    logger.LogDebug("setting the list to cache which is requested for the first time");
                    NoteList = (List<NoteEntity>)noteBuisness.GetAllNotes();
                    var SerializedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SerializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(10));


                    await _cache.SetAsync(CacheKey, redisNoteList, options);
                }
                logger.LogInformation("Got noteslist sucessfully from redis");
                return Ok(NoteList);
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex, "Exception thrown");
                return BadRequest(new { Sucess = false, Message = ex.Message });
            }
       
        }

        [HttpGet("NoteExist")]
        public ActionResult NoteExist(string notetitle)
        {
           
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("userId").Value);
                var result = noteBuisness.NoteExist(notetitle,userID);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "image uploaded", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Success = false, Message = "not uploaded", Data = result });
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
