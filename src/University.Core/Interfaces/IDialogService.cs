namespace University.Core.Interfaces;

public interface IDialogService
{
    public object? ShowDialog(object view, object viewModel, IDialogConfiguration? configuration, object data = null!);
}