using CommonLayer.ReqModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface INoteRepo
    {
        //public NoteEntity NoteReg(NotesModel notesModel,int id);;
        public NoteEntity NoteReg(NotesModel notesModel, int userID);

        public List<NoteEntity> GetAllNotes(int UserID);

        public bool DeleteNotes();

        public NoteEntity EditNote(int userId, int noteID, string noteDes);

        public NoteEntity EditNotes(int noteID, int userID, NotesModel notesModel);

        public bool TrashNote(int noteId);

        public bool ChangePin(int noteId);

        public NoteEntity DeleteNote(int noteId, int userId);

        public bool IsArchive(int noteId);

        public string ChangeColourNote(int noteId, string usercolour);

        public DateTime reminder(int noteId, DateTime reminder, int userId);

        public string UploadImage(string filePath, long notesId, long userId);

        public List<NoteEntity> NoteExist(string notetitle, int userId);
    }
}