using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    // Represents a member of the library
    public class Member
    {
        public int Id { get; set; }                 // EF vill kunna sätta
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime MemberSince { get; set; }

        // Navigation
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        // EF behöver parameterlös ctor
        public Member() { }

        // Din "vanliga" ctor kan finnas kvar
        public Member(int id, string name, string email, DateTime memberSince)
        {
            Id = id;
            Name = name;
            Email = email;
            MemberSince = memberSince;
        }

        // Hjälpmetoder kan du behålla, men basera på Loans
        public List<Loan> GetActiveLoans() => Loans.Where(l => !l.IsReturned).ToList();
    }
}
