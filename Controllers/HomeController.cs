using _25DH110604_LTW.Models;
using _25DH110604_LTW.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



namespace _25DH110604_LTW.Controllers
{
    public class HomeController : Controller
    {
        private MyStore2Entities db = new MyStore2Entities();
        public ActionResult Index(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            //var products = db.Products.AsQueryable();
            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    model.SearchTerm = searchTerm;
            //    products = products.Where(p => p.ProductName.Contains(searchTerm) ||
            //        p.ProductDescription.Contains(searchTerm) ||
            //        p.Category.CategoryName.Contains(searchTerm));                                 
            //}
            //int pageNumber = page ?? 1;
            //int pageSize = 6;
            //model.FeaturedProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(10).ToList();
            //model.NewProducts=products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);
            model.FeaturedProducts = new List<Product>();
            model.NewProducts = new List<Product>().ToPagedList(1, 6);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
        public ActionResult ProductDetails(int? id, int? quantity, int? page)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product pro = db.Products.Find(id);
            if (pro == null)
                return HttpNotFound();

            // Các sản phẩm cùng danh mục, trừ chính nó
            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID);

            var model = new ProductDetailsVM();
            int pageNumber = page ?? 1;
            int pageSize = model.PageSize;

            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToList();
            model.TopProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber, pageSize);

            if (quantity.HasValue && quantity.Value >= 1)
                model.quantity = quantity.Value;
            model.estimatedValue = model.quantity * pro.ProductPrice;   // slide chưa có dòng này

            return View(model);
        }
    }
}