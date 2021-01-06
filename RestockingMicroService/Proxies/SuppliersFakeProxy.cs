using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class SuppliersFakeProxy : SupplierInterface
    {
        List <Suppliers> suppliers;

        public SuppliersFakeProxy()
        {
            suppliers = new List<Suppliers>
            {
                new Suppliers { SupplierID = 1, SupplierName = "Help", URL = "abc.com" },
                new Suppliers { SupplierID = 2, SupplierName = "Me", URL = "xyz.net" }
            };
        }

        public Task<List<Suppliers>> GetSuppliers()
        {
            return Task.FromResult(suppliers);
        }

        public Task<Suppliers> GetSupplier(int Id)
        {
            return Task.FromResult(suppliers.Find(a => a.SupplierID == Id));
        }
    }
}
