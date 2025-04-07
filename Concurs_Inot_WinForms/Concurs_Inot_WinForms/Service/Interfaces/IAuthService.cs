using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurs_Inot_WinForms.Service.Interfaces
{
    public interface IAuthService
    {
        bool authentificate(String username, String password);
    }
}
