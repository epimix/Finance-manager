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
using System.Windows.Shapes;
using TeamProject.Entities;

namespace TeamProject.ChangeWind
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public Transaction TranToEdit { get; private set; }
        public EditWindow(Transaction tr)
        {
            InitializeComponent();
            TranToEdit = tr;

            AmountBox.Text = tr.Amount.ToString();
            NoteBox.Text = tr.Note;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (decimal.TryParse(AmountBox.Text, out var amount))
            {
                TranToEdit.Amount = amount;
                TranToEdit.Note = NoteBox.Text;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введи правильну ціну");
                AmountBox.Text = "";
            }

        }
    }
}
