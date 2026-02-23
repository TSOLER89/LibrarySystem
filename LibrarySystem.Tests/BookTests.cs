using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Book_Should_Be_Available_When_Created()
        {
            // Arrange & Act
            var book = new Book("1", "Test Book", "Test Author", 2024);

            // Assert
            Assert.True(book.IsAvailable);
        }
    }
}