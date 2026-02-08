using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using System;
using System.Collections.Generic;

namespace LibrarySystem;

public class Program
{
    public static void Main(string[] args)
    {
        var books = new List<Book>
        {
            new Book("1", "1984", "George Orwell", "1949"),
            new Book("2", "The Hobbit", "Tolkien", "1937")
        };

        var loans = new List<Loan>();

        var library = new Library(books, loans);

        Console.WriteLine("=== Library System ===");
        Console.WriteLine($"Total books: {library.TotalBooks()}");
        Console.WriteLine($"Borrowed books: {library.BorrowedBooksCount()}");
    }
}
