using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryContext _db;

    public MemberRepository(LibraryContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _db.Members
            .Include(m => m.Loans)
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _db.Members
            .Include(m => m.Loans)
                .ThenInclude(l => l.Book)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Member member)
    {
        _db.Members.Add(member);
        await _db.SaveChangesAsync();
    }
}