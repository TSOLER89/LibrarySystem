using Xunit;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

    public class SortingTests
{
    [Fact]
    public void Should_Sort_Books_bY_Title()
    {
        var books = new List<Book>
        {
                new Book("C", "A", "1", "2000"),
                new Book("B", "A", "2", "2001"),
                new Book("A", "B", "3", "2002")
        };
                   var catalog = new BookCatalog(books);

                    var sorted = catalog.SortByTitle();

                    Assert.Equal("A", sorted[0].Title);
                    Assert.Equal("B", sorted[1].Title);
                    Assert.Equal("C", sorted[2].Title);



                }
            }
        }

    }
}

