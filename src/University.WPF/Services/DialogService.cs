using System.Windows;
using University.Core.Interfaces;

namespace University.WPF.Services;

public class DialogService: IDialogService
{
    public object? ShowDialog(object view, object viewModel, IDialogConfiguration? configuration, object data)
    {
        var window = new Window
        {
            Title = configuration!.Title,
            Height = configuration.Height,
            Width = configuration.Width,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Content = view,
            DataContext = viewModel
        };

        if (viewModel is IDataHolder dataHolder)
        {
            dataHolder.Data = data;
        }

        if (viewModel is IClosable closable)
        {
            closable.FinishInterAction = () => window.Close();
        }

        window.ShowDialog();

        return (viewModel as IResultHolder)!.Result;
    }
}