using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Domain.Validator
{
    public class ValidatorException : Exception
    {
        public ValidatorException() { }

        public ValidatorException(string message) : base(message) { }

        public ValidatorException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
