using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Data;

namespace BUS_MyShop {
    public class RelativePathToAbsolutePathConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string? relativePath = value as string;
            if (relativePath == null)
                return "";
            // check if it is already an absolute path
            if (Path.IsPathRooted(relativePath))
                return relativePath;
            string absolutePath = Path.GetFullPath(relativePath);
            return absolutePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string? absolutePath = value as string;
            if (absolutePath == null)
                return "";

            string relativePath = Path.Combine("Images", Path.GetFileName(absolutePath));
            // copy image to 'Images' folder
            Debug.WriteLine(relativePath);
            // check if exist already, do nothing
            if (File.Exists(relativePath))
                return relativePath;

            File.Copy(absolutePath, relativePath, true);
            return relativePath;
        }
    }

}
