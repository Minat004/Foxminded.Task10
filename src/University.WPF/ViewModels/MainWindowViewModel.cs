using CommunityToolkit.Mvvm.ComponentModel;
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
        IPdfService pdfService,
        IConfiguration configuration,
        ICsvService csvService)
    {
        Menu = 
            new NavigationViewModel(
                courseService, groupService, studentService, teacherService, dialogService, csvService, pdfService, configuration);
    }

    public NavigationViewModel Menu { get; }
}