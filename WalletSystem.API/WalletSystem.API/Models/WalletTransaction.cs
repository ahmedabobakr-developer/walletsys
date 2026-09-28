namespace WalletSystem.API.Models;

public class WalletTransaction
{
    public long Id { get; set; }

    public int WalletId { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfter { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public Wallet Wallet { get; set; } = null!;
}
