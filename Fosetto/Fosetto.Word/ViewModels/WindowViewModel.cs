using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Fosetto.Word.ViewModels;

public partial class WindowViewModel : ObservableObject
{
    private readonly Window _window;

    public WindowViewModel(Window window)
    {
        _window = window;
        _window.StateChanged += Window_StateChanged;
    }

    [ObservableProperty]
    public partial int ResizeBorder { get; set; } = 6;

    public Thickness ResizeBorderThickness => new (ResizeBorder + OuterMarginSize);

    public int OuterMarginSize => _window.WindowState == WindowState.Maximized ? 0 : 10;

    public Thickness OuterMarginThickness => new (OuterMarginSize);

    public int WindowRadius => _window.WindowState == WindowState.Maximized ? 0 : 10;

    public CornerRadius WindowCorderRadius => new (WindowRadius);

    private void Window_StateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ResizeBorderThickness));
        OnPropertyChanged(nameof(OuterMarginThickness));
        OnPropertyChanged(nameof(WindowCorderRadius));
    }
}