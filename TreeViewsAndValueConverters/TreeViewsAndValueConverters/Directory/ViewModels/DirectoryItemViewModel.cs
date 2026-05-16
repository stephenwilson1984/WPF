using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TreeViewsAndValueConverters.Directory.Data;

namespace TreeViewsAndValueConverters.Directory.ViewModels;

public partial class DirectoryItemViewModel : ObservableObject
{
    public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
    {
        FullPath = fullPath;
        Type = type;
        ExpandCommand = new RelayCommand(Expand);
        ClearChildren();
    }

    [ObservableProperty]
    public partial string FullPath { get; set; }

    [ObservableProperty] 
    public partial DirectoryItemType Type { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<DirectoryItemViewModel?> Children { get; set; } = [];

    public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileOrFolderName(FullPath);

    public bool CanExpand => Type != DirectoryItemType.File;

    public bool IsExpanded
    {
        get
        {
            return Children.Any(c => c is not null);
        }
        set
        {
            if (value)
            {
                Expand();
            }
            else
            {
                ClearChildren();
            }
        }
    }

    public RelayCommand ExpandCommand { get; }

    private void Expand()
    {
        if (Type == DirectoryItemType.File)
        {
            return;
        }

        Children = new ObservableCollection<DirectoryItemViewModel?>(DirectoryStructure.GetDirectoryContents(FullPath)
            .Select(x => new DirectoryItemViewModel(x.FullPath, x.Type)));
    }

    private void ClearChildren()
    {
        Children = [];

        if (Type != DirectoryItemType.File)
        {
            Children.Add(null);
        }
    }
}