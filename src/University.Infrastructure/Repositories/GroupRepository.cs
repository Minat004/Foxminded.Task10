using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class GroupRepository : IGroupRepository<Group>
{
    private readonly UniversityDbContext _context;

    public GroupRepository(UniversityDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        var groups = await _context.Groups
            .Include(x => x.Course)
            .Include(x => x.Teacher)
            .ToListAsync();

        return groups;
    }

    public async Task<IEnumerable<Student>> GetGroupStudentsAsync(int groupId)
    {
        return await _context.Students.Where(x => x.GroupId == groupId).ToListAsync();
    }

    public async Task<Teacher> GetTeacherOrDefaultAsync(int teacherId)
    {
        return (await _context.Teachers.Where(x => x.Id == teacherId).FirstOrDefaultAsync())!;
    }

    public async Task UpdateAsync(Group item)
    {
        var newGroup = _context.Groups.FirstOrDefault(x => x.Id == item.Id);

        if (newGroup is null)
        {
            throw new NullReferenceException();
        }
        
        newGroup.Name = item.Name;
        
        _context.Update(newGroup);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Group group)
    {
        _context.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Group group)
    {
        _context.Remove(group);
        await _context.SaveChangesAsync();
    }
}