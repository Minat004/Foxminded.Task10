using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class EditStudentViewModel : ObservableObject, IDataHolder, IResultHolder, IClosable
{
    private readonly IStudentService<Student> _studentService;

    public EditStudentViewModel(IStudentService<Student> studentService)
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
            CurrentStudent = ((StudentViewModel)_data!).GetStudent();
        }
    }

    [ObservableProperty] 
    private Student? currentStudent;
    
    [RelayCommand]
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
}