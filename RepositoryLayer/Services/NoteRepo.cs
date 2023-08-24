using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.ReqModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;
using System.Linq;
using System.Text;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly  FundooDBContext Context;
        private readonly IConfiguration configuration;

        public NoteRepo(FundooDBContext Context, IConfiguration configuration)
        {
            this.Context = Context;
            this.configuration = configuration;

        }

        public NoteEntity NoteReg(NotesModel notesModel,int UserID)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.userID = UserID;

                noteEntity.Title = notesModel.Title;
                noteEntity.TakeNote = notesModel.TakeNote;
                noteEntity.ImageToAdd = notesModel.ImageToAdd;
                noteEntity.Colour = notesModel.Colour;
                noteEntity.ArchiveNote = notesModel.ArchiveNote;
                noteEntity.Trash = notesModel.Trash;
                noteEntity.Reminder = notesModel.Reminder;
                noteEntity.CreatedAt = DateTime.Now;
                noteEntity.UpdatedAt = DateTime.Now;
                noteEntity.pinNote = notesModel.pinNote;
                noteEntity.unPinNote = notesModel.unPinNote;
                Context.Notes.Add(noteEntity);
                Context.SaveChanges();
                return noteEntity;
            }
            catch (Exception ex) { throw ex; }
        }


        public List<NoteEntity> GetAllNotes(int UserID)
        {
            try
            {
                int id = Context.Users.Where(x=>x.userID == UserID).Select(x=> x.userID).FirstOrDefault();
                bool idExist = Context.Notes.Any(x=>x.userID == id);
                if (idExist)
                {
                    List<NoteEntity> resultNotes = Context.Notes.ToList();
                    return resultNotes;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNotes()
        {
            try
            {
                List<NoteEntity> resultNotes = Context.Notes.ToList();
                Context.Notes.RemoveRange(resultNotes);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity DeleteNote(int noteId,int userId)
        {
            try 
            { 
                NoteEntity noteEntity = Context.Notes.FirstOrDefault(e => e.NoteId == noteId && e.userID==userId);
                if (noteEntity != null)
                {
                    Context.Notes.Remove(noteEntity);
                    Context.SaveChanges();
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public NoteEntity EditNote(int userId,int noteID,string noteDes)
        {
            try
            {
                NoteEntity noteEntity = Context.Notes.Where(e => e.NoteId == noteID).FirstOrDefault();
                if (noteEntity != null)
                {
                    if (noteEntity.userID == userId)
                    {
                        noteEntity.TakeNote = noteDes;
                        Context.SaveChanges();
                        return noteEntity;
                    }

                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity EditNotes(int noteID,int userID,NotesModel notesModel)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = Context.Notes.Where(x => x.NoteId == noteID && x.userID == userID).FirstOrDefault();
                if (noteEntity != null)
                {

                    noteEntity.Title = notesModel.Title;
                    noteEntity.TakeNote = notesModel.TakeNote;
                    noteEntity.ImageToAdd = notesModel.ImageToAdd;
                    noteEntity.Colour = notesModel.Colour;
                    noteEntity.ArchiveNote = notesModel.ArchiveNote;
                    noteEntity.Trash = notesModel.Trash;
                    noteEntity.Reminder = notesModel.Reminder;

                    noteEntity.UpdatedAt = DateTime.Now;
                    noteEntity.pinNote = notesModel.pinNote;
                    noteEntity.unPinNote = notesModel.unPinNote;

                    Context.SaveChanges();
                    return noteEntity;
                }
                return null;
            }catch(Exception ex) { throw ex; } 
           
        }

        public bool ChangePin(int noteId)
        {
            try
            {
                NoteEntity result = this.Context.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (result.pinNote == false)
                {
                    result.pinNote = true;
                    result.unPinNote = false;
                    Context.SaveChanges();
                    return result.pinNote;

                }
                else
                {
                    result.pinNote = false;
                    result.unPinNote = true;
                    Context.SaveChanges();
                    return result.pinNote;
                }
            }catch(Exception ex) { throw ex; }
        }

        public bool TrashNote(int noteId)
        {
            try
            {
                NoteEntity note = this.Context.Notes.FirstOrDefault(x=>x.NoteId == noteId);
                if (note.Trash)
                {
                    note.Trash = false;
                    Context.SaveChanges() ;
                    return note.Trash;
                }
                else
                {
                    note.Trash = true;
                    return note.Trash;
                }
            }catch(Exception ex) { throw ex; }
        }

        public bool IsArchive(int noteId)
        {
            try
            {
                NoteEntity note = this.Context.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if(note.ArchiveNote)
                {
                    note.ArchiveNote = false;
                    Context.SaveChanges();
                    return note.ArchiveNote;
                }
                else
                {
                    note.ArchiveNote = true;
                    Context.SaveChanges();
                    return note.ArchiveNote;
                }
            }catch(Exception ex) { throw ex; }
        }

        public string ChangeColourNote(int noteId,string usercolour)
        {
            try
            {
                NoteEntity note = this.Context.Notes.FirstOrDefault(x=>x.NoteId==noteId);
                if(note != null)
                {
                    note.Colour = usercolour;
                    Context.SaveChanges();
                    return note.Colour;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { throw ex; }   
        }

        public DateTime reminder(int noteId,DateTime reminder,int userId)
        {
            try
            {
                NoteEntity note = this.Context.Notes.FirstOrDefault(x => x.NoteId == noteId && x.userID==userId);
                if(note != null)
                {
                    note.Reminder = reminder;
                    Context.SaveChanges();
                    return note.Reminder;
                }
                return note.Reminder;

            }
            catch( Exception ex ) { throw ex; }
        }




       /* public ImageUploadResult UploadImage(IFormFile imagePath)
        {
            Account account = new Account(configuration["Cloudinary: CloudName"], configuration["Cloudinary: ApiKey"], configuration["Cloudinary: ApiSecret"]);
            Cloudinary cloud = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {

                File = new FileDescription(imagePath.FileName, imagePath.OpenReadStream()),
            };
            var uploadImageRes = cloud.Upload(uploadParams);
            if (uploadImageRes != null)
            {
                return uploadImageRes;
            }
            else
            {
                return null;
            }

        }*/

        public string UploadImage(string filePath, long notesId, long userId)
        {
            var filterUser = Context.Notes.Where(e => e.userID == userId);
            if (filterUser != null)
            {
                var findNotes = filterUser.FirstOrDefault(e => e.NoteId == notesId);
                if (findNotes != null )
                {
                    
                        Account account = new Account("detjidukc", "656178876553924", "W4sISF1GLDvwXuO2lpiIiC8TreE");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = findNotes.Title
                        };

                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.ImageToAdd = uploadResult.Url.ToString();
                        Context.SaveChanges();
                        return "Upload Successfull";
                        
                    
                    
                }
                return null;
            }
            else { return null; }
        }



        public List<NoteEntity> NoteExist(string notetitle,int userId)
        {
            try
            {
                
                List<NoteEntity> notes = this.Context.Notes.Where(x => x.Title == notetitle && x.userID == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                return null;

            }
            catch(Exception ex) { throw ex; }   
        }



    }
}
