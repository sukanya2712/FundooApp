using BuisnessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class CollabBuisness : ICollabBuisness
    {
        private ICollabRepo icollabRepo;

        public CollabBuisness(ICollabRepo icollabRepo)
        {
            this.icollabRepo = icollabRepo;
        }
        public CollabEntity AddCollaborator(int userId, int noteId, string Email)
        {
            return icollabRepo.AddCollaborator(userId, noteId, Email);
        }

        public List<CollabEntity> DisplayCollaborator(int userId)
        {
            return icollabRepo.DisplayCollaborator(userId);
        }

        public CollabEntity RemoveCollab(int userId)
        {
            return icollabRepo.RemoveCollab(userId);
        }

        public CollabEntity RemoveCollabratorfromnote(int userId, int noteId, int collabId)
        {
            return icollabRepo.RemoveCollabratorfromnote(userId, noteId, collabId);
        }

        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId)
        {
            return icollabRepo.GetAllNotesByCollab(userId, collabId);
        }

        public List<CollabEntity> GetAllCollabbyNoteId(int userId)
        {
            return icollabRepo.GetAllCollabbyNoteId(userId);
        }
    }
}
