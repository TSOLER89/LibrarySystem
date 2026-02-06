namespace LibrarySystem.Core.Interface;

public interface ISearchable
{
    bool Matches(string searchTerm);
}
