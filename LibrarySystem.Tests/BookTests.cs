using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void constructor_ShouldSetPropertiesCorrectly()
        {
           var book = new Book ("123", "Testbok", "Författare", "2020");
           Assert.Equal("123", book.ISBN);
           Assert.True(book.IsAvailable);

        }
    }
}
