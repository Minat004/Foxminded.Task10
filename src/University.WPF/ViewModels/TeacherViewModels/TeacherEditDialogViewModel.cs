using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.TeacherViewModels;

public partial class TeacherEditDialogViewModel : ObservableObject, IDataHolder, IResultHolder, IClosable
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
    
    [RelayCommand]
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
}