using System.Collections.ObjectModel;
using System.Windows;
using University.Core.Models;

namespace University.WPF.Views.UserControls.GroupViews;

public partial class GroupDataGridView
{
    public GroupDataGridView()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty CollectionProperty =
        DependencyProperty.Register(
            nameof(Collection), typeof(ObservableCollection<Group>), typeof(GroupDataGridView));

    public ObservableCollection<Group> Collection
    {
        get => (ObservableCollection<Group>)GetValue(CollectionProperty);
        set => SetValue(CollectionProperty, value);
    }
}