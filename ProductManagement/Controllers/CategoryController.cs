using ProductManagement.DAL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagement.Controllers
{
    public class CategoryController : ApiController
    {
        private IGenericRepository<Tbl_Category> tblCategory;
        public CategoryController()
        {
            tblCategory = new GenericRepository<Tbl_Category>();
        }

        // GET: api/Category
        public IHttpActionResult GetAllCategory()
        {
            var category = tblCategory.GetAllData().ToList();
            return Ok(category);
        }

        // GET: api/Category/5
        public IHttpActionResult GetCategory(int id)
        {
            var category = tblCategory.GetDataById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Category
        public IHttpActionResult AddCategory([FromBody]Tbl_Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            category.CategoryId = 0;
            tblCategory.InsertData(category);
            tblCategory.SaveData();
            return CreatedAtRoute("DefaultApi", new
            {
                id = category.CategoryId
            }, category);
        }

        // PUT: api/Category/5
        public IHttpActionResult Put([FromBody]Tbl_Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (id != category.CategoryId)
            //{
            //    return BadRequest();
            //}
            tblCategory.UpdateData(category);

            tblCategory.SaveData();
            return Ok();
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var deleteCategory = tblCategory.GetDataById(id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            tblCategory.DeleteData(id);
            tblCategory.SaveData();
            return Ok(deleteCategory);
        }
    }
}
