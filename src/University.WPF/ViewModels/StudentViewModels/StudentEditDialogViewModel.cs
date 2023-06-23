using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class StudentEditDialogViewModel : ObservableValidator, IDataHolder, IResultHolder, IClosable
{
    private readonly IStudentService<Student> _studentService;

    public StudentEditDialogViewModel(IStudentService<Student> studentService)
    {
        _studentService = studentService;
    }

    public object? Result { get; private set; }

    public Action? FinishInterAction { get; set; }

    private object? _data;
    public object? Data
    {
        get => _data;
        set
        {
            _data = value;
            CurrentStudent = (Student)_data!;
            CurrentStudentFirstName = CurrentStudent.FirstName;
            CurrentStudentLastName = CurrentStudent.LastName;
        }
    }

    [ObservableProperty] 
    private Student? currentStudent;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditStudentCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? currentStudentFirstName;

    partial void OnCurrentStudentFirstNameChanged(string? value)
    {
        CurrentStudent!.FirstName = value!;
    }

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditStudentCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? currentStudentLastName;

    partial void OnCurrentStudentLastNameChanged(string? value)
    {
        CurrentStudent!.LastName = value!;
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private void EditStudent()
    {
        _studentService.UpdateAsync(CurrentStudent!);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
    
    private bool CanEdit()
    {
        if (string.IsNullOrEmpty(CurrentStudentFirstName) || string.IsNullOrEmpty(CurrentStudentLastName))
        {
            return false;
        }

        return !HasErrors;
    }
}