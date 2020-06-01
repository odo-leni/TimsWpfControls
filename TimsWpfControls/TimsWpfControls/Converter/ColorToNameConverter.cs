﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TimsWpfControls.Converter
{
    [MarkupExtensionReturnType(typeof(ColorToNameConverter))]
    [ValueConversion(typeof(Color), typeof(string))]
    public class ColorToNameConverter :  MarkupExtension, IValueConverter, IMultiValueConverter
    {
        ColorToNameConverter _instance;

        /// <summary>
        /// Converts a given <see cref="Color"/> to its Name
        /// </summary>
        /// <param name="values">Needed: The <see cref="Color"/>. </param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Optional: A <see cref="Dictionary{Color?, string}"/></param>
        /// <param name="culture"></param>
        /// <returns>The name of the color or the Hex-Code if no name is available</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return ColorHelper.GetColorName(color, parameter as Dictionary<Color?, string>);
            }
            else
            {
                throw new InvalidCastException("Unable to convert the provided value to System.Windows.Media.Color");
            }
        }

        /// <summary>
        /// Converts a given <see cref="Color"/> to its Name
        /// </summary>
        /// <param name="values">Needed: The <see cref="Color"/>. Optional: A <see cref="Dictionary{Color?, string}"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>The name of the color or the Hex-Code if no name is available</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = values.FirstOrDefault(x => x?.GetType() == typeof(Color)) as Color? ?? throw new ArgumentException("No valid Color found");
            Dictionary<Color?, string> colorNamesDictionary = values.FirstOrDefault(x => x?.GetType() == typeof(Dictionary<Color?, string>)) as Dictionary<Color?, string>;

            return ColorHelper.GetColorName(color, colorNamesDictionary);
        }


        /// <summary>
        /// Converts a given <see cref="string"/> back to a <see cref="Color"/>
        /// </summary>
        /// <param name="value">The name of the <see cref="Color"/></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Optional: A <see cref="Dictionary{Color?, string}"/></param>
        /// <param name="culture"></param>
        /// <returns><see cref="Color"/></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return ColorHelper.ColorFromString(text, parameter as Dictionary<Color?, string>) ?? Binding.DoNothing;
            }
            else
            {
                throw new InvalidCastException("Unable to convert the provided value to System.Windows.Media.Color");
            }
        }


        /// <summary>
        /// The ConvertBack-Method is not available inside a <see cref="MultiBinding"/>. Use a <see cref="Binding"/> with the optional <see cref="Binding.ConverterParameter"/> instead.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <throws><see cref="NotSupportedException"/></throws>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ??= new ColorToNameConverter();
        }
    }
}
