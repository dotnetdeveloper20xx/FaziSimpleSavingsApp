namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalTransactions
{
    public class GroupTransactionDto
    {
        public string UserFullName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
