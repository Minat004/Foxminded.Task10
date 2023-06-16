using CsvHelper.Configuration;

namespace University.Core.Models.Mapping;

public sealed class StudentMapCsvSave : ClassMap<Student>
{
    public StudentMapCsvSave()
    {
        Map(x => x.Id).Index(0);
        Map(x => x.GroupId).Index(0);
        Map(x => x.FirstName).Index(1);
        Map(x => x.LastName).Index(2);
    }
}