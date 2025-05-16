using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamProject.Entities;
using System.Windows.Interop;
using Microsoft.VisualBasic;

namespace TeamProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Transaction> AllTran;
        public MainWindow()
        {
            InitializeComponent();
            FilterComboBox.SelectedIndex = 0;
            AllTran = App.Db.Transactions.ToList();
            DownloadTransactions();
            FilterComboBox.SelectionChanged += FilterComboBox_SelectionChanged;

        }
        private void myTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterComboBox_SelectionChanged(null,null);
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var selectedCategory = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string searchTextBox = SearchTextBox.Text;
                DownloadsListBox.Items.Clear();

                if (selectedCategory == "All")
                {
                    if (string.IsNullOrWhiteSpace(searchTextBox))
                    {
                        foreach (var t in AllTran)
                            DownloadsListBox.Items.Add(t);
                    }
                    else
                    {
                        foreach (var t in AllTran)
                            if (t.Note.ToLower().Contains(searchTextBox))
                                DownloadsListBox.Items.Add(t);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(searchTextBox))
                    {
                        var filtered = AllTran.Where(t => t.Category == selectedCategory);
                        foreach (var t in filtered)
                            DownloadsListBox.Items.Add(t);
                    }
                    else
                    {
                        var filtered = AllTran.Where(t => t.Category == selectedCategory);
                        foreach (var t in filtered)
                            if (t.Note.ToLower().Contains(searchTextBox))
                                DownloadsListBox.Items.Add(t);
                    }
                }
            }
            catch (Exception ex) { }// тут виникає помилка яка не впливає на роботу програми, але якщо її не зловити вона не запуститься
        }

        private bool ValidateEmptyFields()
        {
            if (string.IsNullOrWhiteSpace(UrlTextBox.Text) || string.IsNullOrWhiteSpace(NoteBox.Text))
            {
                UrlTextBox.Text = "";
                return true;
            }
            if (string.IsNullOrWhiteSpace((CategoryBox.SelectedItem as ComboBoxItem)?.Content.ToString()) || string.IsNullOrWhiteSpace((TypeBox.SelectedItem as ComboBoxItem)?.Content.ToString()))
                return true;
           return false;
        }
        private bool CheckEnteredTypes()
        {
            if (UrlTextBox.Text.All(char.IsDigit))
                return false;
            return true;
        }
        private void Add_Btn(object sender, RoutedEventArgs e)
        {
            
            if (ValidateEmptyFields()) { MessageBox.Show("Заповни пусті поля!"); return; }
            if (CheckEnteredTypes()) { MessageBox.Show("Введи правильну ціну!"); return; }
            var transaction = new Transaction
            {
                Amount = Convert.ToDecimal(UrlTextBox.Text),
                Date = DateTime.Now,
                type = TypeBox.Text,
                Note = NoteBox.Text,
                Category = CategoryBox.Text,
            };

            UrlTextBox.Text = "";
            NoteBox.Text = "";
            App.Db.Transactions.Add(transaction);
            App.Db.SaveChanges();
            DownloadsListBox.Items.Add(transaction);

            AllTran = App.Db.Transactions.ToList();
            FilterComboBox_SelectionChanged(null, null);

            MessageBox.Show("Транзакція успішно додана!");
        }
        private void DownloadTransactions()
        {
            //var all = App.Db.Transactions.ToList();
            foreach (var t in AllTran)
            {
                DownloadsListBox.Items.Add(t);
            }
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearBtn(object sender, RoutedEventArgs e)
        {
            DownloadsListBox.Items.Clear();
        }


    }
}