namespace Application.Transactions.Queries.GetTransactionsByGoal;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}
