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


        // Constructor to initialize a loan with a book, member, and loan date
        public Loan(Book book, Member member, DateTime loanDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
        }

        // A loan is considered overdue if it's been more than 14 days since the loan date
        public bool IsOverdue()
        {
            return (DateTime.Now - LoanDate).Days > 14;
        }

        // Simple late fee calculation: $5 per day after 14 days
        public int CalculateLateFee(DateTime today)
        {
            // Define the loan period and fee per day
            const int loanPeriodDays = 14;
            const int feePerDay = 5;

            // Calculate the number of overdue days
            var overdueDays = (today - LoanDate).Days - loanPeriodDays;

            // If there are overdue days, calculate the fee; otherwise, return 0
            return overdueDays > 0 ? overdueDays * feePerDay : 0;
        }
    }
}