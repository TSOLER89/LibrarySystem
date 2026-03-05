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


    // Hämta alla medlemmar med deras lån
    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _db.Members
            .Include(m => m.Loans)
            .ToListAsync();
    }

    // Hämta en medlem med deras lån och de lånade böckerna
    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _db.Members
            .Include(m => m.Loans)
                .ThenInclude(l => l.Book)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    // För att lägga till en ny medlem, inte lån
    public async Task AddAsync(Member member)
    {
        _db.Members.Add(member);
        await _db.SaveChangesAsync();
    }

    // För uppdatering av medlemsinformation, inte lån
    public async Task UpdateAsync(Member member)
    {
        _db.Members.Update(member);
        await _db.SaveChangesAsync();
    }

}