using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetGroupGoalDetails
{
    public class GroupGoalDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal TargetAmount { get; set; }
        public decimal TotalSaved { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; } = null!;
        public List<GroupGoalMemberDto> Members { get; set; } = new();
    }

    public class GroupGoalMemberDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public decimal ContributedAmount { get; set; }
    }
}
