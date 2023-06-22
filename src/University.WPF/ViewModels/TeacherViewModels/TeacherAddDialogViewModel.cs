using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.TeacherViewModels;

public partial class TeacherAddDialogViewModel : ObservableObject, IResultHolder, IClosable
{
    private readonly ITeacherService<Teacher> _teacherService;

    public TeacherAddDialogViewModel(ITeacherService<Teacher> teacherService)
    {
        _teacherService = teacherService;
    }

    public object? Result { get; private set; }
    
    public Action? FinishInterAction { get; set; }

    [ObservableProperty]
    private string? teacherFirstName;

    [ObservableProperty] 
    private string? teacherLastName;
    
    [RelayCommand]
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
}