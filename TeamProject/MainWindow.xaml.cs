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
using TeamProject.ChangeWind;
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
        User user;
        public MainWindow()
        {



        }
        public MainWindow(string login, string Password)
        {
            InitializeComponent();
            //
            FilterComboBox.SelectedIndex = 0;
            AllTran = App.Db.Transactions.ToList();
            DownloadTransactions();
            FilterComboBox.SelectionChanged += FilterComboBox_SelectionChanged;
            //
            //переніс цей шматок для завантаження транзакцій при старту вікна
            var user = new User
            {
                Login = login,
                Password = Password
            };
            App.Db_user.Users.Add(user);
            App.Db_user.SaveChanges();


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
                var selectedPriceFilter = (FilterComboBoxByPrice.SelectedItem as ComboBoxItem)?.Content.ToString();
                string searchTextBox = SearchTextBox.Text.ToLower();
                DownloadsListBox.Items.Clear();

                IEnumerable<Transaction> filtered = AllTran;


                if (selectedCategory != "All")
                {
                    filtered = filtered.Where(t => t.Category == selectedCategory);
                }


                if (!string.IsNullOrWhiteSpace(searchTextBox))
                {
                    filtered = filtered.Where(t => t.Note.ToLower().Contains(searchTextBox));
                }


                switch (selectedPriceFilter)
                {
                    case "Latest":
                        filtered = filtered.OrderByDescending(t => t.Date);
                        break;
                    case "Oldest":
                        filtered = filtered.OrderBy(t => t.Date);
                        break;
                    case "Low":
                        filtered = filtered.OrderBy(t => t.Amount);
                        break;
                    case "High":
                        filtered = filtered.OrderByDescending(t => t.Amount);
                        break;
                }


                foreach (var t in filtered)
                    DownloadsListBox.Items.Add(t);
            }
            catch (Exception ex)
            {}
            // тут виникає помилка яка не впливає на роботу програми, але якщо її не зловити вона не запуститься
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

        private void ClearBtn(object sender, RoutedEventArgs e)
        {
            DownloadsListBox.Items.Clear();
        }

        private void ChengeBtn(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tr = button.DataContext as Transaction;

            if (tr != null)
            {
                var editWindow = new EditWindow(tr);
                if (editWindow.ShowDialog() == true)
                {
                }
            }
            FilterComboBox_SelectionChanged(null, null);

        }

        private void DeletedBtn(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            var item = btn.DataContext;

            var dltItem = item as Transaction;

            if (dltItem != null)
            {
                App.Db.Transactions.Remove(dltItem);
                AllTran.Remove(dltItem);
                DownloadsListBox.Items.Remove(dltItem);

                MessageBox.Show($"Видаляємо запис...");
            }
        }
    }
}