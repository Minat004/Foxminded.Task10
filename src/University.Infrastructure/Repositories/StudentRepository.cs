using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository<Student>
{
    private readonly UniversityDbContext _context;

    public StudentRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        var students = await _context.Students.Include(x => x.Group).ToListAsync();
        
        return students;
    }

    public async Task UpdateAsync(Student item)
    {
        var newStudent = _context.Students.FirstOrDefault(x => x.Id == item.Id);

        if (newStudent is null)
        {
            throw new NullReferenceException();
        }
        
        newStudent.FirstName = item.FirstName;
        newStudent.LastName = item.LastName;
        
        _context.Update(newStudent);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Student student)
    {
        _context.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Remove(student);
        await _context.SaveChangesAsync();
    }
}