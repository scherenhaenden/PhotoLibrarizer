using System;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace PhotoLibrarizerCrossPlat.Client.Converters;

public class ByteArrayToBitmapConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is byte[] bytes && bytes.Length > 0)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return new Bitmap(ms);
            }
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms); // Save the bitmap to the memory stream
                return ms.ToArray(); // Return the byte array
            }
        }

        return null;
    }
}