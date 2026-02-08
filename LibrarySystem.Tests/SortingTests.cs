using Xunit;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;


namespace LibrarySystem.Tests;

public class SortingTests
{
    [Fact]
    public void Should_Sort_Books_bY_Title()
    {
        var books = new List<Book>
        {
                new Book("1", "C", "A", "2000"),
                new Book("2", "B", "A", "2001"),
                new Book("3", "A", "B", "2002")
        };
        var catalog = new BookCatalog(books);

        var sorted = catalog.SortByTitle();

        Assert.Equal("A", sorted[0].Title);
        Assert.Equal("B", sorted[1].Title);
        Assert.Equal("C", sorted[2].Title);
    }
}

        
