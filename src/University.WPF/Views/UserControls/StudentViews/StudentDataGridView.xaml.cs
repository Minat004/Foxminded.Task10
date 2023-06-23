using System.Collections.ObjectModel;
using System.Windows;
using University.Core.Models;

namespace University.WPF.Views.UserControls.StudentViews;

public partial class StudentDataGridView
{
    public StudentDataGridView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty CollectionProperty =
        DependencyProperty.Register(
            nameof(Collection), typeof(ObservableCollection<Student>), typeof(StudentDataGridView));

    public ObservableCollection<Student> Collection
    {
        get => (ObservableCollection<Student>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
    
    public static readonly DependencyProperty IsImportButtonProperty =
        DependencyProperty.Register(
            nameof(IsImportButton), typeof(Visibility), typeof(StudentDataGridView),
            new FrameworkPropertyMetadata(Visibility.Collapsed));

    public Visibility IsImportButton
    {
        get => (Visibility)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
    
    public static readonly DependencyProperty IsExportButtonProperty =
        DependencyProperty.Register(
            nameof(IsExportButton), typeof(Visibility), typeof(StudentDataGridView),
            new FrameworkPropertyMetadata(Visibility.Collapsed));

    public Visibility IsExportButton
    {
        get => (Visibility)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
    
    public static readonly DependencyProperty IsReportButtonProperty =
        DependencyProperty.Register(
            nameof(IsReportButton), typeof(Visibility), typeof(StudentDataGridView),
            new FrameworkPropertyMetadata(Visibility.Collapsed));

    public Visibility IsReportButton
    {
        get => (Visibility)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}