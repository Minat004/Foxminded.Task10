using CsvHelper.Configuration;

namespace University.Core.Models.Mapping;

public sealed class StudentMapCsvLoad : ClassMap<Student>
{
    public StudentMapCsvLoad()
    {
        Map(x => x.GroupId).Index(0);
        Map(x => x.FirstName).Index(1);
        Map(x => x.LastName).Index(2);
    }
}