using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core.Models
{
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
