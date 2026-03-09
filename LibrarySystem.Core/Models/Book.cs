using LibrarySystem.Core.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Core.Models
{
    public class Book : ISearchable
    {
        // Primary key for EF Core
        public int Id { get; set; }

        [Required(ErrorMessage = "ISBN är obligatoriskt")]
        [RegularExpression(@"^(?:\d{10}|\d{13})$",
            ErrorMessage = "ISBN måste vara 10 eller 13 siffror")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Titel är obligatorisk")]
        [StringLength(200, MinimumLength = 1,
            ErrorMessage = "Titeln måste vara mellan 1 och 200 tecken")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Författare är obligatorisk")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Författarens namn måste vara mellan 2 och 100 tecken")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publiceringsår är obligatoriskt")]
        [Range(1450, 2026,
            ErrorMessage = "Publiceringsår måste vara mellan 1450 och 2026")]
        public int PublishedYear { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation property (for EF Core relation)
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        // Empty constructor required by EF Core
        public Book() { }

        // original constructor 
        public Book(string isbn, string title, string author, int publishedYear)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true;
        }

        // Method to get a formatted string with the book's information.
        public string GetInfo()
        {
            var status = IsAvailable ? "Tillgänglig" : "Utlånad";
            return $"{Title} av {Author} ({PublishedYear}) - {status}";
        }

        // Implementing the ISearchable interface method to
        // check if the search term matches any of the book's properties.
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