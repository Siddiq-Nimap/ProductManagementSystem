using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces.ProductInterfaces
{
    public interface IProductModify
    {
        //void DeleteProduct(Product pro);
        //void EditProduct(Product pro);
        Task<bool> EditProductAsync(Product pro);

        Task<bool> DeleteProductAsync(int id);

        Task<int> SaveAsync();
    }
}
