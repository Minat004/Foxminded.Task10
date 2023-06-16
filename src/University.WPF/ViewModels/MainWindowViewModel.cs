using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    public MainWindowViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        IConfiguration configuration,
        ICsvService csvService)
    {
        HomeViewModels.Add(
            new HomeViewModel(courseService, groupService, studentService, teacherService, 
                dialogService, csvService, configuration, 0, "Course"));

        SelectedItem = HomeViewModels[0];
    }

    [ObservableProperty]
    private ObservableCollection<HomeViewModel> homeViewModels = new();

    [ObservableProperty] 
    private UnitedEntityViewModel? selectedItem;

    [RelayCommand]
    private void SetSelectedItem()
    {
        foreach (var courseViewModel in HomeViewModels[0].CourseViewModels)
        {
            if (courseViewModel.IsSelected)
            {
                SelectedItem = courseViewModel;
                return;
            }

            SelectedItem = courseViewModel.GroupsByCourseViews.FirstOrDefault(x => x.IsSelected);

            if (SelectedItem is not null) return;

            SelectedItem = HomeViewModels[0];
        }
    }
}