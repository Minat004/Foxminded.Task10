using University.Core.Interfaces;
using University.Core.Models;

namespace University.Core.Services;

public class CourseService : ICourseService<Course>
{
    private readonly ICourseRepository<Course> _courseRepository;

    public CourseService(ICourseRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Group>> GetCourseGroupsAsync(int courseId)
    {
        return await _courseRepository.GetCourseGroupsAsync(courseId);
    }
}