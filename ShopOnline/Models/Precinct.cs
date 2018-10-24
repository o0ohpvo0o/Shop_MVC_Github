using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class Precinct
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ProvinceID { get; set; }
        public string DistrictID { get; set; }
    }
}