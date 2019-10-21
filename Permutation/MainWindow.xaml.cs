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
                TableHandler.CleanTable(OriginalTable);
                TableHandler.CleanTable(EncryptedTable);
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
                
                //  Добавить в таблицу колонки
                TableHandler.AddColumns(columnCount);
                // добавить в таблицу рядочки
                TableHandler.AddRows((int)rowCount);
                
                OriginalText = OriginalTextBox.Text;
                keyWord = KeyTextBox.Text;

                TableHandler.FillKeyWord(keyWord, columnCount);
                TableHandler.FillText(OriginalText, (int)rowCount, columnCount);
                
                //// put key word into original table
                //TableHandler.FillTable1(Grid1, keyWord, 0, 0, columnCount, 1);
                //// Convert key word to its indexes
                //for (int i = 0; i < keyWord.Length; i++)
                //{
                //    int index = (int)keyWord[i];
                //    string text = index.ToString();
                //    TableHandler.FillTable1(Grid1, text, 0, 1, columnCount, 2);
                //}
                //// put key word indexes into original table
                //TableHandler.FillTable1(Grid1, OriginalText, 0, 2, columnCount, (int)rowCount);

                //
                
            }
        }
    }
}
