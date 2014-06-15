using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Entities;

namespace BussinessLogic.AuthorizationLogic
{
    public interface IMembershipProvider
    {
        UserEntity GetLoggedUser();
    }
}
