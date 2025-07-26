using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaziSimpleSavings.Application.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; }
    }
}
