
namespace FaziSimpleSavings.Application.Dtos
{
    public class SavingsGoalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
