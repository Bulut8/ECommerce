using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Electro.BOL.Entities;

namespace Electro.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Brand> Brands { get; set; }
    }
}