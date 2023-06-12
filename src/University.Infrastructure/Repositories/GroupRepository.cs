using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class GroupRepository : IGroupRepository<Group>
{
    private readonly IDbContextFactory<UniversityDbContext> _dbContextFactory;

    public GroupRepository(IDbContextFactory<UniversityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<IEnumerable<Group>> GetAllAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var groups = await context.Groups
            .Include(x => x.Course)
            .Include(x => x.Teacher)
            .ToListAsync();

        return groups;
    }

    public async Task<IEnumerable<Student>> GetGroupStudentsAsync(int groupId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Students.Where(x => x.GroupId == groupId).ToListAsync();
    }

    public async Task<Teacher> GetTeacherOrDefaultAsync(int teacherId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return (await context.Teachers.Where(x => x.Id == teacherId).FirstOrDefaultAsync())!;
    }

    public async Task UpdateAsync(Group item)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var newGroup = context.Groups.FirstOrDefault(x => x.Id == item.Id);

        if (newGroup is null)
        {
            throw new NullReferenceException();
        }
        
        newGroup.Name = item.Name;
        
        context.Update(newGroup);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(Group group)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Add(group);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Group group)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        context.Remove(group);
        await context.SaveChangesAsync();
    }
}