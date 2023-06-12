using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.GenericCollections;

namespace University.WPF.ViewModels;

public partial class CourseViewModel : UnitedEntityViewModel
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly Course _course;

    public CourseViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        Course course) : base(course.Id, course.Name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _course = course;

        Description = course.Description!;
        
        LoadGroupsByCourseAsync().GetAwaiter();
    }

    [ObservableProperty] 
    private string description;

    [ObservableProperty]
    private ObservableCollection<GroupViewModel> groupsByCourseViews = new(new List<GroupViewModel>());
    
    private async Task LoadGroupsByCourseAsync()
    {
        var groups = await _courseService.GetCourseGroupsAsync(_course.Id);
        var viewModels = groups.Select(group => new GroupViewModel(_groupService, group)).ToList();

        GroupsByCourseViews = new ObservableCollection<GroupViewModel>(viewModels);
    }
}