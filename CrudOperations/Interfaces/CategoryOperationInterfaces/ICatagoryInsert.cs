using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICatagoryInsert
    {
        Task<bool> InsertCatagoryAsync(Catagory cat);
        Task<bool> InsertProductInCatagoryAsync(int productId, int CatagoryId);

    }
}
