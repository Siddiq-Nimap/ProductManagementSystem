using CrudOperations.Interfaces;
using CrudOperations.Models;
using CrudOperations.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.CategoryCrudOperation
{
    public class Categorys : ICatagory
    {
        readonly ProductContex productdb;
        public Categorys(ProductContex productdb)
        {
            this.productdb = productdb;
        }
        public async Task<List<Catagory>> GetCatagoriesAsync()
        {

            List<Catagory> list = new List<Catagory>();

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("GetCatagory", con);
                command.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Catagory catagory = new Catagory();
                    catagory.Id = await dr.GetFieldValueAsync<int>(0);

                    catagory.CatagoryName = await dr.GetFieldValueAsync<string>(1);
                    catagory.IsActive = await dr.GetFieldValueAsync<bool>(2);

                    list.Add(catagory);
                }
            }
            return list;
        }

        public async Task<Catagory> GetCatagoryByIdAsync(int Id)
        {
            Catagory catagory = new Catagory();
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spGetCatagoryById", con);

                command.CommandType = CommandType.StoredProcedure;
                con.Open();

                command.Parameters.AddWithValue("@Id", Id);
                SqlDataReader sdr = await command.ExecuteReaderAsync();

                while (await sdr.ReadAsync())
                {
                    catagory.Id = await sdr.GetFieldValueAsync<int>(0);
                    catagory.CatagoryName = await sdr.GetFieldValueAsync<string>(1);
                    catagory.IsActive = await sdr.GetFieldValueAsync<bool>(2);


                }

            }
            return catagory;
        }

        public async Task<IEnumerable<Product>> GetNonAddedProduct(int id)
        {
            List<Product> productlists = new List<Product>();

            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {

                SqlCommand command = new SqlCommand("spAddTocata", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@CatagoryId", id);

                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    Product product = new Product();
                    product.Id = await sdr.GetFieldValueAsync<int>(0);

                    product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);

                    product.ProductTitle = await sdr.GetFieldValueAsync<string>(2);

                    product.ProductPrice = await sdr.GetFieldValueAsync<int>(3);

                    product.ImagePath = await sdr.GetFieldValueAsync<string>(4);

                    productlists.Add(product);
                }
            }
            return productlists;
        }

        public async Task<List<Product>> GetProductsByCatagoryIdAsync(int id)
        {
            List<Product> productlists = new List<Product>();

            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {

                SqlCommand command = new SqlCommand("spGetCatagoriesProduct", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@CatagoryId", id);

                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    Product product = new Product();
                    product.Id = await sdr.GetFieldValueAsync<int>(0);

                    product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);

                    product.ProductTitle = await sdr.GetFieldValueAsync<string>(2);

                    product.ProductPrice = await sdr.GetFieldValueAsync<int>(3);

                    product.ImagePath = await sdr.GetFieldValueAsync<string>(4);

                    productlists.Add(product);
                }
            }
            return productlists;
        }

        public async Task<List<ReportDto>> GetReportAsync(int id)
        {
            var param = new SqlParameter("@LoginId", id);

            var data = await productdb.Database.SqlQuery<ReportDto>("execute spReport @LoginId", param).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            var data = await productdb.Database.SqlQuery<ReportDto>("execute sp_Reportall").ToListAsync();
            return data;

        }


    }
}