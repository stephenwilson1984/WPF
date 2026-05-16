using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TreeViewsAndValueConverters.Directory;

namespace TreeViewsAndValueConverters;

[ValueConversion(typeof(string), typeof(BitmapImage))]
public class HeaderToImageConverter : IValueConverter
{
    public static HeaderToImageConverter Instance = new HeaderToImageConverter();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // If the path is null, ignore
        if (value is not string path)
        {
            return null;
        }

        // Get the name of the file or folder
        var name = DirectoryStructure.GetFileOrFolderName(path);

        // By default, we assume file
        var image = "Images/file.png";

        // If the name is blank, we presume it's a drive as we cannot have a blank file or folder name
        if (string.IsNullOrEmpty(name))
        {
            image = "Images/drive.png";
        }
        else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
        {
            image = "Images/folder-closed.png";
        }

        return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}