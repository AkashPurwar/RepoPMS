using ProductManagement.DAL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagement.Controllers
{
    public class ProductController : ApiController
    {
        private IGenericRepository<Tbl_Product> tblProduct;
        

        public ProductController()
        {
            tblProduct = new GenericRepository<Tbl_Product>();

        }

        // GET: api/Product
        public IHttpActionResult GetAllProduct()
        {
            var product = tblProduct.GetAllData().ToList();
            return Ok(product);
        }

   

        // GET: api/Product/5
        public IHttpActionResult GetProduct(int id)
        {
            var product = tblProduct.GetDataById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        public IHttpActionResult AddProduct([FromBody]Tbl_Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.ProductId =0;
            tblProduct.InsertData(product);
            tblProduct.SaveData();
            return CreatedAtRoute("DefaultApi", new
            {
                id = product.ProductId
            }, product);
        }

        // PUT: api/Product/5
        public IHttpActionResult Put( [FromBody]Tbl_Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != product.ProductId)
            //{
            //    return BadRequest();
            //}
            tblProduct.UpdateData(product);
          
                tblProduct.SaveData();
            return Ok();
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var deleteProduct = tblProduct.GetDataById(id);
            if (deleteProduct == null)
            {
                return NotFound();
            }
            tblProduct.DeleteData(id);
            tblProduct.SaveData();
            return Ok(deleteProduct);
        }



    }
}
