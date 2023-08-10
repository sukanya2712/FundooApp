using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabEntity AddCollaborator(int userId, int noteId, string Email);

        public List<CollabEntity> DisplayCollaborator(int userId);

        public CollabEntity RemoveCollab(int userId);

        public CollabEntity RemoveCollabratorfromnote(int userId, int noteId, int collabId);

        public List<NoteEntity> GetAllNotesByCollab(int userId, int collabId);

        public List<CollabEntity> GetAllCollabbyNoteId(int userId);
    }
}