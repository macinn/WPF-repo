using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfColorsPreview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PropertyInfo[] props = typeof(Colors).GetProperties(BindingFlags.Static |
            BindingFlags.Public);
            IEnumerable<ColorInfo> colorInfos = props.Select(prop =>
            {
                Color color = (Color)prop.GetValue(null, null);
                return new ColorInfo()
                {
                    Name = prop.Name,
                    Rgb = color,
                    RgbInfo = $"R:{color.R} G:{color.G}, B:{color.B}"
                };
            });
            this.DataContext = colorInfos;
        }
    }
    class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            if (!(value is Color))
                return null;
            return new SolidColorBrush((Color)value);
        }
        public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ColorInfo
    {
        public string Name { get; set; }
        public string RgbInfo { get; set; }
        public Color Rgb { get; set; }
    }
}
