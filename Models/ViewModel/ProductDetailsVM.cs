using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web;
using PagedList.Mvc;

namespace _25DH110604_LTW.Models.ViewModel
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public int quantity { get; set; } = 1;
        public decimal estimatedValue { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
        public List<Product> RelatedProducts { get; set; }
        public PagedList.IPagedList <Product> TopProducts { get; set; }
    }
}