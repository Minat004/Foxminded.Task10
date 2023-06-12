using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class StudentViewModel : UnitedEntityViewModel
{
    public StudentViewModel(Student student) : base(student.Id, $"{student.FirstName} {student.LastName}")
    {
        GroupName = student.Group!.Name;
    }

    [ObservableProperty] 
    private string groupName;
}