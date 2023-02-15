using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface IAuthenticationManager
    {
        string GenerateToken(Logins user);

        ClaimsPrincipal ValidationToken(string token);

    }
}
