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
        public DateTime DueDate { get; }
        public DateTime? ReturnDate { get; private set; }
        public bool IsReturned => ReturnDate.HasValue;


        // Constructor to initialize a loan with a book, member, and loan date
        public Loan(Book book, Member member, DateTime loanDate)
        {
            Book = book;
            Member = member;
            LoanDate = loanDate;
            DueDate = loanDate.AddDays(14); // Set due date to 14 days after loan date
            ReturnDate = null;
        }

        // A loan is considered overdue if it's been more than 14 days since the loan date
        public bool IsOverdue()
        {
            if (IsRetuned)
                return false;
            return DateTime.Now > DueDate;
        }

        // Simple late fee calculation: $5 per day after 14 days
        public int CalculateLateFee(DateTime today)
        {
            // Define the loan period and fee per day
            const int feePerDay = 5;

            // Calculate the number of overdue days
            var checkDate = IsReturned ? ReturnDate.Value : today;
            var overdueDays = (checkDate - DueDate).Days;

            // If there are overdue days, calculate the fee; otherwise, return 0
            return overdueDays > 0 ? overdueDays * feePerDay : 0;
        }

        // Method to mark the loan as returned
        public void MarkAsReturned(DateTime returnDate)
        {
            ReturnDate = returnDate;
        }
    }
}