using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaziSimpleSavings.Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "You are not authorized to perform this action.")
            : base(message)
        {
        }
    }
}
