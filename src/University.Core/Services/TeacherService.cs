using University.Core.Interfaces;
using University.Core.Models;

namespace University.Core.Services;

public class TeacherService : ITeacherService<Teacher>
{
    private readonly ITeacherRepository<Teacher> _teacherRepository;

    public TeacherService(ITeacherRepository<Teacher> teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _teacherRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        await _teacherRepository.UpdateAsync(teacher);
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _teacherRepository.AddAsync(teacher);
    }

    public async Task DeleteAsync(Teacher teacher)
    {
        await _teacherRepository.DeleteAsync(teacher);
    }
}