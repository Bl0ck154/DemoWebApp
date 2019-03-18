using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplication.Entities
{
    [Table("subjects")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(64)]
        public string Name { get; set; }

        public Faculty Faculty { get; set; }

        public int FacultyId { get; set; }

        public Teacher Teacher { get; set; }

        public int TeacherId { get; set; }
    }
}
