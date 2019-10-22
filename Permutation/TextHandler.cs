using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Permutation
{
    class TextHandler
    {
        static private string MakeText(Grid table, int rowCount, int columnCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 2; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    TextBlock textBlock = TableHandler.Find(table, i, j);
                    stringBuilder.Append(textBlock.Text);
                }
            }
            return stringBuilder.ToString();
        }
        static public void OutEncryptedText(Grid table, TextBox textBox, int rowCount, int columnCount)
        {
            textBox.Text = MakeText(table, rowCount, columnCount);
        }

        static public void OutNormalText(Grid table, TextBox textBox, int rowCount, int columnCount)
        {
            string text = MakeText(table, rowCount, columnCount);
            int number;
            int.TryParse(string.Join("", text.Where(c => char.IsDigit(c))), out number);
            int startIndex = text.Length - number;
            text = text.Remove(startIndex);
            textBox.Text = text;
        }
    }
}
