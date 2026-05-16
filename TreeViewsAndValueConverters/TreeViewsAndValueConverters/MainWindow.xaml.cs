using System.Windows;
using TreeViewsAndValueConverters.Directory.ViewModels;

namespace TreeViewsAndValueConverters;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new DirectoryStructureViewModel();
    }
}