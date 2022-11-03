using CF.Account.API.Entities;
using CF.CustomMediator.Models;
using Microsoft.EntityFrameworkCore;

namespace CF.Account.API.Data.RelationalDatabase
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        public DbSet<Entities.Account> Accounts { get; set; }
        public DbSet<AccountManager> AccountManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            Entities.Account defaultAccount = new();
            defaultAccount.Id = 1;
            defaultAccount.MoneyAmount = 0m;

            modelBuilder.Entity<Entities.Account>().HasData(defaultAccount);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountDbContext).Assembly);
        }
    }
}
