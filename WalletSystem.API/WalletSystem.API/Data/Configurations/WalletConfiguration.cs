using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSystem.API.Models;

namespace WalletSystem.API.Data.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets", table =>
            table.HasCheckConstraint("CK_Wallets_Balance_NonNegative", "[Balance] >= 0"));

        builder.HasKey(wallet => wallet.Id);

        builder.Property(wallet => wallet.Id)
            .ValueGeneratedOnAdd();

        builder.Property(wallet => wallet.UserId)
            .IsRequired();

        builder.HasIndex(wallet => wallet.UserId)
            .IsUnique();

        builder.Property(wallet => wallet.Balance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(wallet => wallet.CreatedAt)
            .IsRequired();

        builder.Property(wallet => wallet.UpdatedAt)
            .IsRequired();

        builder.HasMany(wallet => wallet.WalletTransactions)
            .WithOne(transaction => transaction.Wallet)
            .HasForeignKey(transaction => transaction.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
