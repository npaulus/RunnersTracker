using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.Business.Service.Impl
{
    public interface ILoginService
    {
        UserDTO Login(string email, string password);
        bool ResetPassword(string email);
        bool NewPassword(string password, string code);

    }
}
