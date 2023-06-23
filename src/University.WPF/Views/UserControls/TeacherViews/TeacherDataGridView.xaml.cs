using System.Collections.ObjectModel;
using System.Windows;
using University.Core.Models;

namespace University.WPF.Views.UserControls.TeacherViews;

public partial class TeacherDataGridView
{
    public TeacherDataGridView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty CollectionProperty =
        DependencyProperty.Register(
            nameof(Collection), typeof(ObservableCollection<Teacher>), typeof(TeacherDataGridView));

    public ObservableCollection<Teacher> Collection
    {
        get => (ObservableCollection<Teacher>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}