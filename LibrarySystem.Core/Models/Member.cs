using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    // Represents a member of the library
    public class Member
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime MemberSince { get; }
        public List<Loan> BorrowedBooks { get; }

        public Member(int id, string name, string email, DateTime memberSince)
        {
            Id = id;
            Name = name;
            Email = email;
            MemberSince = memberSince;
            BorrowedBooks = new List<Loan>();
        }

        // Metod för att lägga till ett lån till medlemmens lista
        public void AddLoan(Loan loan)
        {
            BorrowedBooks.Add(loan);
        }

        // Metod för att få alla aktiva (ej returnerade) lån
        public List<Loan> GetActiveLoans()
        {
            return BorrowedBooks.Where(l => !l.IsReturned).ToList();
        }

        // Metod för att kontrollera om medlemmen har försenade böcker
        public bool HasOverdueBooks()
        {
            return BorrowedBooks.Any(l => l.IsOverdue());
        }

        // Metod för att få information om medlemmen
        public string GetInfo()
        {
            var activeLoans = GetActiveLoans().Count;
            return $"Member: {Name} (ID: {Id})\nEmail: {Email}\nMember since: {MemberSince:yyyy-MM-dd}\nActive loans: {activeLoans}";
        }
    }
}