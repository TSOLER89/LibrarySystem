using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem;

public class Program
{
    public static void Main(string[] args)
    {
        var books = new List<Book>
        {
            new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", "1954"),
            new Book("978-91-0-987654-3", "Hobbiten", "J.R.R. Tolkien", "1937")
        };

        var members = new List<Member>
        {
            new Member(1, "Anna Andersson"),
            new Member(2, "Erik Svensson")
        };

        var loans = new List<Loan>();
        var library = new Library(books, loans);
        var catalog = new BookCatalog(books);

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Bibliotekssystem ===");
            Console.WriteLine("1. Visa alla böcker");
            Console.WriteLine("2. Sök bok");
            Console.WriteLine("3. Låna bok");
            Console.WriteLine("4. Returnera bok");
            Console.WriteLine("5. Visa medlemmar");
            Console.WriteLine("6. Statistik");
            Console.WriteLine("0. Avsluta");
            Console.Write("\nVälj: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach (var book in books)
                    {
                        Console.WriteLine(
                            $"\"{book.Title}\" av {book.Author} ({book.PublishedYear}) - {GetBookStatus(book, loans)}");
                    }
                    break;

                case "2":
                    Console.Write("\nSökterm: ");
                    var term = Console.ReadLine();

                    var results = catalog.SearchByTitle(term);

                    Console.WriteLine("\nSökresultat:");
                    int index = 1;
                    foreach (var book in results)
                    {
                        Console.WriteLine(
                            $"{index++}. \"{book.Title}\" av {book.Author} ({book.PublishedYear}) - {GetBookStatus(book, loans)}");
                    }
                    break;

                case "3":
                    Console.Write("\nAnge ISBN: ");
                    var isbn = Console.ReadLine();

                    Console.Write("Ange medlems-ID: ");
                    int memberId = int.Parse(Console.ReadLine());

                    var bookToLoan = books.FirstOrDefault(b => b.ISBN == isbn);
                    var member = members.FirstOrDefault(m => m.Id == memberId);

                    if (bookToLoan == null || member == null)
                    {
                        Console.WriteLine("Felaktigt ISBN eller medlems-ID.");
                        break;
                    }

                    loans.Add(new Loan(bookToLoan, member, DateTime.Now));

                    Console.WriteLine(
                        $"Boken \"{bookToLoan.Title}\" har lånats ut till {member.Name}.");
                    Console.WriteLine(
                        $"Återlämningsdatum: {DateTime.Now.AddDays(14):yyyy-MM-dd}");
                    break;

                case "4":
                    Console.Write("\nAnge ISBN: ");
                    var returnIsbn = Console.ReadLine();

                    var loan = loans.FirstOrDefault(l => l.Book.ISBN == returnIsbn);

                    if (loan == null)
                    {
                        Console.WriteLine("Boken är inte utlånad.");
                        break;
                    }

                    loans.Remove(loan);
                    Console.WriteLine($"Boken \"{loan.Book.Title}\" har returnerats.");
                    break;

                case "5":
                    foreach (var m in members)
                    {
                        Console.WriteLine($"{m.Id}: {m.Name}");
                    }
                    break;

                case "6":
                    Console.WriteLine($"Totalt antal böcker: {library.TotalBooks()}");
                    Console.WriteLine($"Utlånade böcker: {library.BorrowedBooksCount()}");
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }
    }

    static string GetBookStatus(Book book, List<Loan> loans)
    {
        return loans.Any(l => l.Book.ISBN == book.ISBN)
            ? "Utlånad"
            : "Tillgänglig";
    }
}
