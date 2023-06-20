using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupEditDialogViewModel : ObservableObject, IDataHolder, IResultHolder, IClosable
{
    private readonly IGroupService<Group> _groupService;

    public GroupEditDialogViewModel(IGroupService<Group> groupService)
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
            CurrentGroup = (Group)value!;
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

        Result = CurrentGroup;

        FinishInterAction!();
    }

    [RelayCommand]
    private void Cancel()
    {
        FinishInterAction!();
    }
}