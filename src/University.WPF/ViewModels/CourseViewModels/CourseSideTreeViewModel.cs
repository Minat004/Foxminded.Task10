using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.CourseViewModels;

public partial class CourseSideTreeViewModel : ObservableObject
{
    public CourseSideTreeViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration) 
    {
        HeaderOfCourses.Add(
            new CourseHeadViewModel(
                courseService, groupService, studentService, teacherService, dialogService,
                csvService, pdfService, configuration, 0, "Courses"));

        SelectedItem = HeaderOfCourses[0];
    }

    [ObservableProperty] 
    private ObservableCollection<CourseHeadViewModel> headerOfCourses = new();

    [ObservableProperty] 
    private UnitedEntityViewModel? selectedItem;
    
    [RelayCommand]
    private void SetSelectedItem()
    {
        foreach (var item in HeaderOfCourses[0].Courses.Result)
        {
            if (item.IsSelected)
            {
                SelectedItem = item;
                return;
            }

            SelectedItem = item.GroupsViewModelByCourse.Result.FirstOrDefault(x => x.IsSelected);

            if (SelectedItem is not null) return;

            SelectedItem = HeaderOfCourses[0];
        }
    }
}