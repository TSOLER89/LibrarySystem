using LibrarySystem.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    // Represents a book in the library system
    public class Book : ISearchable
    {
        public string ISBN { get; set; }
        public string Title { get; private set; }

        public string Author { get; private set; }
        public string PublishedYear { get; private set; }

        public bool IsAvailable { get; private set; }


        // Constructor to initialize a new book
        public Book(string isbn, string title, string author, string publishedYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true; // New books are available by default
        }


        // Returns a string with the book's information
        public string GetInfo()
        {
            var status = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $" {Title} av {Author} ({PublishedYear}) - {status}";
        }


        // Checks if the book matches a search term (by title, author, or ISBN)
        public bool Matches(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            return Title.ToLower().Contains(searchTerm)
                || Author.ToLower().Contains(searchTerm)
                || ISBN.ToLower().Contains(searchTerm);
        }

        // Marks the book as borrowed (not available)
        public void MarkAsBorrowed() => IsAvailable = false;
        public void MarkAsReturned() => IsAvailable = true;
    }
}


