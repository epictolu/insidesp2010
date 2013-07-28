using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductConnector.ProductModel
{
 
    public partial class Product
    {
        public string ID { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public byte[] SecurityDescriptor { get; set; }
    }
}
