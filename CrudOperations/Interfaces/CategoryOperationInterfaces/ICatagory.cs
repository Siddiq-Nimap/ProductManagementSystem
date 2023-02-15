using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICatagory
    {
        Task<List<Catagory>> GetCatagoriesAsync();

        Task<List<Product>> GetProductsByCatagoryIdAsync(int id);

        Task<Catagory> GetCatagoryByIdAsync(int Id);

        Task<List<ReportDto>> GetReportAsync(int id);
        Task<IEnumerable<ReportDto>> GetReportAsync();

        Task<IEnumerable<Product>> GetNonAddedProduct(int id);

    }
}
