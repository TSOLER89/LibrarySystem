using Xunit;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class SearchTests
    {
        [Fact]
        public void Book_Should_Match_Search_By_Title_Or_Author()
        {
            var book = new Book("1", "1984", "George Orwell", 1949);

            Assert.True(book.Matches("1984"));
            Assert.True(book.Matches("George Orwell"));
            Assert.False(book.Matches("Brave New World"));
        }
    }
}