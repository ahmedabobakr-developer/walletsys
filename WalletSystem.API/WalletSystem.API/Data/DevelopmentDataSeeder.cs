using Microsoft.EntityFrameworkCore;
using WalletSystem.API.Models;

namespace WalletSystem.API.Data;

public static class DevelopmentDataSeeder
{
    public static async Task SeedAsync(WalletDbContext dbContext)
    {
        if (await dbContext.Users.AnyAsync())
        {
            return;
        }

        var now = DateTime.UtcNow;
        dbContext.Users.Add(new User
        {
            Name = "Demo User",
            CreatedAt = now,
            Wallet = new Wallet
            {
                Balance = 0,
                CreatedAt = now,
                UpdatedAt = now
            }
        });

        await dbContext.SaveChangesAsync();
    }
}
