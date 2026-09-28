namespace WalletSystem.API.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public Wallet? Wallet { get; set; }
}
