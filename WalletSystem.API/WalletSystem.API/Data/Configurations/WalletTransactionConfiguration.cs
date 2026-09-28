using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSystem.API.Models;

namespace WalletSystem.API.Data.Configurations;

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.ToTable("WalletTransactions", table =>
        {
            table.HasCheckConstraint("CK_WalletTransactions_Amount_Positive", "[Amount] > 0");
            table.HasCheckConstraint("CK_WalletTransactions_Type_Valid", "[Type] IN (1, 2)");
        });

        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Id)
            .ValueGeneratedOnAdd();

        builder.Property(transaction => transaction.WalletId)
            .IsRequired();

        builder.Property(transaction => transaction.Type)
            .IsRequired();

        builder.Property(transaction => transaction.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(transaction => transaction.BalanceAfter)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(transaction => transaction.Description)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(transaction => transaction.CreatedAt)
            .IsRequired();
    }
}
