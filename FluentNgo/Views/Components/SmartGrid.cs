using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FluentNgo.Views.Components
{
    public class SmartGrid : Grid
    {
        public int Rows
        {
            get { return (int)GetValue(RowsDependencyProperty); }
            set { SetValue(RowsDependencyProperty, value); }
        }
        public static readonly DependencyProperty RowsDependencyProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(SmartGrid), new PropertyMetadata(0));

        DependencyPropertyDescriptor RowsPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(RowsDependencyProperty, typeof(SmartGrid));

        public SmartGrid()
        {
            RowsPropertyDescriptor?.AddValueChanged(this, delegate
            {
                RowDefinitions.Clear();
                for (int i = 0; i < Rows; i++)
                    RowDefinitions.Add(new RowDefinition());
            });
        }
    }
}
