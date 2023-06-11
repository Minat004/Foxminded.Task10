using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    private ObservableCollection<CourseViewModel> courseViewModels = new(new List<CourseViewModel>());

    [ObservableProperty] 
    private UnitedEntityViewModel? selectedItem;

    [RelayCommand]
    private void SetSelectedItem()
    {
        SelectedItem = CourseViewModels.FirstOrDefault(x => x.IsSelected)!;
    }

    private async Task LoadCoursesViewAsync()
    {
        var courses = await _courseService.GetAllAsync();
        
        CoursesView = new ObservableCollectionView<Course>(courses);
    }

    private async Task LoadCourseViewModelsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var viewModels = courses.Select(course => new CourseViewModel(_courseService, course));
        
        CourseViewModels = new ObservableCollection<CourseViewModel>(viewModels);
        
        SelectedItem = CourseViewModels.FirstOrDefault(x => x.IsSelected)!;
    }
}