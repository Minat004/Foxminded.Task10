using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.TeacherViewModels;

public partial class TeacherEditDialogViewModel : ObservableValidator, IDataHolder, IResultHolder, IClosable
{
    private readonly ITeacherService<Teacher> _teacherService;

    public TeacherEditDialogViewModel(ITeacherService<Teacher> teacherService)
    {
        _teacherService = teacherService;
    }

    private object? _data;
    public object? Data
    {
        get => _data;
        set
        {
            _data = value;
            CurrentTeacher = (Teacher)_data!;
        }
    }
    
    public object? Result { get; private set; }
    
    public Action? FinishInterAction { get; set; }

    [ObservableProperty] 
    private Teacher? currentTeacher;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditTeacherCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? currentTeacherFirstName;

    partial void OnCurrentTeacherFirstNameChanged(string? value)
    {
        CurrentTeacher!.FirstName = value!;
    }

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditTeacherCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[\p{L}\p{Mn}]+$", ErrorMessage = "The field must have letters only.")]
    private string? currentTeacherLastName;

    partial void OnCurrentTeacherLastNameChanged(string? value)
    {
        CurrentTeacher!.LastName = value!;
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private void EditTeacher()
    {
        _teacherService.UpdateAsync(CurrentTeacher!);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
    
    private bool CanEdit()
    {
        if (string.IsNullOrEmpty(CurrentTeacher!.FirstName) || string.IsNullOrEmpty(CurrentTeacher.LastName))
        {
            return false;
        }

        return !HasErrors;
    }
}