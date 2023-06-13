using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.Services;
using University.WPF.Views;

namespace University.WPF.ViewModels;

public partial class CourseViewModel : UnitedEntityViewModel
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;

    private readonly Course _course;

    public CourseViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        Course course
        ) : base(course.Id, course.Name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _teacherService = teacherService;
        _dialogService = dialogService;
        _course = course;

        Description = course.Description!;
        
        LoadGroupsByCourseAsync().GetAwaiter();
    }

    [ObservableProperty] 
    private string description;

    [ObservableProperty]
    private GroupViewModel? selectedGroupViewModel;

    [ObservableProperty]
    private ObservableCollection<GroupViewModel> groupsByCourseViews = new(new List<GroupViewModel>());

    [RelayCommand]
    private void OpenCreateGroupWindow()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Group",
            Height = 250,
            Width = 400
        };
        
        var group = (GroupViewModel) _dialogService.ShowDialog(
            new CreateGroupView(), 
            new CreateGroupViewModel(_courseService, _groupService, _teacherService),
            dialogConfiguration)!;

        LoadGroupsByCourseAsync().GetAwaiter();
    }
    
    
    [RelayCommand]
    private void OpenEditGroupWindow()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Group",
            Height = 250,
            Width = 400
        };
        
        var group = (GroupViewModel) _dialogService.ShowDialog(
            new EditGroupView(), 
            new EditGroupViewModel(_groupService),
            dialogConfiguration, SelectedGroupViewModel!)!;
        
        LoadGroupsByCourseAsync().GetAwaiter();
    }

    [RelayCommand]
    private void DeleteGroup()
    {
        
    }

    private async Task LoadGroupsByCourseAsync()
    {
        var groups = await _courseService.GetCourseGroupsAsync(_course.Id);
        var viewModels = groups.Select(group => new GroupViewModel(_groupService, group)).ToList();

        GroupsByCourseViews = new ObservableCollection<GroupViewModel>(viewModels);
    }
}