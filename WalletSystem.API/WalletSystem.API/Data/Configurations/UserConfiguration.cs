using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSystem.API.Models;

namespace WalletSystem.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedOnAdd();

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.CreatedAt)
            .IsRequired();

        builder.HasOne(user => user.Wallet)
            .WithOne(wallet => wallet.User)
            .HasForeignKey<Wallet>(wallet => wallet.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
