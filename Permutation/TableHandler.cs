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
        public static TextBlock Find(int row, int column)
        {
            foreach (Border border in MainWindow.OriginalTable.Children)
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
            int indexExtra = 97; // индекс для екстра символов. Для заполнение пустых клеточек
            int counterExtra = 0; // Показывает сколько екстра символов было добавлено
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
                    else
                    {
                        char a = (char)indexExtra;
                        character = a.ToString();
                        indexExtra++;
                        counterExtra++;
                    }
                    // Grid
                    SquareCreator.create(MainWindow.OriginalTable, character, i, j);
                }
            }

            character = counterExtra.ToString();
            TextBlock textBlock = Find((int)rowCount - 1, columnCount - 1);
            textBlock.Text = "";
            SquareCreator.create(MainWindow.OriginalTable ,text, (int)rowCount - 1, columnCount - 1);
        }

        public static void FillTable1(Grid table,
                                        string text,
                                        int startColumn,
                                        int startRow,
                                        int endColumn,
                                        int endRow)
        {
            int index = 0;
            int textLength = text.Length;
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startColumn; j < endColumn; j++)
                {
                    if(index < textLength)
                        SquareCreator.create(table, text[index++].ToString(), i, j);
                }
            }
        }
    }
}
