using System;

namespace LibrarySystem.Core.Models
{
    public class Loan
    {
        public int Id { get; set; }

        // Foreign keys
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;

        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        public Loan() { } // EF

        public Loan(Book book, Member member, DateTime loanDate)
        {
            Book = book;
            BookId = book.Id;       // (valfritt men bra)
            Member = member;
            MemberId = member.Id;   // (valfritt men bra)
            LoanDate = loanDate;
            DueDate = loanDate.AddDays(14);
        }
        
        public bool IsOverdue() => IsOverdue(DateTime.Today);

        public bool IsOverdue(DateTime today) => !IsReturned && today > DueDate;

        public void MarkAsReturned(DateTime returnDate)
        {
            ReturnDate = returnDate;
        }

        public int CalculateLateFee(DateTime today)
        {
            const int feePerDay = 5;
            var checkDate = IsReturned ? ReturnDate!.Value : today;
            var overdueDays = (checkDate - DueDate).Days;
            return overdueDays > 0 ? overdueDays * feePerDay : 0;
        }
    }
}