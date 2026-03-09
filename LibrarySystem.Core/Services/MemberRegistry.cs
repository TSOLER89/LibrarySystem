using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Services
{
    public class MemberRegistry
    {
        private readonly List<Member> _members;

        public MemberRegistry()
        {
            _members = new List<Member>();
        }

        public MemberRegistry(List<Member> members)
        {
            _members = members ?? new List<Member>();
        }

        // Lägg till en ny medlem
        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member cannot be null");

            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException($"Member with ID {member.Id} already exists");

            _members.Add(member);
        }

        // Hitta medlem via ID
        public Member FindMemberById(int id)
        {
            var member = _members.FirstOrDefault(m => m.Id == id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");

            return member;
        }

        // Sök medlemmar via namn eller email
        public List<Member> SearchMembers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return new List<Member>();

            searchTerm = searchTerm.ToLower();

            return _members
                .Where(m =>
                    m.Name.ToLower().Contains(searchTerm) ||
                    m.Email.ToLower().Contains(searchTerm))
                .ToList();
        }

        // Få alla medlemmar
        public List<Member> GetAllMembers()
        {
            return _members.ToList();
        }

        // Få medlemmar med försenade böcker
        public List<Member> GetMembersWithOverdueBooks()
        {
            // ✅ FIX: HasOverdueBooks kräver DateTime today
            return _members.Where(m => m.HasOverdueBooks(DateTime.Today)).ToList();
        }
    }
}