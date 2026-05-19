using System.Windows;
using Fosetto.Word.ViewModels;

namespace Fosetto.Word;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new WindowViewModel(this);
    }
}