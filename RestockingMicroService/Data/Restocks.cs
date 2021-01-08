using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Data
{
    public class Restocks
    {
        [Key]
        public int RestockId { get; set; }

        public string AccountName { get; set; }

        public int ProductID { get; set; }

        public int Gty { get; set; }

        public DateTime Date { get; set; }

        public string ProductName { get; set; }

        public string ProductEan { get; set; }

        public double TotalPrice { get; set; }

        public int SupplierID { get; set; }

        public bool Approved { get; set; }
    }
}
