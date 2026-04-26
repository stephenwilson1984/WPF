using System.Windows;

namespace WpfBasics;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show($"Apply button clicked!\nDescription: {DescriptionText.Text}");
    }
}