using University.Core.Interfaces;
using University.Core.Models;

namespace University.Core.Services;

public class StudentService : IStudentService<Student>
{
    private readonly IStudentRepository<Student> _studentRepository;

    public StudentService(IStudentRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Student item)
    {
        await _studentRepository.UpdateAsync(item);
    }

    public async Task AddAsync(Student student)
    {
        await _studentRepository.AddAsync(student);
    }

    public async Task DeleteAsync(Student student)
    {
        await _studentRepository.DeleteAsync(student);
    }
}