using CrudOperations.Interfaces.ProductInterfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.ProductCrudOperation.Insertion
{
    public class ProductInsert : IProductInsert
    {
        readonly ProductContex productdb;
        public ProductInsert(ProductContex productdb)
        {
            this.productdb = productdb;
        }
    
        //public bool InsertProduct(Product pro)
        //{
        //    var data = productdb.Products.Add(pro);
        //    if (data == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        
        public async Task<bool> InsertProductAsync(Product pro)
        {
            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spAddProducts", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@ProductGenericName", pro.ProductGenericName);
                command.Parameters.AddWithValue("@ProductDescription", pro.ProductDescription);
                command.Parameters.AddWithValue("@ProductTitle", pro.ProductTitle);
                command.Parameters.AddWithValue("@ProductWeight", pro.ProductWeight);
                command.Parameters.AddWithValue("@ProductPrice", pro.ProductPrice);
                command.Parameters.AddWithValue("@ImagePath", pro.ImagePath);
                command.Parameters.AddWithValue("@userId", pro.UserID);

                int a = await command.ExecuteNonQueryAsync();
                if(a > 0) { return true; } else { return false; }
            }

        }

        public async Task<int> SaveAsync()
        {
            var data = await productdb.SaveChangesAsync();
            return data;
        }
    }
}