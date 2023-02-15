﻿using CrudOperations.CustomFilters;
using CrudOperations.Interfaces;
using CrudOperations.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CrudOperations.Controllers
{

    [Authorize]
    [CustomFilterClass]
    public class CatagoryController : Controller
    {
        readonly IPagination page;
        //readonly ProductContex productdb;
        readonly ICredential logins;
        readonly ICatagory category;
        readonly ICatagoryActivate CategoryActivate;
        readonly ICatagoryInsert CategoryInsert;
        readonly ICatagoryModify CategoryModify;
        readonly IProduct products;
        public CatagoryController(IPagination page,
            ICredential logins,
            ICatagory category, IProduct product,
            ICatagoryInsert insert,
            ICatagoryActivate activate,
            ICatagoryModify modify)
        {
            this.page = page;
            this.products = product;
            this.logins = logins;
            this.category = category;
            this.CategoryActivate = activate;
            this.CategoryInsert = insert;
            this.CategoryModify = modify;
        }

        [HttpGet]
        public ActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CatagoryList(int PagingNbr = 1)
        {
            var data = await page.Paging<List<Catagory>>(PagingNbr,"List<Catagory>");

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = page.TotalPages();

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Report()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity.Claims;

            var IdentifierName = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string name = IdentifierName.Value;
            var id = await logins.GetIdByUsername(name);

            var data = await category.GetReportAsync(id);
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ReportALL()
        {
            var data = await category.GetReportAsync();
            if(data != null) { return View(data); } else { return RedirectToAction("CatagoryList", "Catagory"); }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Create")]
      public async Task<ActionResult> CreateAsync(Catagory cat)
        {
            //bool data = await productdb.InsertCatagoryAsync(cat);
            bool data = await CategoryInsert.InsertCatagoryAsync(cat);
            if (data == true)
            {
                return Json("Your data has successfully inserted");
            }
            else
            {
                ViewBag.Create = "Your Data has not inserted";
                return View();
            }
        }

        [HttpGet]
        [ActionName("ShowProduct")]
        public async Task<ActionResult> ShowProductAsync(int id)
        {
            var data = await category.GetProductsByCatagoryIdAsync(id);
            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("AddProduct")]
        public async Task<ActionResult> AddProductAsync(int id)
        {
            TempData["id"] = id;
            var data = await category.GetNonAddedProduct(id);
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("AddProduct")]
        public async Task<ActionResult> AddProductAsync(int Prodid, int Cataid)
        {
           bool data =   await CategoryInsert.InsertProductInCatagoryAsync(Prodid, Cataid);
            
            if (data == true)
            {
                return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
            else
            {
             ViewBag.ProductInsertCatagory = "Your Produc is not inserted in the catagory";
             return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
        }

        [Authorize(Roles = "Admin")]
        [ActionName("DeActivate")]
        public async Task<ActionResult> DeActivateAsync(int id)
        {
            await CategoryActivate.DeActivateAsync(id);

            return RedirectToAction("CatagoryList","Catagory");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Activate")]
        public async Task<ActionResult> ActivateAsync(int id)
        {
            await CategoryActivate.ActivateAsync(id);
            return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(int Id)
        {
            var data = await category.GetCatagoryByIdAsync(Id);
            return View(data);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(Catagory cat)
        {
            bool data = await CategoryModify.EditCatagoryAsync(cat);
            if (data == true)
            {
                return RedirectToAction("CatagoryList", "Catagory");
            }
            else
            {
                ViewBag.EditMessage = "Your data has not been edited";
                return View();
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = await category.GetCatagoryByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(Catagory catagory)
        {
                await CategoryModify.DeleteCatagoryAsync(catagory.Id);
                return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var data = await category.GetCatagoryByIdAsync(id);
            return View(data);
        }
    }
}