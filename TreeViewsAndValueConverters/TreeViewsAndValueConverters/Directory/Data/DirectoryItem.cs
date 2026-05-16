namespace TreeViewsAndValueConverters.Directory.Data;

public class DirectoryItem
{
    public string Name => Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileOrFolderName(FullPath);

    public DirectoryItemType Type { get; set; }

    public string FullPath { get; set; }
}