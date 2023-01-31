using CrudOperations.Interfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.Insertion
{
    public class CategoryInsertion : ICategoryInsertion
    {
        readonly ProductContex productdb;
        public CategoryInsertion(ProductContex productdb)
        {
            this.productdb = productdb;
        }

        public async Task<bool> InsertCatagoryAsync(Catagory cat)
        {
            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spInsertCatagory", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@CatagoryName", cat.CatagoryName);
                command.Parameters.AddWithValue("@IsActive", cat.IsActive);


                int a = await command.ExecuteNonQueryAsync();
                if (a > 0) { return true; } else { return false; }
            }
        }

        public async Task<bool> InsertProductInCatagoryAsync(int productId, int CatagoryId)
        {

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spAddProduct", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@CatagoryId", CatagoryId);

                int a = await command.ExecuteNonQueryAsync();
                if (a > 0){return true;}else{return false;}
            }
        }

    }
}