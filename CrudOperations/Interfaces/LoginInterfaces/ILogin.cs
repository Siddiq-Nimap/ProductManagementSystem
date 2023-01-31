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

        Task<bool> InsertSignDetailsAsync(SignUpClass Sign);
        Task<bool> LoginAsync(LoginClass login);

    }
}
