using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Interface;


public interface ISearchable
{
    bool Matches(string searchTerm);
}
