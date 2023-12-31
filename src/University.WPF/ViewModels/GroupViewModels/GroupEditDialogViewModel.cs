﻿using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupEditDialogViewModel : ObservableValidator, IDataHolder, IResultHolder, IClosable
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
            CurrentGroupName = CurrentGroup.Name;
            TeacherFullName = $"{CurrentGroup.Teacher!.FirstName} {CurrentGroup.Teacher.LastName}";
        }
    }

    [ObservableProperty]
    private Group? currentGroup;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(EditGroupCommand))]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(10)]
    private string? currentGroupName;

    [ObservableProperty] 
    private string? teacherFullName;

    [RelayCommand(CanExecute = nameof(CanEdit))]
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
    
    private bool CanEdit()
    {
        if (string.IsNullOrEmpty(CurrentGroupName))
        {
            return false;
        }

        return !HasErrors;
    }
}