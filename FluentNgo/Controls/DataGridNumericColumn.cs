using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System;
using FluentNgo.Core;

namespace FluentNgo.Controls
{
    public class DataGridNumericColumn : DataGridTemplateColumn
    {
        public string BindingPath { get; set; }

        public int MaxValue { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBox textBox = new TextBox();
            Binding binding = new Binding(BindingPath) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, TargetNullValue = "", Converter = new NumericTextConverter()};
            textBox.SetBinding(TextBox.TextProperty, binding);
            textBox.PreviewTextInput += TB_PreviewTextInput;
            textBox.LostFocus += TB_LostFocus;
            return textBox;
        }

        private void TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9Aa.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int value = int.Parse(((TextBox)sender).Text);
                if (value > MaxValue) {
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
