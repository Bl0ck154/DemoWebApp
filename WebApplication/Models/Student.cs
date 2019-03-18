using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Entities
{
    [Table("students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]    
        public int Id { get; set; }

        [Column("firstName")]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Column("lastName")]
        [StringLength(128)]
        public string LastName { get; set; }

        
        public Group Group { get; set; }

        public int GroupId { get; set; }

        public List<Mark> Marks { get; set; }
    }
}
