﻿using StoreApi.Entity._Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreApi.Entity._Image
{
    public class ImagePath
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        string _Image; 
        public string Image {
            get { return  _Image; } 
            set {_Image = "path: " + value; }
        }
    }
}

