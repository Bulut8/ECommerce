﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Electro.BOL.Entities;

namespace Electro.ViewModels
{
    public class Cart
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}