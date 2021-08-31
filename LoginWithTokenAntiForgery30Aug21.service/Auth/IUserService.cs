using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithTokenAntiForgery30Aug21.service.Auth
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
