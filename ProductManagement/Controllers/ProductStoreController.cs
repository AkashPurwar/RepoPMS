using System;
using System.Collections.Generic;
using System.Linq;
using ProductManagement.Models;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;

namespace ProductManagement.Controllers
{
    public class ProductStoreController : Controller
    {

        public ActionResult Index(string categoryId)
        {

            IEnumerable<ProductData> products = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Product");

            var consumeApi = hc.GetAsync("Product");
            consumeApi.Wait();
            var getData = consumeApi.Result;

            if (getData.IsSuccessStatusCode)
            {
                var displayData = getData.Content.ReadAsAsync<IList<ProductData>>();
                displayData.Wait();
                if (categoryId == null || categoryId == "")
                    products = displayData.Result;
                else
                    products = displayData.Result.Where(c => c.CategoryId == Convert.ToInt32(categoryId));
            }
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductData product, FormCollection form)
        {
            product.CategoryId = Convert.ToInt32(Request.Form["hdnCate"]);

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Product");

            var insertRecord = hc.PostAsJsonAsync<ProductData>("Product", product);
            insertRecord.Wait();

            var savedata = insertRecord.Result;

            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");

        }

        public ActionResult Edit(int? id)
        {
            ProductData product = new ProductData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Product");

            var consumeApi = hc.GetAsync("Product/" + id.ToString());
            consumeApi.Wait();
            var getData = consumeApi.Result;

            if (getData.IsSuccessStatusCode)
            {
                var displayData = getData.Content.ReadAsAsync<ProductData>();
                displayData.Wait();
                if (displayData == null )
                    return HttpNotFound();
                else
                    product = displayData.Result;
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductData product)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Product");

            var updateRecord = hc.PutAsJsonAsync<ProductData>("Product", product);
            updateRecord.Wait();
            var savedata = updateRecord.Result;

            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44398/api/Product");

            var deleteRecord = hc.DeleteAsync("Product/" + id.ToString());
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