using Microsoft.EntityFrameworkCore;
using University.Core.Models;

namespace University.Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course() { Id = 1, Name = "Space Engineering", Description = "Department of Space Engineering" },
            new Course() { Id = 2, Name = "Aircraft", Description = "Department of Aircraft Control Systems" },
            new Course() { Id = 3, Name = "Chemical Engineering", Description = "Faculty of Chemical Engineering" },
            new Course() { Id = 4, Name = "Machine Design", Description = "Department of Machine Design" },
            new Course() { Id = 5, Name = "Engineering Department", Description = "Electronic Engineering Department" });

        modelBuilder.Entity<Group>().HasData(
            new Group() { Id = 1, Name = "SE-01", CourseId = 1, TeacherId = 1 },
            new Group() { Id = 2, Name = "SE-02", CourseId = 1, TeacherId = 2 },
            new Group() { Id = 3, Name = "SE-03", CourseId = 1, TeacherId = 3 },
            new Group() { Id = 4, Name = "AC-01", CourseId = 2, TeacherId = 4 },
            new Group() { Id = 5, Name = "AC-02", CourseId = 2, TeacherId = 5 },
            new Group() { Id = 6, Name = "AC-03", CourseId = 2, TeacherId = 6 },
            new Group() { Id = 7, Name = "CE-01", CourseId = 3, TeacherId = 7 },
            new Group() { Id = 8, Name = "CE-02", CourseId = 3, TeacherId = 8 },
            new Group() { Id = 9, Name = "CE-03", CourseId = 3, TeacherId = 9 },
            new Group() { Id = 10, Name = "SR-01", CourseId = 5, TeacherId = 10 },
            new Group() { Id = 11, Name = "SR-02", CourseId = 5, TeacherId = 11 },
            new Group() { Id = 12, Name = "SR-03", CourseId = 5, TeacherId = 12 },
            new Group() { Id = 13, Name = "DC-01", CourseId = 4, TeacherId = 13 },
            new Group() { Id = 14, Name = "DC-02", CourseId = 4, TeacherId = 14 },
            new Group() { Id = 15, Name = "DC-03", CourseId = 4, TeacherId = 15 });

        modelBuilder.Entity<Student>().HasData(
            new Student() { Id = 1, FirstName = "Tony", LastName = "Stark", GroupId = 1 },
            new Student() { Id = 2, FirstName = "Hank", LastName = "Pym", GroupId = 1 },
            new Student() { Id = 3, FirstName = "Janet", LastName = "Pym", GroupId = 1 },
            new Student() { Id = 4, FirstName = "Bruce", LastName = "Banner", GroupId = 1 },
            new Student() { Id = 5, FirstName = "Thor", LastName = "Odinson", GroupId = 1 },
            new Student() { Id = 6, FirstName = "Rick", LastName = "Jones", GroupId = 1 },
            new Student() { Id = 7, FirstName = "Steven", LastName = "Rogers", GroupId = 2 },
            new Student() { Id = 8, FirstName = "Francis", LastName = "Barton", GroupId = 2 },
            new Student() { Id = 9, FirstName = "Pietro", LastName = "Maximoff", GroupId = 2 },
            new Student() { Id = 10, FirstName = "Wanda", LastName = "Maximoff", GroupId = 2 },
            new Student() { Id = 11, FirstName = "Harry", LastName = "Walters", GroupId = 2 },
            new Student() { Id = 12, FirstName = "Cleese", LastName = "Rambeau", GroupId = 2 },
            new Student() { Id = 13, FirstName = "Victor", LastName = "Shade", GroupId = 2 },
            new Student() { Id = 14, FirstName = "Dane", LastName = "Whitman", GroupId = 2 },
            new Student() { Id = 15, FirstName = "Natasha", LastName = "Romanoff", GroupId = 3 },
            new Student() { Id = 16, FirstName = "Henry", LastName = "McCoy", GroupId = 3 },
            new Student() { Id = 17, FirstName = "Marc", LastName = "Spector", GroupId = 3 },
            new Student() { Id = 18, FirstName = "Heather", LastName = "Douglas", GroupId = 3 },
            new Student() { Id = 19, FirstName = "Patsy", LastName = "Walker", GroupId = 3 },
            new Student() { Id = 20, FirstName = "Matthew", LastName = "Liebowitz", GroupId = 3 },
            new Student() { Id = 21, FirstName = "Patsy", LastName = "Walker", GroupId = 3 },
            new Student() { Id = 22, FirstName = "Simon", LastName = "Williams", GroupId = 3 },
            new Student() { Id = 23, FirstName = "Aleta", LastName = "Ogord", GroupId = 4 },
            new Student() { Id = 24, FirstName = "Martinex", LastName = "T'Naga", GroupId = 4 },
            new Student() { Id = 25, FirstName = "Nicholette", LastName = "Gold", GroupId = 4 },
            new Student() { Id = 26, FirstName = "Vance", LastName = "Astrovik", GroupId = 4 },
            new Student() { Id = 27, FirstName = "Yondu", LastName = "Udonta", GroupId = 5 },
            new Student() { Id = 28, FirstName = "Mar", LastName = "Vell", GroupId = 5 },
            new Student() { Id = 29, FirstName = "Carol", LastName = "Danvers", GroupId = 5 },
            new Student() { Id = 30, FirstName = "Samuel", LastName = "Wilson", GroupId = 5 },
            new Student() { Id = 31, FirstName = "Jennifer", LastName = "Walters", GroupId = 13 },
            new Student() { Id = 32, FirstName = "Monica", LastName = "Rambeau", GroupId = 13 },
            new Student() { Id = 33, FirstName = "James", LastName = "Rhodes", GroupId = 13 },
            new Student() { Id = 34, FirstName = "Barbara", LastName = "Barton", GroupId = 13 },
            new Student() { Id = 35, FirstName = "Moira", LastName = "Brandon", GroupId = 6 },
            new Student() { Id = 36, FirstName = "Bonita", LastName = "Juarez", GroupId = 6 },
            new Student() { Id = 37, FirstName = "Marc", LastName = "Spector", GroupId = 6 },
            new Student() { Id = 38, FirstName = "John", LastName = "Walker", GroupId = 6 },
            new Student() { Id = 39, FirstName = "Jim", LastName = "Hammond", GroupId = 10 },
            new Student() { Id = 40, FirstName = "Miguel", LastName = "Santos", GroupId = 10 },
            new Student() { Id = 41, FirstName = "Julia", LastName = "Carpenter", GroupId = 10 },
            new Student() { Id = 42, FirstName = "Christopher", LastName = "Powell", GroupId = 10 });

        modelBuilder.Entity<Teacher>().HasData(
            new Teacher() { Id = 1, FirstName = "Alain", LastName = "Aspect" },
            new Teacher() { Id = 2, FirstName = "John", LastName = "Clauser" },
            new Teacher() { Id = 3, FirstName = "Anton", LastName = "Zeilinger" },
            new Teacher() { Id = 4, FirstName = "Syukuro", LastName = "Manabe" },
            new Teacher() { Id = 5, FirstName = "Klaus", LastName = "Hasselmann" },
            new Teacher() { Id = 6, FirstName = "Giorgio", LastName = "Parisi" },
            new Teacher() { Id = 7, FirstName = "Roger", LastName = "Penrose" },
            new Teacher() { Id = 8, FirstName = "Reinhard", LastName = "Genzel" },
            new Teacher() { Id = 9, FirstName = "Andrea", LastName = "Ghez" },
            new Teacher() { Id = 10, FirstName = "James", LastName = "Peebles" },
            new Teacher() { Id = 11, FirstName = "Michel", LastName = "Mayor" },
            new Teacher() { Id = 12, FirstName = "Didier", LastName = "Queloz" },
            new Teacher() { Id = 13, FirstName = "Arthur", LastName = "Ashkin" },
            new Teacher() { Id = 14, FirstName = "Gerard", LastName = "Mourou" },
            new Teacher() { Id = 15, FirstName = "Donna", LastName = "Strickland" });
    }
}