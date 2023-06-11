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
    private readonly Course _course;

    public CourseViewModel(ICourseService<Course> courseService, Course course) : base(course.Id, course.Name)
    {
        _courseService = courseService;
        _course = course;
        LoadGroupsByCourseAsync().GetAwaiter();
    }

    [ObservableProperty]
    private ObservableCollection<GroupViewModel> groupsByCourseViews = new(new List<GroupViewModel>());
    
    private async Task LoadGroupsByCourseAsync()
    {
        var courses = await _courseService.GetCourseGroupsAsync(_course.Id);
        var viewModels = courses.Select(group => new GroupViewModel(group)).ToList();

        GroupsByCourseViews = new ObservableCollection<GroupViewModel>(viewModels);
    }
}