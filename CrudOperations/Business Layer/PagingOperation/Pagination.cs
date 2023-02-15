using CrudOperations.Interfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace CrudOperations.Business_Layer
{
    public class Pagination : IPagination
    {
        readonly ProductContex productdb;
        public Pagination(ProductContex productdb)
        {
            this.productdb = productdb;
        }
        public int TotalPage;
        public async Task<T> Paging<T>(int PagingNbr,string returntype)
        {
            var Parameters = new[]
            {
                new SqlParameter("@PagingNbr",PagingNbr),
                new SqlParameter("@TotalPages",SqlDbType.Int){Direction = ParameterDirection.Output}
            };

            if (returntype.StartsWith("List<Product>"))
            {
                var data = await productdb.Products.SqlQuery("execute spGetProductsPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

                TotalPage = (int)Parameters[1].Value;

                return (T)(object)data;

            }
            else
            {
                var data = await productdb.Catagories.SqlQuery("execute spGetCatagoryPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

                TotalPage = (int)Parameters[1].Value;

                return (T)(object)data;
            }
                      
        }

        public int TotalPages()
        {
            return TotalPage;
        }
    }

}