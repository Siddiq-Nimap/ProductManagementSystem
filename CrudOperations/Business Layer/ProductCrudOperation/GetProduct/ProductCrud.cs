using CrudOperations.Interfaces;
using CrudOperations.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudOperations.Business_Layer.ProductCrudOperation
{
    public class ProductCrud : IProduct
    {
       readonly ProductContex productdb;
        public ProductCrud(ProductContex productdb)
        {
            this.productdb = productdb;
        }

        //public async Task<List<Product>> GetProduct()
        //{
        //    return await productdb.Products.ToListAsync();
        //}

        //public async Task<Product> GetProductById(int id)
        //{
        //    var data = await productdb.Products.FirstOrDefaultAsync(model => model.Id == id);
        //    return data;
        //}

        public async Task<List<Product>> GetProductAsync()
        {
            List<Product> productlist = new List<Product>();

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spReadProducts", con);
                command.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (await dr.ReadAsync())
                {
                    Product product = new Product();
                    product.Id = await dr.GetFieldValueAsync<int>(0);

                    product.ProductGenericName = await dr.GetFieldValueAsync<string>(1);
                    product.ProductDescription = await dr.GetFieldValueAsync<string>(2);
                    product.ProductTitle = await dr.GetFieldValueAsync<string>(3);
                    product.ProductWeight = await dr.GetFieldValueAsync<int>(4);
                    product.ProductPrice = await dr.GetFieldValueAsync<int>(5);
                    product.ImagePath = await dr.GetFieldValueAsync<string>(6);

                    productlist.Add(product);
                }
            }
            return productlist;
        }
        
        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spGetProductById", con);

                command.CommandType = CommandType.StoredProcedure;
                con.Open();

                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader sdr = await command.ExecuteReaderAsync();

                while (sdr.Read())
                {
                    product.Id = await sdr.GetFieldValueAsync<int>(0);
                    product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);
                    product.ProductDescription = await sdr.GetFieldValueAsync<string>(2);
                    product.ProductTitle = await sdr.GetFieldValueAsync<string>(3);
                    product.ProductWeight = await sdr.GetFieldValueAsync<int>(4);
                    product.ProductPrice = await sdr.GetFieldValueAsync<int>(5);
                    product.ImagePath = await sdr.GetFieldValueAsync<string>(6);
                    product.UserID = await sdr.GetFieldValueAsync<int>(7);
                }
            }
            return product;
        }

    }

 }