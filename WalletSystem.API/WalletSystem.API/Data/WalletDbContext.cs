using Microsoft.EntityFrameworkCore;
using WalletSystem.API.Models;

namespace WalletSystem.API.Data;

public class WalletDbContext(DbContextOptions<WalletDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Wallet> Wallets => Set<Wallet>();

    public DbSet<WalletTransaction> WalletTransactions => Set<WalletTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
