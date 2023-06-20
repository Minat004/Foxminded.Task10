using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using University.Core.Interfaces;
using University.Core.Models;
using University.Core.Models.Mapping;
using University.WPF.Services;
using University.WPF.ViewModels.StudentViewModels;
using University.WPF.Views.UserControls.StudentViews;

namespace University.WPF.ViewModels.GroupViewModels;

public partial class GroupViewModel : UnitedEntityViewModel
{
    private readonly IGroupService<Group> _groupService;
    private readonly IStudentService<Student> _studentService;
    private readonly IDialogService _dialogService;
    private readonly ICsvService _csvService;
    private readonly IPdfService _pdfService;
    private readonly IConfiguration _configuration;

    public GroupViewModel(
        IGroupService<Group> groupService,
        IStudentService<Student> studentService,
        IDialogService dialogService,
        ICsvService csvService,
        IPdfService pdfService,
        IConfiguration configuration,
        Group group) 
        : base(group.Id, group.Name)
    {
        _groupService = groupService;
        _studentService = studentService;
        _dialogService = dialogService;
        _csvService = csvService;
        _pdfService = pdfService;
        _configuration = configuration;

        Group = group;

        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    [ObservableProperty] 
    private Group group;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Student>> studentsByGroup;

    [ObservableProperty] 
    private NotifyTask<ObservableCollection<Group>> groups;

    [ObservableProperty] 
    private GroupViewModel? selectedItem;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(OpenStudentEditDialogCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteStudentCommand))]
    private Student? selectedStudent;
    
    [RelayCommand]
    private void OpenStudentAddDialog()
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Add Student",
            Height = 250,
            Width = 400
        };
        
        var student = (Student) _dialogService.ShowDialog(
            new StudentAddWindowView(), 
            new StudentAddDialogViewModel(_groupService, _studentService),
            dialogConfiguration)!;

        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }
    
    
    [RelayCommand(CanExecute = nameof(CanOpenStudentEditDialogOrDeleteStudent))]
    private void OpenStudentEditDialog(Student oldStudent)
    {
        IDialogConfiguration dialogConfiguration = new DialogConfiguration()
        {
            Title = "Edit Student",
            Height = 250,
            Width = 400
        };
        
        var student = (Student) _dialogService.ShowDialog(
            new StudentEditDialogView(), 
            new StudentEditDialogViewModel(_studentService),
            dialogConfiguration, oldStudent)!;
        
        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    [RelayCommand(CanExecute = nameof(CanOpenStudentEditDialogOrDeleteStudent))]
    private void DeleteStudent(Student oldStudent)
    {
        _studentService.DeleteAsync(oldStudent);
        
        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }

    [RelayCommand]
    private void ImportStudents()
    {
        var filePath = _configuration["CsvHelper:ImportFilePath"];
        
        _csvService.Save<Student, StudentMapCsvSave>(filePath!, Group.Students);
    }

    [RelayCommand]
    private void ExportStudents()
    {
        var filePath = _configuration["CsvHelper:ExportFilePath"];

        var loadStudents = _csvService.Load<Student, StudentMapCsvLoad>(filePath!);

        foreach (var student in loadStudents)
        {
            _studentService.AddAsync(student!);
        }

        StudentsByGroup = new NotifyTask<ObservableCollection<Student>>(GetStudentsByGroupAsync());

        Groups = new NotifyTask<ObservableCollection<Group>>(GetGroupsAsync());
    }
    
    [RelayCommand]
    private void SaveReport()
    {
        Group.Students = StudentsByGroup.Result;
        _pdfService.SaveReport(Group);
    }

    private bool CanOpenStudentEditDialogOrDeleteStudent(Student? student)
    {
        return student is not null;
    }
    
    private async Task<ObservableCollection<Student>> GetStudentsByGroupAsync()
    {
        var students = await _groupService.GetGroupStudentsAsync(Group.Id).ConfigureAwait(false);
        
        var observeStudents = new ObservableCollection<Student>(students);

        return observeStudents;
    }

    private async Task<ObservableCollection<Group>> GetGroupsAsync()
    {
        var groups = await _groupService.GetAllAsync().ConfigureAwait(false);
        
        var observeGroups = new ObservableCollection<Group>(groups);

        return observeGroups;
    }
}