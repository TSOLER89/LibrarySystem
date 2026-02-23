using LibrarySystem.Core;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using LibrarySystem.Core.Interface;
using System;
using System.Collections.Generic;

try
{
    Console.WriteLine("=== Welcome to Library System ===\n");

    // Skapa böcker
    var books = new List<Book>
    {
        new Book("978-0451524935", "1984", "George Orwell", 1949),
        new Book("978-0547928227", "The Hobbit", "J.R.R. Tolkien", 1937),
        new Book("978-0060850524", "Brave New World", "Aldous Huxley", 1932)
    };

    // Skapa medlemmar
    var members = new List<Member>
    {
        new Member(1, "Alice Johnson", "alice@example.com", DateTime.Now.AddYears(-2)),
        new Member(2, "Bob Smith", "bob@example.com", DateTime.Now.AddYears(-1)),
        new Member(3, "Carol Williams", "carol@example.com", DateTime.Now.AddMonths(-6))
    };

    // Skapa service-objekt
    var bookCatalog = new BookCatalog(books);
    var memberRegistry = new MemberRegistry(members);
    var loanManager = new LoanManager();

    // Skapa biblioteket med komposition
    var library = new Library(bookCatalog, memberRegistry, loanManager);

    // Visa initial statistik
    Console.WriteLine(library.GetLibraryStatistics());

    // --- Simulera låneaktiviteter ---
    Console.WriteLine("\n=== Borrowing Books ===");

    // Alice lånar 1984
    try
    {
        var loan1 = library.BorrowBook("978-0451524935", 1);
        Console.WriteLine($"✓ Alice borrowed '1984' (Due: {loan1.DueDate:yyyy-MM-dd})");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error: {ex.Message}");
    }

    // Bob lånar The Hobbit
    try
    {
        var loan2 = library.BorrowBook("978-0547928227", 2);
        Console.WriteLine($"✓ Bob borrowed 'The Hobbit' (Due: {loan2.DueDate:yyyy-MM-dd})");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error: {ex.Message}");
    }

    // Försök låna samma bok igen (ska misslyckas)
    try
    {
        var loan3 = library.BorrowBook("978-0451524935", 3);
        Console.WriteLine($"✓ Carol borrowed '1984'");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"✗ Expected error: {ex.Message}");
    }

    // Visa uppdaterad statistik
    Console.WriteLine(library.GetLibraryStatistics());

    // --- Sök efter böcker ---
    Console.WriteLine("\n=== Searching for Books ===");
    var searchResults = library.SearchBooksByTitle("Hobbit");
    Console.WriteLine($"Found {searchResults.Count} book(s) matching 'Hobbit':");
    foreach (var book in searchResults)
    {
        Console.WriteLine($"  - {book.Title} by {book.Author}");
    }

    // --- Visa medlemmar med försenade böcker ---
    Console.WriteLine("\n=== Checking Overdue Books ===");
    var overdueLoans = library.GetOverdueLoans();
    if (overdueLoans.Any())
    {
        Console.WriteLine($"Found {overdueLoans.Count} overdue loan(s):");
        foreach (var loan in overdueLoans)
        {
            var fee = loan.CalculateLateFee(DateTime.Now);
            Console.WriteLine($"  - {loan.Member.Name} has '{loan.Book.Title}' overdue. Fee: ${fee}");
        }
    }
    else
    {
        Console.WriteLine("No overdue loans at this time.");
    }

    // --- Returnera en bok ---
    Console.WriteLine("\n=== Returning Books ===");
    try
    {
        var aliceLoans = library.FindMember(1).GetActiveLoans();
        if (aliceLoans.Any())
        {
            library.ReturnBook(aliceLoans.First());
            Console.WriteLine("✓ Alice returned '1984'");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error: {ex.Message}");
    }

    // Slutlig statistik
    Console.WriteLine(library.GetLibraryStatistics());

    Console.WriteLine("\n=== Program completed successfully ===");
}
catch (Exception ex)
{
    Console.WriteLine($"\n!!! FATAL ERROR !!!");
    Console.WriteLine($"Type: {ex.GetType().Name}");
    Console.WriteLine($"Message: {ex.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
}
finally
{
    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
}