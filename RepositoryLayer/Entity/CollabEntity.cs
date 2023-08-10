using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collabId { get; set; }

        public string Email { get; set; }

        [ForeignKey("NoteCollab")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public virtual NoteEntity NoteCollab { get; set; }

        [ForeignKey("UserCollab")]
        public int userID { get; set; }

        [JsonIgnore]
        public virtual UserEntity UserCollab { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
