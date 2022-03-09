using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi5.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; } 
        public DateTime BirthDate { get; set; }
    }
}