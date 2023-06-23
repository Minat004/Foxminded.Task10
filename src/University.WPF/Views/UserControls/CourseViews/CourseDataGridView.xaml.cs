using System.Collections.ObjectModel;
using System.Windows;
using University.WPF.ViewModels;
using CourseViewModel = University.WPF.ViewModels.CourseViewModels.CourseViewModel;

namespace University.WPF.Views.UserControls.CourseViews;

public partial class CourseDataGridView
{
    public CourseDataGridView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty CollectionProperty =
        DependencyProperty.Register(
            nameof(Collection), typeof(ObservableCollection<CourseViewModel>), typeof(CourseDataGridView));

    public ObservableCollection<CourseViewModel> Collection
    {
        get => (ObservableCollection<CourseViewModel>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}