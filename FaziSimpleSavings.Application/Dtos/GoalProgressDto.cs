using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaziSimpleSavings.Application.Dtos
{
    public class GoalProgressDto
    {
        public Guid GoalId { get; set; }
        public required string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public int ProgressPercentage { get; set; }
        public bool IsGoalAchieved { get; set; }
    }
}
