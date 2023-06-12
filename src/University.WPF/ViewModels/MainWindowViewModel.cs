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
    private readonly IGroupService<Group> _groupService;

    public MainWindowViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService)
    {
        _courseService = courseService;
        _groupService = groupService;
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
        SetSelectedItemOrDefault();
    }

    private async Task LoadCoursesViewAsync()
    {
        var courses = await _courseService.GetAllAsync();
        
        CoursesView = new ObservableCollectionView<Course>(courses);
    }

    private async Task LoadCourseViewModelsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var viewModels = courses.Select(course => 
            new CourseViewModel(_courseService, _groupService, course));
        
        CourseViewModels = new ObservableCollection<CourseViewModel>(viewModels);
    }

    private void SetSelectedItemOrDefault()
    {
        foreach (var courseViewModel in CourseViewModels)
        {
            if (courseViewModel.IsSelected)
            {
                SelectedItem = courseViewModel;
                return;
            }

            SelectedItem = courseViewModel.GroupsByCourseViews.FirstOrDefault(x => x.IsSelected);

            if (SelectedItem is not null) return;
        }
    }
}