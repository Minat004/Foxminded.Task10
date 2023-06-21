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
using University.WPF.Views.UserControls.GroupViews;

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

    public CourseViewModel(
        Course course,
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        ITeacherService<Teacher> teacherService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration) 
        : base(course.Id, course.Name)
    {
        _courseService = courseService;
        _groupService = groupService;
        _studentService = studentService;
        _teacherService = teacherService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;

        Course = course;

        GroupsViewModelByCourse = new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsViewModelByCourseAsync());
        GroupsByCourse = new NotifyTask<ObservableCollection<Group>>(GetGroupsByCourseAsync());
        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    [ObservableProperty] 
    private Course course;

    [ObservableProperty] 
    private CourseViewModel? selectedItem;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(OpenEditGroupDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteGroupCommand))]
    private Group? selectedGroup;

    [ObservableProperty]
    private NotifyTask<ObservableCollection<GroupViewModel>> groupsViewModelByCourse;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Group>> groupsByCourse;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Group>> groups;

    [RelayCommand]
    private void OpenAddGroupDialog()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Group",
            Height = 250,
            Width = 400
        };
        
        var group = (Group) _dialogService.ShowDialog(
            new GroupAddDialogView(),
            new GroupAddDialogViewModel(_courseService, _groupService, _teacherService),
            dialogConfiguration, null!)!;
        
        GroupsByCourse = new NotifyTask<ObservableCollection<Group>>(GetGroupsByCourseAsync());
        
        GroupsViewModelByCourse =
            new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsViewModelByCourseAsync());
        
        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenEditGroupDialogOrDeleteGroup))]
    private void OpenEditGroupDialog(Group oldGroup)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Group",
            Height = 250,
            Width = 400
        };
        
        var group = (Group) _dialogService.ShowDialog(
            new GroupEditDialogView(), 
            new GroupEditDialogViewModel(_groupService),
            dialogConfiguration, oldGroup)!;
        
        GroupsByCourse = new NotifyTask<ObservableCollection<Group>>(GetGroupsByCourseAsync());
        
        GroupsViewModelByCourse =
            new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsViewModelByCourseAsync());
        
        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    [RelayCommand(CanExecute = nameof(CanOpenEditGroupDialogOrDeleteGroup))]
    private async Task DeleteGroup(Group oldGroup)
    {
        var studentsGroup = await _groupService.GetGroupStudentsAsync(oldGroup.Id);
        
        if (studentsGroup.Count() != 0)
        {
            return;
        }
        
        await _groupService.DeleteAsync(oldGroup);
        
        GroupsByCourse = new NotifyTask<ObservableCollection<Group>>(GetGroupsByCourseAsync());
        
        GroupsViewModelByCourse =
            new NotifyTask<ObservableCollection<GroupViewModel>>(GetGroupsViewModelByCourseAsync());
        
        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }
    
    private bool CanOpenEditGroupDialogOrDeleteGroup(Group? group)
    {
        return group is not null;
    }

    private async Task<ObservableCollection<Group>> GetGroupsAsync()
    {
        var allGroups = await _groupService.GetAllAsync().ConfigureAwait(false);

        var observeGroups = new ObservableCollection<Group>(allGroups);

        return observeGroups;
    }
    
    private async Task<ObservableCollection<GroupViewModel>> GetGroupsViewModelByCourseAsync()
    {
        var courseGroups = await _courseService.GetCourseGroupsAsync(Course.Id).ConfigureAwait(false);
        
        var viewModels = courseGroups.Select(group => 
            new GroupViewModel(
                _groupService, _studentService, _dialogService, _csvService, _pdfService, _configuration, group));

        var observeViewModels = new ObservableCollection<GroupViewModel>(viewModels);

        return observeViewModels;
    }
    
    private async Task<ObservableCollection<Group>> GetGroupsByCourseAsync()
    {
        var courseGroups = await _courseService.GetCourseGroupsAsync(Course.Id).ConfigureAwait(false);

        var observeGroups = new ObservableCollection<Group>(courseGroups);

        return observeGroups;
    }
}