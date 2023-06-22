using System.Collections.ObjectModel;
using System.Windows.Controls;
using Castle.Core.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using University.WPF.Services;
using University.WPF.ViewModels.GroupViewModels;
using University.WPF.Views.UserControls.GroupViews;

namespace University.WPF.Tests.ViewModels.CourseViewModels;

public class CourseViewModelTests
{
    private readonly Mock<ICourseService<Course>> _mockCourseService;
    private readonly Mock<IGroupService<Group>> _mockGroupService;
    private readonly Mock<IStudentService<Student>> _mockStudentService;
    private readonly Mock<ITeacherService<Teacher>> _mockTeacherService;
    private readonly Mock<IDialogService> _mockDialogService;
    private readonly Mock<ICsvService> _mockCsvService;
    private readonly Mock<IPdfService> _mockPdfService;

    
    public CourseViewModelTests()
    {
        _mockCourseService = new Mock<ICourseService<Course>>();
        _mockCourseService
            .Setup(x => x.GetCourseGroupsAsync(It.IsAny<int>()))
            .ReturnsAsync(MockDataHelper.GetGroupsOfCourseById(It.IsAny<int>()));

        _mockGroupService = new Mock<IGroupService<Group>>();
        // _mockGroupService
        //     .Setup(x => x.GetGroupStudentsAsync(It.IsAny<int>()))
        //     .ReturnsAsync(MockDataHelper.GetStudentsOfGroupById(It.IsAny<int>()));
        
        _mockStudentService = new Mock<IStudentService<Student>>();
        _mockTeacherService = new Mock<ITeacherService<Teacher>>();
        
        _mockDialogService = new Mock<IDialogService>();
        _mockDialogService.Setup(x => x.ShowDialog(It.IsAny<UserControl>(),
                It.IsAny<ObservableObject>(), It.IsAny<IDialogConfiguration?>(), null!));
        
        _mockCsvService = new Mock<ICsvService>();
        _mockPdfService = new Mock<IPdfService>();
    }

    [StaFact]
    public void OpenAddGroupDialogCommandTest()
    {
        // Arrange
        var courseViewModel =
            new CourseViewModel(new Course(), _mockCourseService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockTeacherService.Object, _mockDialogService.Object,
                _mockCsvService.Object, _mockPdfService.Object, MockDataHelper.GetConfig());

        // Act
        courseViewModel.OpenAddGroupDialogCommand.Execute(null);

        // Assert
        _mockDialogService.Verify(x => x.ShowDialog(It.IsAny<UserControl>(), 
            It.IsAny<ObservableObject>(), It.IsAny<IDialogConfiguration?>(), null!), Times.Once);
    }

    [StaFact]
    public void OpenEditGroupDialogCommandTest()
    {
        // Arrange
        var courseViewModel =
            new CourseViewModel(new Course(), _mockCourseService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockTeacherService.Object, _mockDialogService.Object,
                _mockCsvService.Object, _mockPdfService.Object, MockDataHelper.GetConfig());
        
        // Act
        courseViewModel.OpenEditGroupDialogCommand.Execute(null);
        
        // Assert
        _mockDialogService.Verify(x => x.ShowDialog(It.IsAny<UserControl>(), 
            It.IsAny<ObservableObject>(), It.IsAny<IDialogConfiguration?>(), null!), Times.Once);
        
        Assert.False(courseViewModel.OpenEditGroupDialogCommand.CanExecute(null));
    }

    [Fact]
    public void DeleteGroupCommandTest()
    {
        // Arrange
        _mockGroupService
            .Setup(x => x.GetGroupStudentsAsync(It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<IEnumerable<Student>>());
        
        var courseViewModel =
            new CourseViewModel(new Course(), _mockCourseService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockTeacherService.Object, _mockDialogService.Object,
                _mockCsvService.Object, _mockPdfService.Object, MockDataHelper.GetConfig());
        
        // Act
        courseViewModel.DeleteGroupCommand.ExecuteAsync(null);
        
        // Assert
        _mockGroupService.Verify(x => x.GetGroupStudentsAsync(It.IsAny<int>()));
        _mockGroupService.Verify(x => x.DeleteAsync(It.IsAny<Group>()), Times.Never);
        
        Assert.False(courseViewModel.DeleteGroupCommand.CanExecute(null));
    }
}