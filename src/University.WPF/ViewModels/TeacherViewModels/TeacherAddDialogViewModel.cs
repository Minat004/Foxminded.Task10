using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.TeacherViewModels;

public partial class TeacherAddDialogViewModel : ObservableValidator, IResultHolder, IClosable
{
    private readonly ITeacherService<Teacher> _teacherService;

    public TeacherAddDialogViewModel(ITeacherService<Teacher> teacherService)
    {
        _teacherService = teacherService;
    }

    public object? Result { get; private set; }
    
    public Action? FinishInterAction { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateTeacherCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? teacherFirstName;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(CreateTeacherCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? teacherLastName;
    
    [RelayCommand(CanExecute = nameof(CanCreate))]
    private void CreateTeacher()
    {
        var teacher = new Teacher
        {
            FirstName = TeacherFirstName!,
            LastName = TeacherLastName!
        };

        _teacherService.AddAsync(teacher);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
    
    private bool CanCreate()
    {
        if (string.IsNullOrEmpty(TeacherFirstName) || string.IsNullOrEmpty(TeacherLastName))
        {
            return false;
        }

        return !HasErrors;
    }
}