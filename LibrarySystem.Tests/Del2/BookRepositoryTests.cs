using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LibrarySystem.Data.Repository;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests.Del2
{
    public class BookRepositoryTests
    {
        // Test for adding a book to the repository
        private static LibraryContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryContext(options);
        }

        [Fact]
        // This test verifies that adding a book to the repository saves it to the database
        public async Task AddAsync_ShouldSaveBookToDatabase()
        {
            using var context = CreateContext();
            var repo = new BookRepository(context);

            var book = new Book { ISBN = "123", Title = "Test", Author = "A", PublishedYear = 2024 };

            await repo.AddAsync(book);

            var saved = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "123");
            Assert.NotNull(saved);
            Assert.Equal("Test", saved!.Title);
        }

        [Fact]
        
        //should retun all books
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            using var context = CreateContext();
            context.Books.AddRange(
                new Book { ISBN = "1", Title = "A", Author = "X", PublishedYear = 2000 },
                new Book { ISBN = "2", Title = "B", Author = "Y", PublishedYear = 2001 }
            );
            await context.SaveChangesAsync();

            var repo = new BookRepository(context);

            var all = (await repo.GetAllAsync()).ToList();

            Assert.Equal(2, all.Count);
        }

    }
}
