using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.Controls
{
    public class TextBoxWithPlaceholder : TextBox
    {
        static private string PlaceholderOldValue = "Введите фразу для поиска…";
        static private string PlaceholderDefault = "";
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public string SearchString
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextBoxWithPlaceholder), new PropertyMetadata(""));

        public static new readonly DependencyProperty TextProperty =
           DependencyProperty.Register("SearchString", typeof(string), typeof(TextBoxWithPlaceholder), new PropertyMetadata("", TextChanged));

        private static new void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.ToString().Length > 0)
                d.SetValue(PlaceholderProperty, PlaceholderDefault);
            if (e.NewValue.ToString().Length == 0)
                d.SetValue(PlaceholderProperty, PlaceholderOldValue);
        }
        public TextBoxWithPlaceholder()
        {
            Placeholder = PlaceholderOldValue;

        }
    }
}
