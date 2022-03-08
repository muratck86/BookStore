using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi5.Common;


namespace WebApi5.Entities 
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreEnum GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}