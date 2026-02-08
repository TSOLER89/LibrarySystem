using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    public class Loan

    {
        public Book Book { get; }
        public Member Member { get; }
        public DateTime LoanDate { get; }

        public Loan(Book book, Member member, DateTime loanDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
        }

        public bool IsOverdue()
        {
            return (DateTime.Now - LoanDate).Days > 14;
        }
    }
}