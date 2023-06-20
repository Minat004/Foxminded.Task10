using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupAddDialogViewModel : ObservableObject, IResultHolder, IClosable
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

        // SelectedCourse = Courses.Result[0];

        // SelectedTeacher = Teachers.Result[0];
    }

    public object? Result { get; private set; }

    public Action? FinishInterAction { get; set; }

    [ObservableProperty]
    private string? groupName;

    [ObservableProperty] 
    private Course? selectedCourse;

    [ObservableProperty] 
    private Teacher? selectedTeacher;

    [ObservableProperty]
    private NotifyTask<ObservableCollection<Course>> courses;

    [ObservableProperty]
    private NotifyTask<ObservableCollection<Teacher>> teachers;

    [RelayCommand]
    private void CreateGroup()
    {
        var group = new Group
        {
            Name = GroupName!,
            CourseId = SelectedCourse!.Id,
            TeacherId = SelectedTeacher!.Id,
        };

        _groupService.AddAsync(group);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
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