using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using WebApi5.Common;
using WebApi5.Entities;

namespace WebApi5.DbOperations 
{
    class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = 
            new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre {
                        Name = "Personal Growth"
                    },
                    new Genre {
                        Name = "Science-Fiction"
                    },
                    new Genre {
                        Name = "Novel"
                    }
                );
                
                context.Books.AddRange(
                    new Book {
                        // Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, // Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 02, 06)
                    },
                    new Book {
                        // Id = 2,
                        Title = "Herland",
                        GenreId = 2, // Science-Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book {
                        // Id = 3,
                        Title = "Dune",
                        GenreId = 2, // Science-Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}