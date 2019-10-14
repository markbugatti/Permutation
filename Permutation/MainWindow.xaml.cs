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
        public double rowCount;
        public int columnCount;
        public int keyWordLength;
        public int OriginalTextLength;
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
            }
        }
    }
}
