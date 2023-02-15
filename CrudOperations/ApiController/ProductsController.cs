using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using CrudOperations.Business_Layer.ProductCrudOperation;
using CrudOperations.Business_Layer.ProductCrudOperation.Insertion;
using CrudOperations.Business_Layer.ProductCrudOperation.Modification;
using CrudOperations.Interfaces;
using CrudOperations.Interfaces.ProductInterfaces;
using CrudOperations.Models;

namespace CrudOperations.Controllers
{
    public class ProductsController : ApiController 
    {
        readonly IProduct product;
        readonly IProductInsert productInsert;
        readonly IProductModify productModify;
        public ProductsController(IProduct product,IProductInsert productInsert,IProductModify productmodify)
        {
            this.product = product;
            this.productInsert = productInsert;
            productModify = productmodify;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<IHttpActionResult> GetProducts()
        {
            var data = await product.GetProductAsync();

            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();

            var jsonstr = scriptSerializer.Serialize(data);
            
            return Ok(jsonstr);
        }

        // GET: api/Products/5
        [HttpGet]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var products = await product.GetProductByIdAsync(id);

            if (product == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

        // PUT: api/Products/5
        [HttpPut]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}

            if (id != product.Id){return BadRequest();}

            try
            {
                await productModify.EditProductAsync(product);
            }

            catch (Exception E){return BadRequest(E.Message);}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product products)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}

           var check = await productInsert.InsertProductAsync(products);
            if (check == true)
            {
                //return Created(new Uri(Request.RequestUri + "/" + new { id = products.Id }), products);
                return CreatedAtRoute("DefaultApi", new { id = products.Id }, products);
            }
            else{return BadRequest(ModelState);}
        }

        // DELETE: api/Products/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var products =await product.GetProductByIdAsync(id);
            if (products == null)
            {
                return NotFound();
            }
             var check =await productModify.DeleteProductAsync(id);
            if(check == true) { return Ok(products); } else { return BadRequest(); }

            
        }


    }
}