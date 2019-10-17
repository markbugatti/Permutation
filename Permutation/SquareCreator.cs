using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Permutation
{
    class SquareCreator
    {
        private const int UniformLength = 1; // Border Thickness
        static public void create(string text, int row, int column)
        {
            // Border
            Border border = new Border();
            border.BorderThickness = new Thickness(UniformLength);
            border.BorderBrush = new SolidColorBrush(Colors.DarkRed);
            // секретное слово
            TextBlock textBlock = new TextBlock();
            //Text
            textBlock.Text = text;
            // Grid
            border.SetValue(Grid.ColumnProperty, column);
            border.SetValue(Grid.RowProperty, row);
            // Font
            textBlock.FontSize = 14;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            // Insert
            border.Child = textBlock;
            MainWindow.OriginalTable.Children.Add(border);
        }
    }
}
