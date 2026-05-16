using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TreeViewsAndValueConverters.Directory.ViewModels;

public partial class DirectoryStructureViewModel : ObservableObject
{
    public DirectoryStructureViewModel()
    {
        Items = new ObservableCollection<DirectoryItemViewModel>(DirectoryStructure.GetLogicalDrives()
            .Select(x => new DirectoryItemViewModel(x.FullPath, x.Type)));
    }

    [ObservableProperty] 
    public partial ObservableCollection<DirectoryItemViewModel> Items { get; set; }
}