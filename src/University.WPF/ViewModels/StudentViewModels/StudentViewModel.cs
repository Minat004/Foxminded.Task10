using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class StudentViewModel : UnitedEntityViewModel
{
    private readonly Student _student;

    public StudentViewModel(Student student) : base(student.Id, $"{student.FirstName} {student.LastName}")
    {
        _student = student;
        GroupName = student.Group!.Name;
    }

    [ObservableProperty] 
    private string groupName;

    public Student GetStudent()
    {
        return _student;
    }
}