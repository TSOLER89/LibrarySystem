using System;
using System.Collections.Generic;

namespace LibrarySystem.Core.Interface;


public interface ISearchable
{
    bool Matches(string searchTerm);
}
