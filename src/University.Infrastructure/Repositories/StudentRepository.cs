using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository<Student>
{
    private readonly IDbContextFactory<UniversityDbContext> _dbContextFactory;

    public StudentRepository(IDbContextFactory<UniversityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var students = await context.Students.Include(x => x.Group).ToListAsync();
        
        return students;
    }

    public async Task UpdateAsync(Student item)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var newStudent = context.Students.FirstOrDefault(x => x.Id == item.Id);

        if (newStudent is null)
        {
            throw new NullReferenceException();
        }
        
        newStudent.FirstName = item.FirstName;
        newStudent.LastName = item.LastName;
        
        context.Update(newStudent);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(Student student)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Remove(student);
        await context.SaveChangesAsync();
    }
}