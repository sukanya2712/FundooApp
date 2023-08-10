using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int labelId { get; set; }

        public string labelName { get; set; }  
        


        [ForeignKey("Users")]
        public int userID { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [ForeignKey("Notes")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public virtual NoteEntity Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
