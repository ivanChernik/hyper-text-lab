using System.Collections.Generic;

namespace AsketHypertext.Services
{
    public interface ISourcesLoader
    {
        IEnumerable<string[]> Load();
    }
}
