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

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task AddAsync(Member member)
    {
        await _repo.AddAsync(member);
    }
}