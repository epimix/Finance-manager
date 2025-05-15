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

namespace TeamProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DownloadTransactions();


        }

        private void Add_Btn(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction
            {
                Amount = Convert.ToDecimal(UrlTextBox.Text),
                Date = DateTime.Now,
                type = TypeBox.Text,
                Note = NoteBox.Text,
                Category = CategoryBox.Text,
            };
            App.Db.Transactions.Add(transaction);
            App.Db.SaveChanges();
            DownloadsListBox.Items.Add(transaction);
        }
        private void DownloadTransactions()
        {
            var all = App.Db.Transactions.ToList();
            foreach (var t in all)
            {
                DownloadsListBox.Items.Add(t);
            }
        }
    }
}