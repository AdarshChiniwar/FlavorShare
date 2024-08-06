using FlavorShare.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FlavorShare.Converters
{
    public class IngredientConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var json = value as string;
            if (string.IsNullOrEmpty(json))
            {
                return string.Empty;
            }

            var ingredients = JsonConvert.DeserializeObject<List<ingredient>>(json);
            var sb = new StringBuilder();
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"name: {ingredient.name}");
                sb.AppendLine($"quantity: {ingredient.quantity}");
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StepConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var json = value as string;
            if (string.IsNullOrEmpty(json))
            {
                return string.Empty;
            }

            var steps = JsonConvert.DeserializeObject<List<instructionClass>>(json);
            var sb = new StringBuilder();
            foreach (var step in steps)
            {
                sb.AppendLine($"stepNumber: {step.stepNumber}");
                sb.AppendLine($"instruction: {step.instruction}");
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
