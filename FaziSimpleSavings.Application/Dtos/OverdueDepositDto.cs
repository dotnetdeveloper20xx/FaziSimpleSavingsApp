using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaziSimpleSavings.Application.Dtos
{
    public class OverdueDepositDto
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; }
        public Guid GoalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextDueDate { get; set; }
        public string GoalName { get; set; } = default!;
    }
}
