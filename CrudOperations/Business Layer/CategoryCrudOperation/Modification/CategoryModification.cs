using CrudOperations.Interfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.Modification
{
    public class CategoryModification : ICategoryModification
    {
        readonly ProductContex productdb;
        public CategoryModification(ProductContex productdb)
        {
            this.productdb = productdb;
        }

        public async Task<bool> DeleteCatagoryAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spDeleteCatagoryById", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@Id", id);

                int a = await command.ExecuteNonQueryAsync();
                if (a > 0) { return true; } else { return false; }
            }
        }

        public async Task<bool> EditCatagoryAsync(Catagory cat)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spUpdateCatagory", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@Id", cat.Id);
                command.Parameters.AddWithValue("@CatagoryName", cat.CatagoryName);
                command.Parameters.AddWithValue("@IsActive", cat.IsActive);

                int a = await command.ExecuteNonQueryAsync();
                if (a > 0) { return true; } else { return false; }
            }
        }
    }
}