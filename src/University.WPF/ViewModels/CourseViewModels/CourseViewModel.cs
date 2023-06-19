using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.Services;
using University.WPF.ViewModels.GroupViewModels;
using University.WPF.Views.GroupViews;

namespace University.WPF.ViewModels.CourseViewModels;

public partial class CourseViewModel : UnitedEntityViewModel
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly ITeacherService<Teacher> _teacherService;
    private readonly IDialogService _dialogService;
    private readonly ICsvService _csvService;
    private readonly IPdfService _pdfService;
    private readonly IConfiguration _configuration;

    private readonly Course _course;

    public CourseViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration,
        Course course
        ) : base(course.Id, course.Name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _studentService = studentService;
        _teacherService = teacherService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;
        _course = course;

        Description = course.Description!;

        GroupsByCourseViews = new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsByCourseAsync());
    }
    
    [ObservableProperty] 
    private string description;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OpenEditGroupWindowCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteGroupCommand))]
    private GroupViewModel? selectedGroupViewModel;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<GroupViewModel>> groupsByCourseViews;

    [RelayCommand]
    private void OpenCreateGroupWindow()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Group",
            Height = 250,
            Width = 400
        };
        
        var group = (GroupViewModel) _dialogService.ShowDialog(
            new CreateGroupView(), 
            new CreateGroupViewModel(_courseService, _groupService, _teacherService),
            dialogConfiguration)!;
        
        GroupsByCourseViews = new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsByCourseAsync());
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenEditGroupWindowOrDeleteGroup))]
    private void OpenEditGroupWindow(GroupViewModel groupViewModel)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Group",
            Height = 250,
            Width = 400
        };
        
        var group = (GroupViewModel) _dialogService.ShowDialog(
            new EditGroupView(), 
            new EditGroupViewModel(_groupService),
            dialogConfiguration, groupViewModel)!;
        
        GroupsByCourseViews = new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsByCourseAsync());
    }

    [RelayCommand(CanExecute = nameof(CanOpenEditGroupWindowOrDeleteGroup))]
    private void DeleteGroup(GroupViewModel groupViewModel)
    {
        if (groupViewModel.GetGroup().Students.Count == 0)
        {
            return;
        }
        
        _groupService.DeleteAsync(groupViewModel.GetGroup());
        
        GroupsByCourseViews = new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsByCourseAsync());
    }

    private bool CanOpenEditGroupWindowOrDeleteGroup(GroupViewModel? groupViewModel)
    { 
        return groupViewModel is not null && groupViewModel.CourseId == Id;
    }
    
    private async Task<ObservableCollection<GroupViewModel>> GetGroupsByCourseAsync()
    {
        var groups = await _courseService.GetCourseGroupsAsync(_course.Id).ConfigureAwait(false);
        
        var viewModels = groups.Select(group => new GroupViewModel(
            _groupService, _studentService, _dialogService, _csvService, _pdfService, _configuration, group));

        var observeViewModels = new ObservableCollection<GroupViewModel>(viewModels);

        return observeViewModels;
    }
}