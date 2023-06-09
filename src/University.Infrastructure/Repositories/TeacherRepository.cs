using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository<Teacher>
{
    private readonly UniversityDbContext _context;

    public TeacherRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.Include(x => x.Group).ToListAsync();
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        var newTeacher = _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

        if (newTeacher is null)
        {
            throw new NullReferenceException();
        }
        
        newTeacher.FirstName = teacher.FirstName;
        newTeacher.LastName = teacher.LastName;
        
        _context.Update(newTeacher);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Teacher teacher)
    {
        _context.Add(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Teacher teacher)
    {
        _context.Add(teacher);
        await _context.SaveChangesAsync();
    }
}