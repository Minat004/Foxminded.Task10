using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University.Core.Interfaces;
using University.Core.Models;

namespace University.WPF.ViewModels.StudentViewModels;

public partial class CreateStudentViewModel : ObservableObject, IResultHolder, IClosable
{
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;

    public CreateStudentViewModel(
        IGroupService<Group> groupService,
        IStudentService<Student> studentService)
    {
        _groupService = groupService;
        _studentService = studentService;

        LoadGroupsAsync().GetAwaiter();
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
    private ObservableCollection<Group> groups = new();
    
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

    private async Task LoadGroupsAsync()
    {
        var enumerableGroups = await _groupService.GetAllAsync();

        Groups = new ObservableCollection<Group>(enumerableGroups);

        SelectedGroup = Groups[0];
    }
}