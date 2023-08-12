using BuisnessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.ReqModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class NoteBuisness : INoteBuisness
    {
        private INoteRepo iNotes;

        public NoteBuisness(INoteRepo Notes)
        {
            this.iNotes = Notes;

        }



        public NoteEntity NoteReg(NotesModel notesModel, int userID)
        {
            return iNotes.NoteReg(notesModel,userID);
        }

        public List<NoteEntity> GetAllNotes()
        {
            return iNotes.GetAllNotes();
        }

        public bool DeleteNotes()
        {
            return iNotes.DeleteNotes();
        }

        public NoteEntity EditNote(int userId, int noteID, string noteDes)
        {
           return iNotes.EditNote(userId, noteID, noteDes);
        }

        public NoteEntity EditNotes(int noteID, int userID, NotesModel notesModel)
        {
            return iNotes.EditNotes(noteID, userID, notesModel);
        }

        public bool TrashNote(int noteId)
        {
            return iNotes.TrashNote(noteId);
        }

        public bool ChangePin(int noteId)
        {
            return iNotes.ChangePin(noteId);
        }

        public NoteEntity DeleteNote(int noteId, int userId)
        {
            return iNotes.DeleteNote(noteId, userId);
        }

        public bool Archive(int noteId)
        {
            return iNotes.IsArchive(noteId);
        }

        public string ChangeColourNote(int noteId, string usercolour)
        {
            return iNotes.ChangeColourNote(noteId, usercolour);
        }

        public DateTime reminder(int noteId, DateTime reminder, int userId)
        {
            return iNotes.reminder(noteId, reminder, userId);
        }

        public string UploadImage(string filePath, long notesId, long userId)
        {
            return iNotes.UploadImage(filePath, notesId, userId);
        }

        public List<NoteEntity> NoteExist(string notetitle, int userId)
        {
            return iNotes.NoteExist(notetitle, userId);
        }
    }
}
