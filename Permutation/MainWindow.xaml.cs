using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Permutation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int size = 40; // column and row size at the table
        private const int UniformLength = 1; // Border Thickness
        public double rowCount;
        public int columnCount;
        public int keyWordLength;
        public int OriginalTextLength;
        public string keyWord;
        public string OriginalText;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if(OriginalTextBox.Text != "" && KeyTextBox.Text != "")
            {
                keyWordLength = KeyTextBox.Text.Length;
                OriginalTextLength = OriginalTextBox.Text.Length;
                columnCount = keyWordLength;
                rowCount = OriginalTextLength / keyWordLength;
                // если количество рядков целое, то нужно добавить еще один ряд пустой ряд. Чтобы все буквы влезли в таблицу
                if(rowCount % 1 == 0)
                {
                    rowCount++;
                } 
                else // иначе округлить в большую сторону. Чтобы все буквы влезли в таблицу
                {
                    rowCount = Math.Ceiling(rowCount);
                }

                for (int i = 0; i < columnCount; i++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(size);
                    OriginalTable.ColumnDefinitions.Add(column);
                    column = new ColumnDefinition();
                    column.Width = new GridLength(size);
                    EncryptedTable.ColumnDefinitions.Add(column);
                }
                // к рядам надо добавить 2. Это будет +2 поля для ключевого слова и его идексов
                for (int i = 0; i < rowCount + 2; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(size);
                    OriginalTable.RowDefinitions.Add(row);
                    row = new RowDefinition();
                    row.Height = new GridLength(size);
                    EncryptedTable.RowDefinitions.Add(row);
                }
                OriginalText = OriginalTextBox.Text;
                keyWord = KeyTextBox.Text;
                
                // первый и второй ряд - секретное слово и индекс
                for (int i = 0; i < columnCount; i++)
                {
                    // Border
                    Border border = new Border();
                    border.BorderThickness = new Thickness(UniformLength);
                    border.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    // секретное слово
                    TextBlock textBlock = new TextBlock();
                    //Text
                    textBlock.Text = keyWord[i].ToString();
                    // Grid
                    border.SetValue(Grid.ColumnProperty, i);
                    // Font
                    textBlock.FontSize = 14;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    // Insert
                    border.Child = textBlock;
                    OriginalTable.Children.Add(border);

                    // индекс секретного слова
                    textBlock = new TextBlock();
                    // Text
                    int num = (int)keyWord[i];
                    textBlock.Text = num.ToString();
                    // Grid
                    textBlock.SetValue(Grid.RowProperty, 1);
                    textBlock.SetValue(Grid.ColumnProperty, i);
                    // Font
                    textBlock.FontSize = 14;
                    // Insert
                    OriginalTable.Children.Add(textBlock);
                }

                // начиная со второго рядка заполнять теблицу символами исходного текста
                int index = 0;
                for (int i = 2; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        TextBlock textBlock = new TextBlock();
                        // Text
                        textBlock.Text = OriginalText[index++].ToString();
                        // Grid
                        textBlock.SetValue(Grid.RowProperty, i);
                        textBlock.SetValue(Grid.ColumnProperty, j);
                        // Font
                        textBlock.FontSize = 14;
                        // Insert
                        OriginalTable.Children.Add(textBlock);
                    }
                }

                //OriginalTable.SetValue(Grid.ShowGridLinesProperty, true);
                EncryptedTable.SetValue(Grid.ShowGridLinesProperty, true);
            }
        }
    }
}
