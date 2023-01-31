using CrudOperations.Interfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.Activation
{
    public class Activation : ICategoryActivation
    {
        readonly ProductContex productdb;
        public Activation(ProductContex productdb)
        {
            this.productdb = productdb;
        }
        public async Task<bool> ActivateAsync(int id)
        {

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand Command = new SqlCommand("spActivator", con);
                Command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                Command.Parameters.AddWithValue("@CatagoryId", id);

                int a = await Command.ExecuteNonQueryAsync();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }

        public async Task<bool> DeActivateAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand Command = new SqlCommand("spDeactivate", con);
                Command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                Command.Parameters.AddWithValue("@CatagoryId", id);

                int a = await Command.ExecuteNonQueryAsync();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
    }
}