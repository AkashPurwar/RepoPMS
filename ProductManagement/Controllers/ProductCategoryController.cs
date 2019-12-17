using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        [Route("ProductCategory/")]
        [Route("Categores/")]

        public ActionResult Index()
        {
            IEnumerable<Category> category = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Category");

            var consumeApi = hc.GetAsync("Category");
            consumeApi.Wait();
            var getData = consumeApi.Result;
            if (getData.IsSuccessStatusCode)
            {
                var displayData = getData.Content.ReadAsAsync<IList<Category>>();
                displayData.Wait();

                category = displayData.Result;
                
            }
            return View(category);
        }

     

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Category");

            var insertRecord = hc.PostAsJsonAsync<Category>("Category", category);
            insertRecord.Wait();

            var savedata = insertRecord.Result;

            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Category");

            var deleteRecord = hc.DeleteAsync("Category/" + id.ToString());
            deleteRecord.Wait();
            var savedata = deleteRecord.Result;

            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete");
        }

      
        
    }
}
