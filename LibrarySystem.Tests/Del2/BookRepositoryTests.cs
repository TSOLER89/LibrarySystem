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


        
        [Fact]
        // updating a book's title using BookRepository.UpdateAsync correctly persists changes to the database.
        public async Task UpdateAsync_ShouldUpdateBook()
        {
            using var context = CreateContext();
            var book = new Book { ISBN = "9", Title = "Old", Author = "A", PublishedYear = 2000 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var repo = new BookRepository(context);

            book.Title = "New";
            await repo.UpdateAsync(book);

            var updated = await context.Books.FirstAsync(b => b.ISBN == "9");
            Assert.Equal("New", updated.Title);
        }

        [Fact]
        //This test ensures that DeleteAsync properly removes a book from the database by
        //creating, saving, deleting, and verifying the absence of the book.


        public async Task DeleteAsync_ShouldRemoveBook()
        {
            using var context = CreateContext();
            var book = new Book { ISBN = "X", Title = "ToDelete", Author = "A", PublishedYear = 2000 };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var repo = new BookRepository(context);

            await repo.DeleteAsync(book.Id);

            Assert.False(await context.Books.AnyAsync(b => b.ISBN == "X"));
        }

        [Fact]
        // This test checks that GetByISBNAsync correctly retrieves a book based on its ISBN by
        // adding multiple books to the context and verifying the correct one is returned.
        public async Task GetByISBNAsync_ShouldReturnCorrectBook()
        {
            using var context = CreateContext();
            context.Books.AddRange(
                new Book { ISBN = "111", Title = "First", Author = "A", PublishedYear = 1999 },
                new Book { ISBN = "222", Title = "Second", Author = "B", PublishedYear = 2005 }
            );
            await context.SaveChangesAsync();

            var repo = new BookRepository(context);

            var found = await repo.GetByISBNAsync("222");

            Assert.NotNull(found);
            Assert.Equal("Second", found!.Title);
        }

        [Fact]
        // This test validates that SearchAsync can find books by matching the search term against the title, author, or ISBN.
        public async Task SearchAsync_ShouldFindBooksByTitle_Author_Or_ISBN()
        {
            using var context = CreateContext();
            context.Books.AddRange(
                new Book { ISBN = "AAA", Title = "Clean Code", Author = "Robert Martin", PublishedYear = 2008 },
                new Book { ISBN = "BBB", Title = "1984", Author = "George Orwell", PublishedYear = 1949 },
                new Book { ISBN = "CCC", Title = "Hobbiten", Author = "Tolkien", PublishedYear = 1937 }
            );
            await context.SaveChangesAsync();

            var repo = new BookRepository(context);

            var byTitle = (await repo.SearchAsync("Clean")).ToList(); // Should match "Clean Code"
            var byAuthor = (await repo.SearchAsync("Orwell")).ToList(); // Should match "1984"
            var byIsbn = (await repo.SearchAsync("CCC")).ToList(); // Should match "Hobbiten"

            Assert.Single(byTitle);
            Assert.Equal("AAA", byTitle[0].ISBN); // Verify that the correct book is returned based on the title search

            Assert.Single(byAuthor);
            Assert.Equal("BBB", byAuthor[0].ISBN); // Verify that the correct book is returned based on the author search

            Assert.Single(byIsbn);
            Assert.Equal("CCC", byIsbn[0].ISBN); // Verify that the correct book is returned based on the ISBN search
        }

    }

}

