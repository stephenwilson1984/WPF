using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TreeViewsAndValueConverters;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Get every logical drive on the machine
        foreach (var drive in Directory.GetLogicalDrives())
        {
            // Create a new item for it
            var item = new TreeViewItem
            {
                Header = drive,
                Tag = drive
            };

            // Add a dummy item to make it expandable
            item.Items.Add(null);

            // Handle the expansion event to load the subdirectories
            item.Expanded += Folder_Expanded;

            // Add it to the tree
            FolderView.Items.Add(item);
        }
    }

    private static void Folder_Expanded(object sender, RoutedEventArgs e)
    {
        var item = (TreeViewItem)sender;

        // If the item only contains the dummy item, we need to load the subdirectories
        if (item.Items is not [null])
        {
            return;
        }

        // Clear the dummy item
        item.Items.Clear();

        // Get the folder path from the Tag property
        var folderPath = (string)item.Tag;

        AddDirectories(folderPath, item);
        AddFiles(folderPath, item);
    }

    private static void AddFiles(string folderPath, TreeViewItem item)
    {
        // Create a blank list for files
        var filesList = new List<string>();

        // Try and get files from the folder ignoring any issues doing so
        try
        {
            var files = Directory.GetFiles(folderPath);
            if (files.Length > 0)
            {
                filesList.AddRange(files);
            }
        }
        catch
        {
            // ignored
        }

        foreach (var file in filesList)
        {
            // Create file item
            var subItem = new TreeViewItem
            {
                // Set header as file name
                Header = GetFileOrFolderName(file),

                // Add tag as full path
                Tag = file
            };

            // Add the subitem to the view
            item.Items.Add(subItem);
        }
    }

    private static void AddDirectories(string folderPath, TreeViewItem item)
    {
        // Create a blank list for directories
        var directories = new List<string>();

        // Try and get directories from the folder ignoring any issues doing so
        try
        {
            var dirs = Directory.GetDirectories(folderPath);
            if (dirs.Length > 0)
            {
                directories.AddRange(dirs);
            }
        }
        catch
        {
            // ignored
        }

        foreach (var directory in directories)
        {
            // Create directory item
            var subItem = new TreeViewItem
            {
                // Set header as folder name
                Header = GetFileOrFolderName(directory),

                // Add tag as full path
                Tag = directory
            };

            // Add dummy item so we can expand folder
            subItem.Items.Add(null);

            // Handle expanding
            subItem.Expanded += Folder_Expanded;

            // Add the subitem to the view
            item.Items.Add(subItem);
        }
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