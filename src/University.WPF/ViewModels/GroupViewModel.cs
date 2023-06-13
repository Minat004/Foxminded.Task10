using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class GroupViewModel : UnitedEntityViewModel
{
    private readonly IGroupService<Group> _groupService;
    private readonly Group _group;

    public GroupViewModel(IGroupService<Group> groupService, Group group) : base(group.Id, group.Name)
    {
        _groupService = groupService;
        _group = group;

        CourseName = group.Course!.Name;
        TeacherFullName = $"{group.Teacher!.FirstName} {group.Teacher.LastName}";

        LoadGroupsByCourseAsync().GetAwaiter();
    }

    [ObservableProperty] 
    private string? courseName;
    
    [ObservableProperty] 
    private string teacherFullName;
    
    [ObservableProperty]
    private ObservableCollection<StudentViewModel> studentsByGroupViews = new(new List<StudentViewModel>());
    
    private async Task LoadGroupsByCourseAsync()
    {
        var students = await _groupService.GetGroupStudentsAsync(_group.Id);
        var viewModels = students.Select(student => new StudentViewModel(student));

        StudentsByGroupViews = new ObservableCollection<StudentViewModel>(viewModels);
    }
}