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
        MessageBox.Show($"The description is: {DescriptionText.Text}");
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

        LengthText.Text = "";
    }

    private void Checkbox_OnChecked(object sender, RoutedEventArgs e)
    {
        LengthText.Text += ((CheckBox)sender).Content + ", ";
    }

    private void FinishCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var combobox = (ComboBox)sender;
        var value = (ComboBoxItem)combobox.SelectedValue;

        if (NoteText is not null)
        {
            NoteText.Text = value.Content.ToString() ?? "";
        }
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        FinishCombobox_OnSelectionChanged(FinishCombobox, null);
    }

    private void SupplierNameText_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        MassText.Text = SupplierNameText.Text;
    }
}