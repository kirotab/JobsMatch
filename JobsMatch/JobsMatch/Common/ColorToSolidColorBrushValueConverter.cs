using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace JobsMatch.Common
{
    public class ColorToSolidColorBrushValueConverter : IValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    if (value == null)
        //        return null;

        //    if (value is Color)
        //        return new SolidColorBrush((Color)value);

        //    throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "], ColorToSolidColorBrushValueConverter.Convert()");
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    if (value == null)
        //        return null;

        //    if (value is SolidColorBrush)
        //        return ((SolidColorBrush)value).Color;

        //    throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "], ColorToSolidColorBrushValueConverter.ConvertBack()");
        //}




        public static Color FromKnownColor(string colorName)
        {
            return (Color)XamlReader.Load(
                @"<Color xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">"
                 + colorName + "</Color>");
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (value is string)
                value = FromKnownColor(value.ToString());
            if (value is Color)
                return new SolidColorBrush((Color)value);

            return new SolidColorBrush(FromKnownColor("Crimson"));

            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "], ColorToSolidColorBrushValueConverter.Convert()");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            //if (value is SolidColorBrush)
            //    return ((SolidColorBrush)value).Color;
            if (value is SolidColorBrush)
                return FromKnownColor(((SolidColorBrush)value).Color.ToString());

            return "Crimson";
            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name + "], ColorToSolidColorBrushValueConverter.ConvertBack()");
        }

        //List<string> ColorNames = new List<string>{
        //    "AliceBlue",
        //    "AntiqueWhite",
        //    "Aqua",
        //    "Aquamarine",
        //    "Azure",
        //    "Beige",
        //    "Bisque",
        //    "Black",
        //    "BlanchedAlmond",
        //    "Blue",
        //    "BlueViolet",
        //    "Brown",
        //    "BurlyWood",
        //    "CadetBlue",
        //    "Chartreuse",
        //    "Chocolate",
        //    "Coral",
        //    "CornflowerBlue",
        //    "Cornsilk",
        //    "Crimson",
        //    "Cyan",
        //    "DarkBlue",
        //    "DarkCyan",
        //    "DarkGoldenrod",
        //    "DarkGray",
        //    "DarkGreen",
        //    "DarkKhaki",
        //    "DarkMagenta",
        //    "DarkOliveGreen",
        //    "DarkOrange",
        //    "DarkOrchid",
        //    "DarkRed",
        //    "DarkSalmon",
        //    "DarkSeaGreen",
        //    "DarkSlateBlue",
        //    "DarkSlateGray",
        //    "DarkTurquoise",
        //    "DarkViolet",
        //    "DeepPink",
        //    "DeepSkyBlue",
        //    "DimGray",
        //    "DodgerBlue",
        //    "Firebrick",
        //    "FloralWhite",
        //    "ForestGreen",
        //    "Fuchsia",
        //    "Gainsboro",
        //    "GhostWhite",
        //    "Gold",
        //    "Goldenrod",
        //    "Gray",
        //    "Green",
        //    "GreenYellow",
        //    "Honeydew",
        //    "HotPink",
        //    "IndianRed",
        //    "Indigo",
        //    "Ivory",
        //    "Khaki",
        //    "Lavender",
        //    "LavenderBlush",
        //    "LawnGreen",
        //    "LemonChiffon",
        //    "LightBlue",
        //    "LightCoral",
        //    "LightCyan",
        //    "LightGoldenrodYellow",
        //    "LightGray",
        //    "LightGreen",
        //    "LightPink",
        //    "LightSalmon",
        //    "LightSeaGreen",
        //    "LightSkyBlue",
        //    "LightSlateGray",
        //    "LightSteelBlue",
        //    "LightYellow",
        //    "Lime",
        //    "LimeGreen",
        //    "Linen",
        //    "Magenta",
        //    "Maroon",
        //    "MediumAquamarine",
        //    "MediumBlue",
        //    "MediumOrchid",
        //    "MediumPurple",
        //    "MediumSeaGreen",
        //    "MediumSlateBlue",
        //    "MediumSpringGreen",
        //    "MediumTurquoise",
        //    "MediumVioletRed",
        //    "MidnightBlue",
        //    "MintCream",
        //    "MistyRose",
        //    "Moccasin",
        //    "NavajoWhite",
        //    "Navy",
        //    "OldLace",
        //    "Olive",
        //    "OliveDrab",
        //    "Orange",
        //    "OrangeRed",
        //    "Orchid",
        //    "PaleGoldenrod",
        //    "PaleGreen",
        //    "PaleTurquoise",
        //    "PaleVioletRed",
        //    "PapayaWhip",
        //    "PeachPuff",
        //    "Peru",
        //    "Pink",
        //    "Plum",
        //    "PowderBlue",
        //    "Purple",
        //    "Red",
        //    "RosyBrown",
        //    "RoyalBlue",
        //    "SaddleBrown",
        //    "Salmon",
        //    "SandyBrown",
        //    "SeaGreen",
        //    "SeaShell",
        //    "Sienna",
        //    "Silver",
        //    "SkyBlue",
        //    "SlateBlue",
        //    "SlateGray",
        //    "Snow",
        //    "SpringGreen",
        //    "SteelBlue",
        //    "Tan",
        //    "Teal",
        //    "Thistle",
        //    "Tomato",
        //    "Transparent",
        //    "Turquoise",
        //    "Violet",
        //    "Wheat",
        //    "White",
        //    "WhiteSmoke",
        //    "Yellow",
        //    "YellowGreen"
        //};
    }
}


