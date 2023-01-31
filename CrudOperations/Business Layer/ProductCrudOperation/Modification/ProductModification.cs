using CrudOperations.Interfaces.ProductInterfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.ProductCrudOperation.Modification
{
    public class ProductModification : IProductModification
    {
        readonly ProductContex productdb;

        public ProductModification(ProductContex productdb)
        {
            this.productdb = productdb;
        }

        //public void EditProduct(Product pro)
        //{
        //    productdb.Entry(pro).State = EntityState.Modified;
        //}

        //public void DeleteProduct(Product pro)
        ////{
        ////    productdb.Entry(pro).State = EntityState.Deleted;
        ////}




        public async Task<bool> EditProductAsync(Product pro)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spUpdateProductById", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@Id", pro.Id);
                command.Parameters.AddWithValue("@ProductGenericName", pro.ProductGenericName);
                command.Parameters.AddWithValue("@ProductDescription", pro.ProductDescription);
                command.Parameters.AddWithValue("@ProductTitle", pro.ProductTitle);
                command.Parameters.AddWithValue("@ProductWeight", pro.ProductWeight);
                command.Parameters.AddWithValue("@ProductPrice", pro.ProductPrice);
                command.Parameters.AddWithValue("@ImagePath", pro.ImagePath);
                command.Parameters.AddWithValue("@UserId", pro.UserID);

                int a = await command.ExecuteNonQueryAsync();

                if (a > 0) { return true; } else { return false; }
            }
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spDeleteProductById", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@Id", id);

                int a = await command.ExecuteNonQueryAsync();

                if (a > 0) { return true; } else { return false; }
            }

        }
        public async Task<int> SaveAsync()
        {
            var data = await productdb.SaveChangesAsync();
            return data;
        }
    }
}