using LibrarySystem.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    public class Book : ISearchable
    {
        public string ISBN { get; set; }
        public string Title { get; private set; }

        public string Author { get; private set; }
        public string PublishedYear { get; private set; }

        public bool IsAvailable { get; private set; }

        public Book(string isbn, string title, string author, string publishedYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true; // New books are available by default
        }

        public string GetInfo()
        {
            var status = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $" {Title} av {Author} ({PublishedYear}) - {status}";
        }

        public bool Matches(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return Title.ToLower().Contains(searchTerm)
                || Author.ToLower().Contains(searchTerm)
                || ISBN.ToLower().Contains(searchTerm);
        }

        public void MarkAsBorrowed() => IsAvailable = false;
        public void MarkAsReturned() => IsAvailable = true;
    }
}


