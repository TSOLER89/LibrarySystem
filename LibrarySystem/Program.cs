using LibrarySystem.Core;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using LibrarySystem.Core.Interface;
using System;
using System.Collections.Generic;

try
{
    Console.WriteLine("=== Bibliotekssystem ===\n");

    // Skapa böcker
    var books = new List<Book>
    {
        new Book("978-0451524935", "1984", "George Orwell", 1949),
        new Book("978-0547928227", "The Hobbit", "J.R.R. Tolkien", 1937),
        new Book("978-0060850524", "Brave New World", "Aldous Huxley", 1932),
        new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954)
    };

    // Skapa medlemmar
    var members = new List<Member>
    {
        new Member(1, "Anna Andersson", "anna@example.com", DateTime.Now.AddYears(-2)),
        new Member(2, "Bob Bengtsson", "bob@example.com", DateTime.Now.AddYears(-1)),
        new Member(3, "Carl Carlsson", "carl@example.com", DateTime.Now.AddMonths(-6))
    };

    // Skapa service-objekt med komposition
    var bookCatalog = new BookCatalog(books);
    var memberRegistry = new MemberRegistry(members);
    var loanManager = new LoanManager();

    // Skapa biblioteket
    var library = new Library(bookCatalog, memberRegistry, loanManager);

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

        try
        {
            switch (choice)
            {
                case "1":
                    ShowAllBooks(library);
                    break;
                case "2":
                    SearchBooks(library);
                    break;
                case "3":
                    BorrowBook(library);
                    break;
                case "4":
                    ReturnBook(library);
                    break;
                case "5":
                    ShowMembers(memberRegistry);
                    break;
                case "6":
                    ShowStatistics(library);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Tack för att du använde bibliotekssystemet!");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Fel: {ex.Message}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n!!! KRITISKT FEL !!!");
    Console.WriteLine($"Typ: {ex.GetType().Name}");
    Console.WriteLine($"Meddelande: {ex.Message}");
}

// Hjälpmetoder
void ShowAllBooks(Library library)
{
    Console.WriteLine("\n=== Alla böcker ===");
    var allBooks = library.GetAllBooksSorted();

    if (!allBooks.Any())
    {
        Console.WriteLine("Inga böcker i katalogen.");
        return;
    }

    foreach (var book in allBooks)
    {
        Console.WriteLine(book.GetInfo());
    }
}

void SearchBooks(Library library)
{
    Console.Write("\nSökterm: ");
    var searchTerm = Console.ReadLine();

    var results = library.SearchBooksByTitle(searchTerm);

    Console.WriteLine("\nSökresultat:");
    if (!results.Any())
    {
        Console.WriteLine("Inga böcker hittades.");
        return;
    }

    for (int i = 0; i < results.Count; i++)
    {
        var status = results[i].IsAvailable ? "Tillgänglig" : "Utlånad";
        Console.WriteLine($"{i + 1}. \"{results[i].Title}\" av {results[i].Author} ({results[i].PublishedYear}) - {status}");
    }
}

void BorrowBook(Library library)
{
    Console.Write("\nAnge ISBN: ");
    var isbn = Console.ReadLine();

    Console.Write("Ange medlems-ID: ");
    if (!int.TryParse(Console.ReadLine(), out int memberId))
    {
        Console.WriteLine("Ogiltigt medlems-ID.");
        return;
    }

    var loan = library.BorrowBook(isbn, memberId);
    var member = library.FindMember(memberId);

    Console.WriteLine($"✅ Boken \"{loan.Book.Title}\" har lånats ut till {member.Name}.");
    Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
}

void ReturnBook(Library library)
{
    Console.Write("\nAnge medlems-ID: ");
    if (!int.TryParse(Console.ReadLine(), out int memberId))
    {
        Console.WriteLine("Ogiltigt medlems-ID.");
        return;
    }

    var member = library.FindMember(memberId);
    var activeLoans = member.GetActiveLoans();

    if (!activeLoans.Any())
    {
        Console.WriteLine($"{member.Name} har inga aktiva lån.");
        return;
    }

    Console.WriteLine("\nAktiva lån:");
    for (int i = 0; i < activeLoans.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {activeLoans[i].Book.Title}");
    }

    Console.Write("\nVälj bok att returnera (nummer): ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > activeLoans.Count)
    {
        Console.WriteLine("Ogiltigt val.");
        return;
    }

    var loanToReturn = activeLoans[choice - 1];
    library.ReturnBook(loanToReturn);

    Console.WriteLine($"✅ Boken \"{loanToReturn.Book.Title}\" har returnerats.");

    var lateFee = loanToReturn.CalculateLateFee(DateTime.Now);
    if (lateFee > 0)
    {
        Console.WriteLine($"⚠️ Förseningsavgift: ${lateFee}");
    }
}

void ShowMembers(MemberRegistry registry)
{
    Console.WriteLine("\n=== Medlemmar ===");
    var allMembers = registry.GetAllMembers();

    foreach (var member in allMembers)
    {
        Console.WriteLine(member.GetInfo());
    }
}

void ShowStatistics(Library library)
{
    Console.WriteLine(library.GetLibraryStatistics());
}