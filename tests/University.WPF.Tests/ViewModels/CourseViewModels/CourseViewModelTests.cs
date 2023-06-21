using System.Collections.ObjectModel;
using Castle.Core.Configuration;
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
        _mockStudentService = new Mock<IStudentService<Student>>();
        _mockTeacherService = new Mock<ITeacherService<Teacher>>();
        
        _mockDialogService = new Mock<IDialogService>();
        _mockDialogService
            .Setup(x => x.ShowDialog(new GroupAddDialogView(),
                new GroupAddDialogViewModel(_mockCourseService.Object, _mockGroupService.Object,
                    _mockTeacherService.Object),
                GetAddDialogConfiguration(), null!))
            .Returns(GetAddGroup);
        
        _mockCsvService = new Mock<ICsvService>();
        _mockPdfService = new Mock<IPdfService>();
    }

    [StaFact]
    public void OpenAddGroupDialogCommandTest()
    {
        // Arrange
        var courseViewModel =
            new CourseViewModel(GetCourse(), _mockCourseService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockTeacherService.Object, _mockDialogService.Object,
                _mockCsvService.Object, _mockPdfService.Object, MockDataHelper.GetConfig());

        // Act
        courseViewModel.OpenAddGroupDialogCommand.Execute(null);

        // Assert
        _mockDialogService.Verify(x => x.ShowDialog(new GroupAddDialogView(), 
            new GroupAddDialogViewModel(_mockCourseService.Object, _mockGroupService.Object, _mockTeacherService.Object), 
            GetAddDialogConfiguration(), null!), Times.Once);
    }

    private Course GetCourse()
    {
        return new Course
        {
            Id = 1,
            Name = "Space Engineering",
            Description = "Department of Space Engineering"
        };
    }

    private Group GetAddGroup()
    {
        return new Group()
        {
            Name = "add",
            CourseId = 1,
            TeacherId = 1,
        };
    }

    private IDialogConfiguration GetAddDialogConfiguration()
    {
        return new DialogConfiguration()
        {
            Title = "Add Group",
            Height = 250,
            Width = 400
        };
    }
}