using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class StudentAddDialogViewModel : ObservableObject, IResultHolder, IClosable
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
    private string? studentFirstName;
    
    [ObservableProperty] 
    private string? studentLastName;

    [ObservableProperty] 
    private Group? selectedGroup;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Group>> groups;
    
    [RelayCommand]
    private void CreateStudent()
    {
        var student = new Student()
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

    private async Task<ObservableCollection<Group>> GetGroupsAsync()
    {
        var groups = await _groupService.GetAllAsync().ConfigureAwait(false);

        var observeGroups = new ObservableCollection<Group>(groups);

        return observeGroups;
    }
}