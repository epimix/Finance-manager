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
        public static UserDbContext Db_user { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var options_finance = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseSqlite("Data Source=finance.db")
                .Options;
            var options_user = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlite("Data Source=user.db")
                .Options;

            Db = new FinanceDbContext(options_finance);
            Db_user = new UserDbContext(options_user);

            Db.Database.EnsureCreated();
            Db_user.Database.EnsureCreated();

            base.OnStartup(e);
        }
    }

}