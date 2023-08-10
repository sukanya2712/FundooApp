using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooDBContext Context;

        public CollabRepo(FundooDBContext Context)
        {
            this.Context = Context;
        }

        public CollabEntity AddCollaborator(int userId, int noteId, string Email)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                var result = Context.Notes.Where(x => x.NoteId == noteId && x.userID == userId).FirstOrDefault();
                if (result!=null)
                {
                    
                        collabEntity.Email = Email;
                        collabEntity.NoteId = noteId;
                        collabEntity.userID = userId;
                        collabEntity.CreatedAt = DateTime.Now;
                        collabEntity.UpdatedAt = DateTime.Now;
                        Context.Collabs.Add(collabEntity);
                        Context.SaveChanges();
                        return collabEntity;
                    
                    
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<CollabEntity> DisplayCollaborator(int userId)
        {
            try
            {
                List<CollabEntity> collabList = new List<CollabEntity>();
                collabList = Context.Collabs.Where(x => x.userID == userId).ToList();
                if(collabList.Count > 0)
                {
                    return collabList;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public CollabEntity RemoveCollab(int userId)
        {
            try
            {
                CollabEntity collab = Context.Collabs.FirstOrDefault(x => x.userID == userId);
                if (collab != null)
                {
                    Context.Collabs.Remove(collab);
                    Context.SaveChanges();
                    return collab;
                }
                return null;
            }
            catch(Exception ex) { throw ex; }   
        }

        public CollabEntity RemoveCollabratorfromnote(int userId,int noteId,int collabId)
        {
            try
            {
                CollabEntity collab = Context.Collabs.FirstOrDefault(x => x.userID == userId && x.NoteId==noteId && x.collabId==collabId);
                if (collab != null )
                {

                    Context.Collabs.Remove(collab);
                    Context.SaveChanges();
                    return collab;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }


        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId)
        {
            try
            {
                List<NoteEntity> collabNotes = Context.Collabs.Where(c => c.userID == userId && c.collabId == collabId).Join(Context.Notes, x => x.NoteId, y => y.NoteId, (x, y) => y).ToList();
                if (collabNotes != null)
                {
                    return collabNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CollabEntity> GetAllCollabbyNoteId(int userId)
        {
            try
            {
                List<CollabEntity> collabEntities = new List<CollabEntity>();
                collabEntities = Context.Collabs.Where(x=>x.userID==userId).ToList();
                if(collabEntities != null)
                {

                    return collabEntities;
                }
                return null;
            }
            catch(Exception ex) { throw ex; }
        }

        

    }
}
