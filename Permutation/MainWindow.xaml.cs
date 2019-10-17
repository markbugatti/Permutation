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
        private const int UniformLength = 1; // Border Thickness
        private const int size = 40; // column and row size at the table
        // глобальные статичиские перменные, хранят ссылки на таблицы из XAML
        static public Grid OriginalTable { get; private set; }
        static public Grid EncryptedTable { get; private set; }
        public double rowCount;
        public int columnCount;
        public int keyWordLength;
        public int OriginalTextLength;
        public string keyWord;
        public string OriginalText;
        public MainWindow()
        {
            InitializeComponent();
            OriginalTable = Grid1;
            EncryptedTable = Grid2;
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
                // Добавляем 2 рядка для секретного слова и индекса
                rowCount += 2;

                for (int i = 0; i < columnCount; i++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(size);
                    OriginalTable.ColumnDefinitions.Add(column);
                    column = new ColumnDefinition();
                    column.Width = new GridLength(size);
                    EncryptedTable.ColumnDefinitions.Add(column);
                }
                
                for (int i = 0; i < rowCount; i++)
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

                string text; // представляет символ, который будет записан в клеточку
                // первый и второй ряд - секретное слово и индекс
                for (int i = 0; i < columnCount; i++)
                {
                    // Text
                    text = keyWord[i].ToString();
                    // Grid
                    SquareCreator.create(text, 0, i);
                    
                    // Text
                    int num = (int)keyWord[i];
                    text = num.ToString();
                    // Grid
                    SquareCreator.create(text, 1, i);
                }

                // начиная со второго рядка заполнять теблицу символами исходного текста
                int index = 0;
                int indexExtra = 97; // индекс для екстра символов. Для заполнение пустых клеточек
                int counterExtra = 0; // Показывает сколько екстра символов было добавлено
                
                for (int i = 2; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        // Text 1.1
                        if (index < OriginalText.Length) {
                            text = OriginalText[index++].ToString();
                        }
                        else
                        {
                            char symbol = (char)indexExtra;
                            text = symbol.ToString();
                            indexExtra++;
                            counterExtra++;
                        }
                        // Grid
                        SquareCreator.create(text, i, j);
                        
                    }
                }
                text = counterExtra.ToString();
                SquareCreator.create(text, (int)rowCount - 1, columnCount - 1);
                //OriginalTable.SetValue(Grid.ShowGridLinesProperty, true);
                //EncryptedTable.SetValue(Grid.ShowGridLinesProperty, true);
            }
        }
    }
}
