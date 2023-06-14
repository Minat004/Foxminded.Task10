using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class EditGroupViewModel : ObservableObject, IDataHolder, IResultHolder, IClosable
{
    private readonly IGroupService<Group> _groupService;

    public EditGroupViewModel(IGroupService<Group> groupService)
    {
        _groupService = groupService;
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
            CurrentGroup = ((GroupViewModel)value!).GetGroup();
            TeacherFullName = $"{CurrentGroup.Teacher!.FirstName} {CurrentGroup.Teacher.LastName}";
        }
    }

    [ObservableProperty]
    private Group? currentGroup;

    [ObservableProperty] 
    private string? teacherFullName;

    [RelayCommand]
    private void EditGroup()
    {
        _groupService.UpdateAsync(CurrentGroup!);

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
}