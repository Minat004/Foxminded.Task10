using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class StudentAddDialogViewModel : ObservableValidator, IResultHolder, IClosable
{
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;

    public StudentAddDialogViewModel(
        IGroupService<Group> groupService,
        IStudentService<Student> studentService)
    {
        _groupService = groupService;
        _studentService = studentService;

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    public object? Result { get; private set; }
    
    public Action? FinishInterAction { get; set; }

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateStudentCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? studentFirstName;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateStudentCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? studentLastName;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateStudentCommand))]
    private Group? selectedGroup;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Group>> groups;
    
    [RelayCommand(CanExecute = nameof(CanCreate))]
    private void CreateStudent()
    {
        var student = new Student
        {
            FirstName = StudentFirstName!,
            LastName = StudentLastName!,
            GroupId = SelectedGroup!.Id
        };

        _studentService.AddAsync(student);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
    
    private bool CanCreate()
    {
        if (string.IsNullOrEmpty(StudentFirstName) || string.IsNullOrEmpty(StudentLastName) || SelectedGroup is null)
        {
            return false;
        }

        return !HasErrors;
    }

    private async Task<ObservableCollection<Group>> GetGroupsAsync()
    {
        var groups = await _groupService.GetAllAsync().ConfigureAwait(false);

        var observeGroups = new ObservableCollection<Group>(groups);

        return observeGroups;
    }
}