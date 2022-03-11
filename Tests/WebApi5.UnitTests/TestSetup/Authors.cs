using System;
using WebApi5.DbOperations;
using WebApi5.Entities;

namespace WebApi5.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Eric", LastName = "Ries", BirthDate = new DateTime(1978, 9, 22) },
                new Author { Name = "Charlotte Perkins", LastName = "Gilman", BirthDate = new DateTime(1860, 7, 3) },
                new Author { Name = "Frank", LastName = "Herbert", BirthDate = new DateTime(1920, 10, 8) },
                new Author { Name = "Sabahattin", LastName = "Ali", BirthDate = new DateTime(1907, 2, 25)}
            );
        }
    }
}