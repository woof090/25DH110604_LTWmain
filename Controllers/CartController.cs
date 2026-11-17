using _25DH110604_LTW.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace _25DH110604_LTW.Controllers
{
    public class CartController : Controller
    {
        private MyStore2Entities db = new MyStore2Entities();
        private CartService GetCartService() { return new CartService(Session); }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                GetCartService().GetCart().AddItem(product.ProductID, product.ProductImage,
                    product.ProductName, product.ProductPrice, quantity, product.Category.CategoryName);
            }
            return RedirectToAction("Index");
        }

        // Xóa 1 sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            GetCartService().GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }

        // Làm trống giỏ
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            GetCartService().GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
        public ActionResult Index2(int? page)
        {
            var cart = GetCartService().GetCart();
            var products = db.Products.ToList();
            var similarProducts = new List<Product>();

            if (cart.Items != null && cart.Items.Any())
            {
                // Cùng danh mục với một món trong giỏ, nhưng chưa có trong giỏ
                similarProducts = products.Where(p =>
                    cart.Items.Any(ci => ci.Category == p.Category.CategoryName)
                    && !cart.Items.Any(ci => ci.ProductID == p.ProductID)).ToList();
            }

            int pageNumber = page ?? 1;
            int pageSize = cart.PageSize;
            cart.SimilarProducts = similarProducts.OrderBy(p => p.ProductID).ToPagedList(pageNumber, pageSize);
            return View(cart);
        }
    }
}
