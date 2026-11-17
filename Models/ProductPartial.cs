using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _25DH110604_LTW
{
    public partial class Product
    {
        [NotMapped]
        public HttpPostedFileBase UploadImg { get; set; }
    }
}