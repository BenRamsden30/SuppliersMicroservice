using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Data
{
    public class Suppliers
    {
        [Key]
        public int SupplierID { get; set; }

        public string SupplierName { get; set; }

        public string Webaddress { get; set; }
    }
}
