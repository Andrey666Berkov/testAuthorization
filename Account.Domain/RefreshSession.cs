using Account.Domain.Models;

namespace Account.Domain;

public class RefreshSession
{
    public Guid Id { get; init; }
    public User User { get; init; } = null!;
    public Guid UserId { get; init; }
    public Guid Refresh { get; init; }
    public Guid JTi { get; init; }
   // public User User { get; init; }
    public DateTime DateBegin { get; init; }=DateTime.Now;
    public DateTime DateEnd { get; init; }=DateTime.UtcNow.AddDays(30);
}