using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ILogin
    {
        Task<int> GetIdByUsername(string username);

        Task<bool> InsertSignDetailsAsync(SignUpDto Sign);
        Task<bool> LoginAsync(LoginDto login);

        Task<Logins> LoginEntAsync(LoginDto user);

    }
}
