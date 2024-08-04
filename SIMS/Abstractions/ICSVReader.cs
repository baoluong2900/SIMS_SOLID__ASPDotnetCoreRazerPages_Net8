using System.Collections.Generic;
namespace SIMS.Abstractions
{
    public interface ICSVReader
    {
        IEnumerable<T> ReadCSV<T>(string filePath) where T : class;
    }
}
