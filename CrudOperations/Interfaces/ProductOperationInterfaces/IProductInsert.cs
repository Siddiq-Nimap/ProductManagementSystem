using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces.ProductInterfaces
{
    public interface IProductInsert
    {
        //bool InsertProduct(Product pro);
        Task<bool> InsertProductAsync(Product product);

        Task<int> SaveAsync();
    }
}
