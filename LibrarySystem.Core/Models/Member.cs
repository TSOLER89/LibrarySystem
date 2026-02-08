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
        public Member(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
