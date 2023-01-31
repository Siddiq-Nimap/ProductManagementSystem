using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICategory
    {
        Task<List<Catagory>> GetCatagoriesAsync();

        Task<List<Product>> GetProductsByCatagoryIdAsync(int id);

        Task<Catagory> GetCatagoryByIdAsync(int Id);

        Task<List<ReportCLass>> GetReportAsync(int id);

    }
}
