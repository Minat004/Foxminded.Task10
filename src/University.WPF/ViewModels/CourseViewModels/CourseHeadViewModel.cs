using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.CourseViewModels;

public partial class CourseHeadViewModel : UnitedEntityViewModel
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;
    private readonly ICsvService _csvService;
    private readonly IPdfService _pdfService;
    private readonly IConfiguration _configuration;

    public CourseHeadViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration,
        int id, string name) : base(id, name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _studentService = studentService;
        _teacherService = teacherService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;

        Courses = new NotifyTask<ObservableCollection<CourseViewModel>>(GetCoursesAsync());
    }

    [ObservableProperty]
    private CourseHeadViewModel? selectedItem;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<CourseViewModel>> courses;
    
    private async Task<ObservableCollection<CourseViewModel>> GetCoursesAsync()
    {
        var courses = await _courseService.GetAllAsync().ConfigureAwait(false);
        
        var viewModels = courses.Select(course => 
            new CourseViewModel(
                course, _courseService, _groupService, _studentService, _teacherService, _dialogService,
                _csvService, _pdfService, _configuration));

        var observeViewModels = new ObservableCollection<CourseViewModel>(viewModels);

        return observeViewModels;
    }
}