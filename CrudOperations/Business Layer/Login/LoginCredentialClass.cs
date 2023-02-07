using CrudOperations.Interfaces;
using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer
{
    public class LoginCredentialClass : ILogin
    {
        readonly ProductContex productdb;
        public LoginCredentialClass(ProductContex prodctdb)
        {
            productdb = prodctdb;
        }
        public async Task<int> GetIdByUsername(string username)
        {
            var data = await productdb.Logins.SingleAsync(model => model.UserName == username);

            return data.Id;
        }

        public async Task<bool> InsertSignDetailsAsync(SignUpClass Sign)
        {
            using (SqlConnection connection = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spInsertSignUp", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                command.Parameters.AddWithValue("@FirstName", Sign.FirstName);
                command.Parameters.AddWithValue("@LastName", Sign.LastName);
                command.Parameters.AddWithValue("@Age", Sign.Age);
                command.Parameters.AddWithValue("@Gender", Sign.Gender);
                command.Parameters.AddWithValue("@Email_Id", Sign.Email_Id);
                command.Parameters.AddWithValue("@UserName", Sign.UserName);
                command.Parameters.AddWithValue("@Password", Sign.Password);
                command.Parameters.AddWithValue("@Roles", Sign.Roles);

                int a = await command.ExecuteNonQueryAsync();
                if (a > 0) { return true; } else { return false; }
            }
        }

        public async Task<bool> LoginAsync(LoginClass login)
        {
            List<LoginClass> list = new List<LoginClass>();
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spLogin", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    LoginClass _login = new LoginClass();
                    _login.UserName = await sdr.GetFieldValueAsync<string>(0);
                    _login.Password = await sdr.GetFieldValueAsync<string>(1);

                    list.Add(_login);
                }

                if (list.Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<Logins> LoginEntAsync(LoginClass user)
        {
            var credentials = await productdb.Logins.FirstOrDefaultAsync(model => model.UserName == user.UserName && model.Password == user.Password);
            if (credentials == null)
            {
                return null;
            }
            else
            {
                return credentials;
            }
        }
    }
}