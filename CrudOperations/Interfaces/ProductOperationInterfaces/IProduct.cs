using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface IProduct
    {
        // Entity Frameworks

        //Task<List<Product>> GetProduct();
        //Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductAsync();

        Task<Product> GetProductByIdAsync(int id);


        


    }
}
