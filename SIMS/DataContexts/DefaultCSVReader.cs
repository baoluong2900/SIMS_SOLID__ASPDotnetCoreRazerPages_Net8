using SIMS.Abstractions;
using System.Globalization;
using CsvHelper;
public class DefaultCSVReader : ICSVReader
{
    // Implement methods as needed
    public IEnumerable<T> ReadCSV<T>(string filePath) where T : class
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<T>().ToList();
    }
}
