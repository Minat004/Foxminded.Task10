using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using University.Core.Interfaces;

namespace University.Core.Services;

public class CsvService : ICsvService
{
    private readonly CsvConfiguration _csvConfig;

    public CsvService()
    {
        _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            Encoding = Encoding.UTF8
        };
    }

    public void Save<T, TMap>(string filePath, IEnumerable<T> list) where T: class where TMap : ClassMap
    {
        using (var writer = new StreamWriter(filePath))
        using (var csvWriter = new CsvWriter(writer, _csvConfig))
        {
            csvWriter.Context.RegisterClassMap<TMap>();
            csvWriter.WriteHeader<T>();
            csvWriter.NextRecord();
            csvWriter.WriteRecords(list);
        }
    }

    public IEnumerable<T?> Load<T, TMap>(string filePath) where T: class where TMap : ClassMap
    {
        using(var reader = new StreamReader(filePath))
        using (var csvReader = new CsvReader(reader, _csvConfig))
        {
            csvReader.Context.RegisterClassMap<TMap>();

            while (csvReader.Read())
            {
                yield return csvReader.GetRecord<T>();
            }
        }
    }
}