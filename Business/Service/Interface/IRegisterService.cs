using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunnersTracker.Business.DTO;

namespace RunnersTracker.Business.Service.Impl
{
    public interface IRegisterService
    {
        bool createNewUser(UserDTO user);
        bool ConfirmUser(string code);
        bool ResendConfirmationLink(string email);

    }
}
