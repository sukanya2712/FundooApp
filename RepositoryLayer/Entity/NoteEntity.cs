using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }

        [ForeignKey("Users")]
        public int userID { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }
        public string Title { get; set; }
        public string TakeNote { get; set; } //descriptionofnote
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
