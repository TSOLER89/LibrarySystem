using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Services
{
    public class BookCatalog
    {
        private readonly List<Book> _books;
       
        public BookCatalog(List<Book> books)
        {
            _books = books;
        }

        public List<Book>SearchByTitle(string title)
        {
            return _books
                .Where(b => b.Title.Contains(title))
                .ToList();
        }
    }
}
