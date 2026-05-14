using System.Windows;
using System.Windows.Controls;

namespace WpfBasics;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show($"Apply button clicked!\n\nDescription: {DescriptionText.Text}");
    }

    private void ResetButton_OnClick(object sender, RoutedEventArgs e)
    {
        WeldCheckbox.IsChecked = false;
        AssemblyCheckbox.IsChecked = false;
        PlasmaCheckbox.IsChecked = false;
        LaserCheckbox.IsChecked = false;
        PurchaseCheckbox.IsChecked = false;
        LatheCheckbox.IsChecked = false;
        DrillCheckbox.IsChecked = false;
        FoldCheckbox.IsChecked = false;
        RollCheckbox.IsChecked = false;
        SawCheckbox.IsChecked = false;

        LengthText.Text = string.Empty;
    }

    private void Checkbox_OnChecked(object sender, RoutedEventArgs e)
    {
        LengthText.Text += ((CheckBox)sender).Content + " ";
    }

    private void FinishComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NoteText is null)
        {
            return;
        }

        var combo = sender as ComboBox;
        var value = combo?.SelectedValue as ComboBoxItem;

        NoteText.Text = $"Selected finish: {value?.Content}";
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        FinishComboBox_OnSelectionChanged(FinishComboBox, null);
    }

    private void SupplierNameText_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        MassText.Text = SupplierNameText.Text;
    }
}