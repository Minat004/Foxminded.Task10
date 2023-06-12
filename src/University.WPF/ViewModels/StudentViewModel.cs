using University.Core.Models;

namespace University.WPF.ViewModels;

public class StudentViewModel : UnitedEntityViewModel
{
    public StudentViewModel(Student student) : base(student.Id, $"{student.FirstName} {student.LastName}")
    {
    }
}