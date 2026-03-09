using LibrarySystem.Core.Models;
using LibrarySystem.Data.Repository;

namespace LibrarySystem.Web.Services;

public class MemberService
{
    private readonly IMemberRepository _repo;

    public MemberService(IMemberRepository repo)
    {
        _repo = repo;
    }

    // Hämta alla medlemmar med deras lån
    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    // Hämta en medlem med deras lån och de lånade böckerna
    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    // För att lägga till en ny medlem, inte lån
    public async Task AddAsync(Member member)
    {
        await _repo.AddAsync(member);
    }

    // För uppdatering av medlemsinformation, inte lån
    public async Task UpdateAsync(Member member)
    {
        await _repo.UpdateAsync(member);
    }
}