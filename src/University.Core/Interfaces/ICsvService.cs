using CsvHelper.Configuration;
using University.Core.Models;

namespace University.Core.Interfaces;

public interface ICsvService
{
    public void Save<T, TMap>(string filePath, IEnumerable<T> list) where T : class where TMap : ClassMap;

    public IEnumerable<T?> Load<T, TMap>(string filePath) where T : class where TMap : ClassMap;
}