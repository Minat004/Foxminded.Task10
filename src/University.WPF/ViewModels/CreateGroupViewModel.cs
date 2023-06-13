using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels;

public partial class CreateGroupViewModel : ObservableObject, IResultHolder, IClosable
{
    private readonly ICourseService<Course> _courseService;
    private readonly IGroupService<Group> _groupService;
    private readonly ITeacherService<Teacher> _teacherService;

    public CreateGroupViewModel(
        ICourseService<Course> courseService,
        IGroupService<Group> groupService,
        ITeacherService<Teacher> teacherService)
    {
        _courseService = courseService;
        _groupService = groupService;
        _teacherService = teacherService;

        LoadCoursesAsync().GetAwaiter();
        LoadTeachersAsync().GetAwaiter();
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
    private ObservableCollection<Course> courses = new();

    [ObservableProperty]
    private ObservableCollection<Teacher> teachers = new();

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

    private async Task LoadCoursesAsync()
    {
        var enumerableCourses = await _courseService.GetAllAsync();
        
        Courses = new ObservableCollection<Course>(enumerableCourses);
        
        SelectedCourse = Courses[0];
    }

    private async Task LoadTeachersAsync()
    {
        var enumerableTeachers = await _teacherService.GetAllAsync();

        Teachers = new ObservableCollection<Teacher>(enumerableTeachers);

        SelectedTeacher = Teachers[0];
    }
}