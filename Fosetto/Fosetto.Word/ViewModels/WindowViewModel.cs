using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace Fosetto.Word.ViewModels;

public partial class WindowViewModel : ObservableObject
{
    private readonly Window _window;

    public WindowViewModel(Window window)
    {
        _window = window;
        _window.StateChanged += Window_StateChanged;

        MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
        MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
        CloseCommand = new RelayCommand(() => _window.Close());
        MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));

        // Fix window resize issue
        var resizer = new WindowResizer(_window);
    }

    public double WindowMinimumWidth { get; set; } = 400;

    public double WindowMinimumHeight { get; set; } = 400;

    [ObservableProperty]
    public partial int ResizeBorder { get; set; } = 6;

    [ObservableProperty] 
    public partial int TitleHeight { get; set; } = 35;

    public GridLength TitleHeightGridLength => new(TitleHeight + ResizeBorder);

    public Thickness ResizeBorderThickness => new (ResizeBorder + OuterMarginSize);

    public Thickness InnerContentPadding => new (ResizeBorder);

    public int OuterMarginSize => _window.WindowState == WindowState.Maximized ? 0 : 10;

    public Thickness OuterMarginThickness => new (OuterMarginSize);

    public int WindowRadius => _window.WindowState == WindowState.Maximized ? 0 : 10;

    public CornerRadius WindowCornerRadius => new (WindowRadius);

    public RelayCommand MinimizeCommand { get; set; }

    public RelayCommand MaximizeCommand { get; set; }

    public RelayCommand CloseCommand { get; set; }

    public RelayCommand MenuCommand { get; set; }

    private Point GetMousePosition()
    {
        Point position = Mouse.GetPosition(_window);

        return new Point(position.X + _window.Left, position.Y + _window.Top);
    }

    private void Window_StateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ResizeBorderThickness));
        OnPropertyChanged(nameof(OuterMarginSize));
        OnPropertyChanged(nameof(OuterMarginThickness));
        OnPropertyChanged(nameof(WindowRadius));
        OnPropertyChanged(nameof(WindowCornerRadius));
    }
}