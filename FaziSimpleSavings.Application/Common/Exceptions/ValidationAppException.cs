using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaziSimpleSavings.Application.Common.Exceptions
{
    public class ValidationAppException : Exception
    {
        public List<string> Errors { get; }

        public ValidationAppException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        public ValidationAppException(List<string> errors)
            : this("Validation failed", errors) { }
    }
}
