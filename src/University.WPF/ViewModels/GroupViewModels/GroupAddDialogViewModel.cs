using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupAddDialogViewModel : ObservableValidator, IResultHolder, IClosable
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly ITeacherService<Teacher> _teacherService;

    public GroupAddDialogViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        ITeacherService<Teacher> teacherService)
    {
        _courseService = courseService;
        _groupService = groupService;
        _teacherService = teacherService;

        Courses = new NotifyTask<ObservableCollection<Course>>(GetCoursesAsync());

        Teachers = new NotifyTask<ObservableCollection<Teacher>>(GetTeachersAsync());
    }

    public object? Result { get; private set; }

    public Action? FinishInterAction { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateGroupCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(10)]
    private string? groupName;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateGroupCommand))]
    private Course? selectedCourse;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateGroupCommand))]
    private Teacher? selectedTeacher;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Course>> courses;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Teacher>> teachers;

    [RelayCommand(CanExecute = nameof(CanCreate))]
    private void CreateGroup()
    {
        var group = new Group
        {
            Name = GroupName!,
            CourseId = SelectedCourse!.Id,
            TeacherId = SelectedTeacher!.Id,
        };

        Result = group;

        _groupService.AddAsync(group);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }

    private bool CanCreate()
    {
        if (string.IsNullOrEmpty(GroupName) || SelectedCourse is null || SelectedTeacher is null)
        {
            return false;
        }

        return !HasErrors;
    }

    private async Task<ObservableCollection<Course>> GetCoursesAsync()
    {
        var courses = await _courseService.GetAllAsync().ConfigureAwait(false);

        var observeCourses = new ObservableCollection<Course>(courses);

        return observeCourses;
    }

    private async Task<ObservableCollection<Teacher>> GetTeachersAsync()
    {
        var teachers = await _teacherService.GetAllAsync().ConfigureAwait(false);

        var observeTeachers = new ObservableCollection<Teacher>(teachers);

        return observeTeachers;
    }
}