using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository<Teacher>
{
    private readonly IDbContextFactory<UniversityDbContext> _dbContextFactory;

    public TeacherRepository(IDbContextFactory<UniversityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        return await context.Teachers.Include(x => x.Group).ToListAsync();
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var newTeacher = context.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

        if (newTeacher is null)
        {
            throw new NullReferenceException();
        }
        
        newTeacher.FirstName = teacher.FirstName;
        newTeacher.LastName = teacher.LastName;
        
        context.Update(newTeacher);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(Teacher teacher)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Add(teacher);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Teacher teacher)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Remove(teacher);
        await context.SaveChangesAsync();
    }
}