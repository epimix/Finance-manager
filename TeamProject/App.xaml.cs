using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;
using TeamProject.Database;

namespace TeamProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FinanceDbContext Db { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseSqlite("Data Source=finance.db")
                .Options;

            Db = new FinanceDbContext(options);

            Db.Database.EnsureCreated();

            base.OnStartup(e);
        }
    }

}