using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Entities
{
    [Table("studentsmarks")]
    public class StudentMark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]    
        public int Id { get; set; }


        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int MarkId { get; set; }
        public Mark Mark { get; set; }
    }
}
