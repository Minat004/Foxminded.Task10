using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;
using University.WPF.Services;
using University.WPF.ViewModels.StudentViewModels;
using University.WPF.Views;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupViewModel : UnitedEntityViewModel
{
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly IDialogService _dialogService;
    private readonly Group _group;

    public GroupViewModel(
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        IDialogService dialogService,
        Group group) : base(group.Id, group.Name)
    {
        _groupService = groupService;
        _studentService = studentService;
        _dialogService = dialogService;
        _group = group;

        CourseId = group.CourseId;
        CourseName = group.Course!.Name;
        TeacherFullName = $"{group.Teacher!.FirstName} {group.Teacher.LastName}";

        LoadStudentsByGroupAsync().GetAwaiter();
    }

    [ObservableProperty] 
    private int? courseId;

    [ObservableProperty] 
    private string? courseName;

    [ObservableProperty] 
    private string teacherFullName;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(OpenEditStudentWindowCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteStudentCommand))]
    private StudentViewModel? selectedStudentViewModel;

    [ObservableProperty]
    private ObservableCollection<StudentViewModel> studentsByGroupViews = new(new List<StudentViewModel>());
    
    [RelayCommand]
    private void OpenCreateStudentWindow()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Student",
            Height = 250,
            Width = 400
        };
        
        var student = (StudentViewModel) _dialogService.ShowDialog(
            new CreateStudentView(), 
            new CreateStudentViewModel(_groupService, _studentService),
            dialogConfiguration)!;

        LoadStudentsByGroupAsync().GetAwaiter();
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenEditStudentWindowOrDeleteStudent))]
    private void OpenEditStudentWindow(StudentViewModel studentViewModel)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Student",
            Height = 250,
            Width = 400
        };
        
        var student = (StudentViewModel) _dialogService.ShowDialog(
            new EditStudentView(), 
            new EditStudentViewModel(_studentService),
            dialogConfiguration, studentViewModel)!;
        
        LoadStudentsByGroupAsync().GetAwaiter();
    }

    [RelayCommand(CanExecute = nameof(CanOpenEditStudentWindowOrDeleteStudent))]
    private void DeleteStudent(StudentViewModel studentViewModel)
    {
        _studentService.DeleteAsync(studentViewModel.GetStudent());
        
        LoadStudentsByGroupAsync().GetAwaiter();
    }

    private bool CanOpenEditStudentWindowOrDeleteStudent(StudentViewModel? studentViewModel)
    {
        return studentViewModel is not null;
    }
    

    public Group GetGroup()
    {
        return _group;
    }

    private async Task LoadStudentsByGroupAsync()
    {
        var students = await _groupService.GetGroupStudentsAsync(_group.Id);
        var viewModels = students.Select(student => new StudentViewModel(student));

        StudentsByGroupViews = new ObservableCollection<StudentViewModel>(viewModels);
    }
}