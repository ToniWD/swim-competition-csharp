using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Domain.Validator
{
    public interface IValidator<E>
    {
        void Validate(E entity);
    }
}
