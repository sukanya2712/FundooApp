using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace CommonLayer.ReqModels
{
    public class NotesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }


        public int UserId { get; set; }
        public string Title { get; set; }
        public string TakeNote { get; set; }
        public string ImageToAdd { get; set; }
        public string Colour { get; set; }
        public bool ArchiveNote { get; set; }
        public bool Trash { get; set; }
        public DateTime Reminder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool pinNote { get; set; }
        public bool unPinNote { get; set; }
    }
}
