using Microsoft.EntityFrameworkCore;
using University.Core.Interfaces;
using University.Core.Models;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository<Course>
{
    private readonly IDbContextFactory<UniversityDbContext> _dbContextFactory;

    public CourseRepository(IDbContextFactory<UniversityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Group>> GetCourseGroupsAsync(int courseId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Groups
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Where(x => x.CourseId == courseId).ToListAsync();
    }
}