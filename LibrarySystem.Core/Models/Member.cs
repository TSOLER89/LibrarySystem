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
        public List <Loan> BorrowedBooks { get; }

        public Member(int id, string name, string email, DateTime memberSince)
        {
            Id = id;
            Name = name;
            Email = email;
            MemberSince = memberSince;
            BorrowedBooks = new List<Loan>();
        }
    }
}
