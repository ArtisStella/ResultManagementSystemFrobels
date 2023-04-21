using FluentNgo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

namespace FluentNgo.Controls
{
    class NumericColumnGenerator
    {
        public int MaxValue { get; set; }

        public DataGridTemplateColumn GenerateNumericColumn(string header, string BindingPath, int maxValue)
        {
            MaxValue = maxValue;

            var column = new DataGridTemplateColumn();
            
            column.Header = header;
            column.CanUserResize = false;
            column.Width = DataGridLength.Auto;

            var template = new DataTemplate();
            var textBoxFactory = new FrameworkElementFactory(typeof(TextBox));
            textBoxFactory.SetBinding(TextBox.TextProperty, new Binding(BindingPath) { Mode = BindingMode.TwoWay, TargetNullValue = "", UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Converter = new NumericTextConverter() });
            textBoxFactory.AddHandler(TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(TB_PreviewTextInput));
            textBoxFactory.AddHandler(TextBox.LostFocusEvent, new RoutedEventHandler(TB_LostFocus));

            template.VisualTree = textBoxFactory;
            column.CellTemplate = template;

            return column;
        }

        private void TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int value = int.Parse(((TextBox)sender).Text);
                if (value > MaxValue)
                {
                    ((TextBox)sender).Text = MaxValue.ToString();
                }
            }
            catch
            {
                ((TextBox)sender).Text = "0";
            }
        }

    }
}
