using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Electro.BOL.Entities;

namespace Electro.ViewModels
{
    public class IndexVM
    {
       
        public ICollection<Product> NewestProducts { get; set; }
        public ICollection<Product> BestSellerProducts { get; set; }
      
    }
}