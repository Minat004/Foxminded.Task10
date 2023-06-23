using Microsoft.Extensions.Configuration;
using University.Core.Models;
using University.WPF.ViewModels.GroupViewModels;

namespace University.WPF.Tests;

public static class MockDataHelper
{
    internal static IEnumerable<Course> GetCourses()
    {
        var courses = new List<Course>
        {
            new()
            {
                Id = 1,
                Name = "Space Engineering",
                Description = "Department of Space Engineering"
            },
            new()
            {
                Id = 2,
                Name = "Aircraft",
                Description = "Department of Aircraft Control Systems"
            },
            new()
            {
                Id = 3,
                Name = "Chemical Engineering",
                Description = "Faculty of Chemical Engineering"
            },
            new()
            {
                Id = 4,
                Name = "Machine Design",
                Description = "Department of Machine Design"
            },
            new()
            {
                Id = 5,
                Name = "Space Engineering",
                Description = "Electronic Engineering Department"
            }
        };

        return courses;
    }

    internal static IEnumerable<Group> GetGroups()
    {
        var groups = new List<Group>
        {
            new()
            {
                Id = 1,
                CourseId = 1,
                Name = "SE-01"
            },
            new()
            {
                Id = 2,
                CourseId = 1,
                Name = "SE-02"
            },
            new()
            {
                Id = 3,
                CourseId = 1,
                Name = "SE-03"
            },
            new()
            {
                Id = 4,
                CourseId = 2,
                Name = "AC-012"
            },
            new()
            {
                Id = 5,
                CourseId = 2,
                Name = "AC-02"
            },
            new()
            {
                Id = 6,
                CourseId = 2,
                Name = "AC-03"
            },
            new()
            {
                Id = 7,
                CourseId = 3,
                Name = "CE-01"
            },
            new()
            {
                Id = 8,
                CourseId = 3,
                Name = "CE-02"
            },
            new()
            {
                Id = 9,
                CourseId = 3,
                Name = "CE-03"
            },
            new()
            {
                Id = 10,
                CourseId = 5,
                Name = "SR-01"
            },
            new()
            {
                Id = 11,
                CourseId = 5,
                Name = "SR-02"
            },
            new()
            {
                Id = 12,
                CourseId = 5,
                Name = "SR-03"
            },
            new()
            {
                Id = 13,
                CourseId = 4,
                Name = "DC-01"
            },
            new()
            {
                Id = 14,
                CourseId = 4,
                Name = "DC-02"
            },
            new()
            {
                Id = 15,
                CourseId = 4,
                Name = "DC-03"
            }
        };

        return groups;
    }

    internal static IEnumerable<Group> GetGroupsOfCourseById(int courseId)
    {
        var groups = new List<Group>
        {
            new()
            {
                Id = 1,
                CourseId = 1,
                Name = "SE-01",
                Course = new Course
                {
                    Id = 1,
                    Name = "Space Engineering",
                    Description = "Department of Space Engineering"
                },
                Teacher = new Teacher()
                {
                    Id = 1,
                    FirstName = "Alain",
                    LastName = "Aspect"
                }
            },
            new()
            {
                Id = 2,
                CourseId = 1,
                Name = "SE-02"
            },
            new()
            {
                Id = 3,
                CourseId = 1,
                Name = "SE-03"
            }
        };

        return groups;

        // return GetGroups().Where(group => group.CourseId == courseId);
    }

    internal static IEnumerable<Student> GetStudents()
    {
        var students = new List<Student>
        {
            new()
            {
                Id = 1,
                GroupId = 1,
                FirstName = "Tony",
                LastName = "Stark"
            },
            new()
            {
                Id = 2,
                GroupId = 1,
                FirstName = "Hank",
                LastName = "Pym"
            },
            new()
            {
                Id = 3,
                GroupId = 1,
                FirstName = "Janet",
                LastName = "Pym"
            },
            new()
            {
                Id = 4,
                GroupId = 1,
                FirstName = "Bruce",
                LastName = "Banner"
            },
            new()
            {
                Id = 5,
                GroupId = 1,
                FirstName = "Thor",
                LastName = "Odinson"
            },
            new()
            {
                Id = 6,
                GroupId = 1,
                FirstName = "Rick",
                LastName = "Jones"
            },
            new()
            {
                Id = 7,
                GroupId = 2,
                FirstName = "Steven",
                LastName = "Rogers"
            },
            new()
            {
                Id = 8,
                GroupId = 2,
                FirstName = "Francis",
                LastName = "Barton"
            },
            new()
            {
                Id = 9,
                GroupId = 2,
                FirstName = "Pietro",
                LastName = "Maximoff"
            },
            new()
            {
                Id = 10,
                GroupId = 2,
                FirstName = "Wanda",
                LastName = "Maximoff"
            },
            new()
            {
                Id = 11,
                GroupId = 2,
                FirstName = "Harry",
                LastName = "Cleese"
            },
            new()
            {
                Id = 12,
                GroupId = 2,
                FirstName = "Victor",
                LastName = "Shade"
            },
            new()
            {
                Id = 13,
                GroupId = 2,
                FirstName = "Dane",
                LastName = "Whitman"
            },
            new()
            {
                Id = 14,
                GroupId = 3,
                FirstName = "Natasha",
                LastName = "Romanoff"
            },
            new()
            {
                Id = 15,
                GroupId = 3,
                FirstName = "Henry",
                LastName = "McCoy"
            },
            new()
            {
                Id = 16,
                GroupId = 3,
                FirstName = "Heather",
                LastName = "Douglas"
            },
            new()
            {
                Id = 17,
                GroupId = 3,
                FirstName = "Patsy",
                LastName = "Walker"
            },
            new()
            {
                Id = 18,
                GroupId = 3,
                FirstName = "Matthew",
                LastName = "Liebowitz"
            },
            new()
            {
                Id = 19,
                GroupId = 3,
                FirstName = "Patsy",
                LastName = "Walker"
            },
            new()
            {
                Id = 20,
                GroupId = 3,
                FirstName = "Simon",
                LastName = "Williams"
            },
            new()
            {
                Id = 21,
                GroupId = 4,
                FirstName = "Aleta",
                LastName = "Ogord"
            },
            new()
            {
                Id = 22,
                GroupId = 4,
                FirstName = "Martinex",
                LastName = "T'Naga"
            },
            new()
            {
                Id = 23,
                GroupId = 4,
                FirstName = "Nicholette",
                LastName = "Gold"
            },
            new()
            {
                Id = 24,
                GroupId = 4,
                FirstName = "Vance",
                LastName = "Astrovik"
            },
            new()
            {
                Id = 25,
                GroupId = 5,
                FirstName = "Yondu",
                LastName = "Udonta"
            },
            new()
            {
                Id = 26,
                GroupId = 5,
                FirstName = "Mar",
                LastName = "Vell"
            },
            new()
            {
                Id = 27,
                GroupId = 5,
                FirstName = "Carol",
                LastName = "Danvers"
            },
            new()
            {
                Id = 28,
                GroupId = 5,
                FirstName = "Samuel",
                LastName = "Wilson"
            },
            new()
            {
                Id = 29,
                GroupId = 13,
                FirstName = "Jennifer",
                LastName = "Walters"
            },
            new()
            {
                Id = 30,
                GroupId = 13,
                FirstName = "Monica",
                LastName = "Rambeau"
            },
            new()
            {
                Id = 31,
                GroupId = 13,
                FirstName = "James",
                LastName = "Rhodes"
            },
            new()
            {
                Id = 32,
                GroupId = 13,
                FirstName = "Barbara",
                LastName = "Barton"
            },
            new()
            {
                Id = 33,
                GroupId = 13,
                FirstName = "Moira",
                LastName = "Brandon"
            },
            new()
            {
                Id = 34,
                GroupId = 13,
                FirstName = "Bonita",
                LastName = "Juarez"
            },
            new()
            {
                Id = 35,
                GroupId = 13,
                FirstName = "Marc",
                LastName = "Spector"
            },
            new()
            {
                Id = 36,
                GroupId = 13,
                FirstName = "John",
                LastName = "Walker"
            },
            new()
            {
                Id = 37,
                GroupId = 13,
                FirstName = "Jim",
                LastName = "Hammond"
            },
            new()
            {
                Id = 38,
                GroupId = 13,
                FirstName = "Miguel",
                LastName = "Santos"
            },
            new()
            {
                Id = 39,
                GroupId = 13,
                FirstName = "Julia",
                LastName = "Carpenter"
            },
            new()
            {
                Id = 40,
                GroupId = 13,
                FirstName = "Christopher",
                LastName = "Powell"
            },
        };

        return students;
    }
    
    internal static IEnumerable<Student> GetStudentsOfGroupById(int groupId)
    {
        var students = new List<Student>
        {
            new()
            {
                Id = 1,
                GroupId = 1,
                FirstName = "Tony",
                LastName = "Stark"
            },
            new()
            {
                Id = 2,
                GroupId = 1,
                FirstName = "Hank",
                LastName = "Pym"
            },
            new()
            {
                Id = 3,
                GroupId = 1,
                FirstName = "Janet",
                LastName = "Pym"
            },
            new()
            {
                Id = 4,
                GroupId = 1,
                FirstName = "Bruce",
                LastName = "Banner"
            },
            new()
            {
                Id = 5,
                GroupId = 1,
                FirstName = "Thor",
                LastName = "Odinson"
            },
            new()
            {
                Id = 6,
                GroupId = 1,
                FirstName = "Rick",
                LastName = "Jones"
            }
        };
        
        return students;

        // return GetStudents().Where(x => x.GroupId == groupId);
    }
    
    internal static IConfiguration GetConfig()
    {
        var inMemory = new Dictionary<string, string>
        {
            { "CsvHelper:ImportFilePath", "import.csv" },
            { "CsvHelper:ExportFilePath", "export.csv" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory!)
            .Build();

        return configuration;
    }
}