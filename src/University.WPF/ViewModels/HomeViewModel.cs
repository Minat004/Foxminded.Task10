using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class HomeViewModel : UnitedEntityViewModel
{
    private readonly IWindowService _windowService;
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;

    public HomeViewModel(
        IWindowService windowService,
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        int id, string name) : base(id, name)
    {
        _windowService = windowService;
        _courseService = courseService;
        _groupService = groupService;

        LoadCourseViewModelsAsync().GetAwaiter();
    }
    
    [ObservableProperty]
    private ObservableCollection<CourseViewModel> courseViewModels = new(new List<CourseViewModel>());
    
    private async Task LoadCourseViewModelsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var viewModels = courses.Select(course => 
            new CourseViewModel(_windowService, _courseService, _groupService, course));
        
        CourseViewModels = new ObservableCollection<CourseViewModel>(viewModels);
    }
}