using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class StudentViewModel : UnitedEntityViewModel
{
    private readonly IStudentService<Student> _studentService;

    public StudentViewModel(
        IStudentService<Student> studentService,
        Student student)
        : base(student.Id, student.FirstName)
    {
        _studentService = studentService;
        
        Student = student;

        Students = new NotifyTask<ObservableCollection<Student>>(GetStudentsAsync());
    }

    [ObservableProperty]
    private Student student;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Student>> students;
    
    private async Task<ObservableCollection<Student>> GetStudentsAsync()
    {
        var students = await _studentService.GetAllAsync().ConfigureAwait(false);
        
        var observeStudents = new ObservableCollection<Student>(students);

        return observeStudents;
    }
}