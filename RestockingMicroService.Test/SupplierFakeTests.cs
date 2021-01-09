using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestockingMicroService.Test
{
    [TestClass]
    public class SupplierFakeTests
    {
        private List<Suppliers> suppliers;
        private List<Products> products;
        private SuppliersFakeProxy testFake;

        [TestInitialize]
        public void Creator()
        {
            suppliers = new List<Suppliers>
            {
                new Suppliers
                {
                    SupplierID = 2, SupplierName = "Me", Webaddress = "xyz.net"
                },
                new Suppliers
                {
                    SupplierID = 1, SupplierName = "Help", Webaddress = "abc.com"
                }
            };

            products = new List<Products>
            {
                new Products { Id = 1, BrandId = 1, BrandName = "Help", CategoryId = 1, CategoryName = "Help", Description = "This takes a long time", Ean = "Idk what this is", ExpectedRestock = DateTime.Now, InStock = true, Name = "Trainers", Price = 66.66 },
                new Products { Id = 2, BrandId = 2, BrandName = "Me", CategoryId = 2, CategoryName = "HMe", Description = "This is no longer fun",  Ean = "Still don't know what this is", ExpectedRestock = DateTime.Now, InStock = false, Name = "Trainers", Price = 99.99 }
            };

            testFake = new SuppliersFakeProxy(suppliers, products);
        }

        [TestMethod]
        public async Task TestGetSuppliers()
        {
            var result = await testFake.GetSuppliers();

            for (int i = 0; i < suppliers.Count; ++i)
            {
                Assert.AreEqual(suppliers[i].SupplierID,result[i].SupplierID);
                Assert.AreEqual(suppliers[i].SupplierName, result[i].SupplierName);
                Assert.AreEqual(suppliers[i].Webaddress, result[i].Webaddress);
            }
        }

        [TestMethod]
        public async Task TestGetSupplierProducts()
        {
            int i = 0;
            var result = await testFake.GetSupplierProducts(1);

            Assert.AreEqual(products[i].Id, result[i].Id);
            Assert.AreEqual(products[i].Description, result[i].Description);
            Assert.AreEqual(products[i].Name, result[i].Name);
            Assert.AreEqual(products[i].BrandId, result[i].BrandId);
            Assert.AreEqual(products[i].BrandName, result[i].BrandName);
            Assert.AreEqual(products[i].CategoryId, result[i].CategoryId);
            Assert.AreEqual(products[i].CategoryName, result[i].CategoryName);
            Assert.AreEqual(products[i].Ean, result[i].Ean);
            Assert.AreEqual(products[i].ExpectedRestock, result[i].ExpectedRestock);
            Assert.AreEqual(products[i].InStock, result[i].InStock);
            Assert.AreEqual(products[i].Price, result[i].Price);
        }

        [TestMethod]
        public async Task TestGetSupplierProductsInValid()
        {
            var result = await testFake.GetSupplierProducts(3);
            Assert.AreEqual(0, result.Count);
        }
    }
}
