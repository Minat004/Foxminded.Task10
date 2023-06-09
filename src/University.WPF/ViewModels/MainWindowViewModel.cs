using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.GenericCollections;

namespace University.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ICourseService<Course> _courseService;

    public MainWindowViewModel(ICourseService<Course> courseService)
    {
        _courseService = courseService;
        LoadCoursesViewAsync().GetAwaiter();
        LoadCourseViewModelsAsync().GetAwaiter();
    }

    [ObservableProperty]
    private ObservableCollectionView<Course> coursesView = new(new List<Course>());

    [ObservableProperty]
    private ObservableCollectionView<CourseViewModel> courseViewModels = new(new List<CourseViewModel>());

    private async Task LoadCoursesViewAsync()
    {
        var courses = await _courseService.GetAllAsync();
        
        CoursesView = new ObservableCollectionView<Course>(courses);
    }

    private async Task LoadCourseViewModelsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var viewModels = courses.Select(course => new CourseViewModel(_courseService, course)).ToList();
        
        CourseViewModels = new ObservableCollectionView<CourseViewModel>(viewModels);
    }
}