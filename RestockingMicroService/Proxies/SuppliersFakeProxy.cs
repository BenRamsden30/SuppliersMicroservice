using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class SuppliersFakeProxy : SupplierInterface
    {
        List<Suppliers> suppliers;
        List<Products> products;

        public SuppliersFakeProxy()
        {
            suppliers = new List<Suppliers>
            {
                new Suppliers { SupplierID = 1, SupplierName = "Help", Webaddress = "abc.com" },
                new Suppliers { SupplierID = 2, SupplierName = "Me", Webaddress = "xyz.net" }
            };

            products = new List<Products>
            {
                new Products { Id = 1, BrandId = 1, BrandName = "Help", CategoryId = 1, CategoryName = "Help", Description = "This takes a long time", Ean = "Idk what this is", ExpectedRestock = DateTime.Now, InStock = true, Name = "Trainers", Price = 66.66 },
                new Products { Id = 2, BrandId = 2, BrandName = "Me", CategoryId = 2, CategoryName = "HMe", Description = "This is no longer fun", Ean = "Still don't know what this is", ExpectedRestock = DateTime.Now, InStock = false, Name = "Trainers", Price = 99.99 }
            };
        }
        public SuppliersFakeProxy(List<Suppliers> suppliers, List<Products> products)
        {
            this.suppliers = suppliers;
            this.products = products;
        }

        public Task<List<Suppliers>> GetSuppliers()
        {
            return Task.FromResult(suppliers);
        }

        public Task<Suppliers> GetSupplier(int Id)
        {
            return Task.FromResult(suppliers.Find(a => a.SupplierID == Id));
        }


        public Task<List<Products>> GetSupplierProducts(int Id)
        {
            return Task.FromResult(products.FindAll(a => a.Id == Id));
        }
    }

}
