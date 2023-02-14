using CrudOperations.CustomFilters;
using CrudOperations.Interfaces;
using CrudOperations.Interfaces.ProductInterfaces;
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
   
    public class ProductController : Controller
    {
        readonly IPaging page;
        readonly IFileSaving file;
        readonly IProduct products;
        readonly IProductInsertion ProductInsert;
        readonly IProductModification ProductModify;
        readonly ILogin logins;
        public ProductController(IPaging page,
            IFileSaving file,
            IProduct products,
            ILogin logins,
            IProductInsertion insert,
            IProductModification modify)
        {
            this.page = page;
            this.file = file;
            this.products = products;
            this.logins = logins;
            this.ProductInsert = insert;
            this.ProductModify = modify;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int PagingNbr = 1)
        {
            var data = await page.Paging<List<Product>>(PagingNbr, "List<Product>");         

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = page.TotalPages();

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity.Claims;

            var IdentifierName = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string name = IdentifierName.Value;

            var data = await logins.GetIdByUsername(name);

            Session["userid"] = data;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync(Product fruits)
        {
            string data = file.FileUpload(fruits);
            if (data.StartsWith("~/Images/"))
            {
                fruits.ImagePath = data;

                bool Check = await ProductInsert.InsertProductAsync(fruits);

                if (Check == true){return Json("Your Data has been inserted successfully");}
                else{return Json("Your Data has not inserted");}
            }
            else
            {
                return Json(data);
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(int Id)
        {
            var data = await products.GetProductByIdAsync(Id);
            Session["Image"] = data.ImagePath;
            return View(data);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(Product fruits)
        {
            if (fruits.ImageFile != null)
            {
                string data = file.FileUpload(fruits);

                if (data.StartsWith("~/Images/"))
                {
                    fruits.ImagePath = data;

                    bool check = await ProductModify.EditProductAsync(fruits);

                    if (check == true)
                    { 
                        file.FileDelete(Session["Image"].ToString());
                        return Json("Your Data has been successfully Edited");
                    }
                    else{ return Json("Your Data has not Edited"); }
                }
                else{ return Json(data); }
            }
            else
            {
                fruits.ImagePath = Session["Image"].ToString();
                bool check = await ProductModify.EditProductAsync(fruits);

                if (check == true){return Json("Your Data has been updated successfully");}
                else{ return Json("Your Data has not Edited");}
            }
            
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var data = await products.GetProductByIdAsync(id);
            TempData["Images"] = data.ImagePath;
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(Product pro)
        {
            var data = await ProductModify.DeleteProductAsync(pro.Id);
            if (data == true)
            {
                file.FileDelete(TempData["Images"].ToString());
                return RedirectToAction("Index", "Product");
            }
            else{return RedirectToAction("Delete","Product",new {id = pro.Id});}
        }

        [HttpGet]
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(int Id)
        {
            var data = await products.GetProductByIdAsync(Id);

            if (data == null){return RedirectToAction("Index", "Catagory");}
            else{return View(data);}
        }
        
        public ActionResult Pages()
        {
            return View();
        }
    }
}