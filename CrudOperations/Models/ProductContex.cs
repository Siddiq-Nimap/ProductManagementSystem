using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;  
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using CrudOperations.ViewModels;

namespace CrudOperations.Models
{
    public class ProductContex : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<CatagoryList> CatagoryLists { get; set; }

        public DbSet<Login> Logins { get; set; }


       public string cs = ConfigurationManager.ConnectionStrings["ProductContex"].ConnectionString;
      
      
    }
}
    

