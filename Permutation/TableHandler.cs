using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Permutation
{
    class TableHandler
    {
        private const int size = 40; // column and row size at the table
        public static TextBlock Find(Grid table,int row, int column)
        {
            foreach (Border border in table.Children)
            {
                int borderRow = (int)border.GetValue(Grid.RowProperty);
                int borderColumn = (int)border.GetValue(Grid.ColumnProperty);
                if (borderRow == row && borderColumn == column)
                {
                    TextBlock textBlock = (TextBlock)border.Child;
                    return textBlock;
                }
            }
            throw new Exception("can`t find TextBlock");
        }

        public static void AddColumns(int count)
        {
            //  Добавить в таблицу колонки
            for (int i = 0; i < count; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(size);
                MainWindow.OriginalTable.ColumnDefinitions.Add(column);
                column = new ColumnDefinition();
                column.Width = new GridLength(size);
                MainWindow.EncryptedTable.ColumnDefinitions.Add(column);
            }
        }

        public static void AddRows(int count)
        {
            for (int i = 0; i < count; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(size);
                MainWindow.OriginalTable.RowDefinitions.Add(row);
                row = new RowDefinition();
                row.Height = new GridLength(size);
                MainWindow.EncryptedTable.RowDefinitions.Add(row);
            }
        }

        public static void FillKeyWord(string keyWord, int columnCount)
        {
            string text; // представляет символ, который будет записан в клеточку
                         // первый и второй ряд - секретное слово и индекс
            for (int i = 0; i < columnCount; i++)
            {
                // Text
                text = keyWord[i].ToString();
                // Grid
                SquareCreator.create(MainWindow.OriginalTable, text, 0, i);

                // Text
                int num = (int)keyWord[i];
                text = num.ToString();
                // Grid
                SquareCreator.create(MainWindow.OriginalTable, text, 1, i);
            }
        }

        public static void FillText(string text, int rowCount, int columnCount)
        {
            // начиная со второго рядка заполнять таблицу символами исходного текста
            int index = 0;
            const int startletter = 97;
            int letter = startletter; // индекс для екстра символов. Для заполнение пустых клеточек
            string character;
            
            for (int i = 2; i < (int)rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    // Text 1.1
                    if (index < text.Length)
                    {
                        character = text[index++].ToString();
                    }
                    else if(i == rowCount-1 && j == columnCount-1)
                    {
                        int number = letter - startletter + 1; // считает сколько дополнительных символов было использовано. +1, куда записываеться сам результат 
                        character = number.ToString();         
                    }
                    else
                    {
                        character = ((char)letter).ToString(); 
                        letter++;
                    }
                    // Grid
                    SquareCreator.create(MainWindow.OriginalTable, character, i, j);
                }
            }
        }

        public static void CleanTable(Grid table)
        {
            table.RowDefinitions.Clear();
            table.ColumnDefinitions.Clear();
            table.Children.Clear();
        }

        public static void FillEncryptedTable(Grid table, int[] keyWordIndexes, int rowCount)
        {
            for (int i = 0; i < keyWordIndexes.Length; i++)
            {
                for (int j = 0; j < keyWordIndexes.Length; j++)
                {
                    TextBlock textBlock = Find(table, 1, j);
                    int number = int.Parse(textBlock.Text);
                    if(keyWordIndexes[i] == number)
                    {
                        for (int k = 0; k < rowCount; k++)
                        {
                            textBlock = Find(table ,k, j);
                            string text = textBlock.Text;
                            SquareCreator.create(MainWindow.EncryptedTable, text, k, i);
                        }
                    }
                }
            }
        }
    }
}
