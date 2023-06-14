using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels.CourseViewModels;

namespace University.WPF.ViewModels;

public partial class HomeViewModel : UnitedEntityViewModel
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;

    public HomeViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        int id, string name) : base(id, name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _studentService = studentService;
        _teacherService = teacherService;
        _dialogService = dialogService;

        LoadCourseViewModelsAsync().GetAwaiter();
    }
    
    [ObservableProperty]
    private ObservableCollection<CourseViewModel> courseViewModels = new(new List<CourseViewModel>());
    
    private async Task LoadCourseViewModelsAsync()
    {
        var courses = await _courseService.GetAllAsync();
        var viewModels = courses.Select(course => 
            new CourseViewModel(_courseService, _groupService, _studentService, _teacherService, _dialogService, course));
        
        CourseViewModels = new ObservableCollection<CourseViewModel>(viewModels);
    }
}