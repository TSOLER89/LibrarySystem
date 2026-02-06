using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests;

public class MemberTests
{
    [Fact]
    public void Member_Should_Have_Id_And_Name()
    {
        var member = new Member(1, "Alice");

        Assert.Equal(1, member.Id);
        Assert.Equal("Alice", member.Name);
    }
}
