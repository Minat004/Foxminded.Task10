using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.ViewModels.CourseViewModels;
using University.WPF.ViewModels.GroupViewModels;
using University.WPF.ViewModels.TeacherViewModels;

namespace University.WPF.ViewModels;

public partial class NavigationViewModel : ObservableObject
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;
    private readonly ICsvService _csvService;
    private readonly IPdfService _pdfService;
    private readonly IConfiguration _configuration;

    public NavigationViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration)
    {
        _courseService = courseService;
        _groupService = groupService;
        _studentService = studentService;
        _teacherService = teacherService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;

        MenuItems = new ObservableCollectionListSource<UnitedEntityViewModel>(GetMenuItems());

        SelectedMenuItem = MenuItems[0];
    }

    [ObservableProperty] 
    private ObservableCollection<UnitedEntityViewModel> menuItems;

    [ObservableProperty] 
    private UnitedEntityViewModel selectedMenuItem;

    private IEnumerable<UnitedEntityViewModel> GetMenuItems()
    {
        var items = _configuration.GetSection("Menu").Get<string[]>();

        var result = new List<UnitedEntityViewModel>
        {
            new CourseWorkSpaceViewModel(
                _courseService, _groupService, _studentService, _teacherService, _dialogService,
                _csvService, _pdfService, _configuration, 0, items![0]),
            
            new CourseViewModel(new Course {Id = 1, Name = items[1]}, _courseService, _groupService, _studentService,
                _teacherService, _dialogService, _csvService, _pdfService, _configuration),
            
            new GroupViewModel(_groupService, _studentService,
                _dialogService, _csvService, _pdfService, _configuration, new Group { Id = 2, Name = items[2]}),
            
            new TeacherViewModel(_teacherService, _dialogService, new Teacher {Id = 3, FirstName = items[3]})
        };

        return result;
    }
}