using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface IPaging
    {
        Task<T> Paging<T>(int PaginNbr,string returntype);

        int TotalPages();
    }
}
