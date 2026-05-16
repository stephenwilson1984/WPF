using TreeViewsAndValueConverters.Directory.Data;

namespace TreeViewsAndValueConverters.Directory;

public static class DirectoryStructure
{
    public static List<DirectoryItem> GetLogicalDrives()
    {
        return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem()
        {
            FullPath = drive,
            Type = DirectoryItemType.Drive
        }).ToList();
    }

    public static List<DirectoryItem> GetDirectoryContents(string fullPath)
    {
        var itemsToReturn = new List<DirectoryItem>();

        try
        {
            var dirs = System.IO.Directory.GetDirectories(fullPath);
            if (dirs.Length > 0)
            {
                itemsToReturn.AddRange(dirs.Select(folder => new DirectoryItem
                {
                    FullPath = folder,
                    Type = DirectoryItemType.Folder
                }));
            }
        }
        catch
        {
            // ignored
        }

        try
        {
            var files = System.IO.Directory.GetFiles(fullPath);
            if (files.Length > 0)
            {
                itemsToReturn.AddRange(files.Select(file => new DirectoryItem
                {
                    FullPath = file,
                    Type = DirectoryItemType.File
                }));
            }
        }
        catch
        {
            // ignored
        }

        return itemsToReturn;
    }

    public static string GetFileOrFolderName(string path)
    {
        // C:\Something\SomeFolder
        // C:\Something\File.png

        // If we have no path, return empty string
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }

        // Make all forward slashes backslashes
        var normalizedPath = path.Replace('/', '\\');

        // Find the last backslash in the path
        var lastIndex = normalizedPath.LastIndexOf('\\');

        // If we don't find a backslash, return the path itself
        if (lastIndex <= 0)
        {
            return path;
        }

        // Return the name after the last backslash
        return path.Substring(lastIndex + 1);
    }
}