using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestockingMicroService.Controllers;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Test
{
    [TestClass]
    public class RestocksControllerTests
    {
        private List<Suppliers> suppliers;
        private List<Products> products;
        private Mock<SupplierInterface> suppliersMock;

        //Sets up the variables to be sued in testing and is ran before any test is ran.
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
                new Products { Id = 2, BrandId = 2, BrandName = "Me", CategoryId = 2, CategoryName = "HMe", Description = "This is no longer fun", Ean = "Still don't know what this is", ExpectedRestock = DateTime.Now, InStock = false, Name = "Trainers", Price = 99.99 }
            };

            suppliersMock = new Mock<SupplierInterface>(MockBehavior.Strict);
        }

        //Testing get supplierproducts
        [TestMethod]
        public async Task TestGetSuppliers()
        {
            //Sets up the controlle rand the method that is to be tested
            suppliersMock.Setup(s => s.GetSuppliers()).ReturnsAsync(suppliers);
            var SupController = new SuppliersController(suppliersMock.Object);

            //Sets up the result from the controller
            var result = await SupController.GetSuppliers();

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var suppliersResult = objResult.Value as IEnumerable<Suppliers>;
            Assert.IsNotNull(suppliersResult);
            var suppliersResultList = suppliersResult.ToList();
            Assert.AreEqual(suppliers.Count, suppliersResultList.Count);
            for (int i = 0; i < suppliers.Count; ++i)
            {
                Assert.AreEqual(suppliers[i].SupplierID, suppliersResultList[i].SupplierID);
                Assert.AreEqual(suppliers[i].SupplierName, suppliersResultList[i].SupplierName);
                Assert.AreEqual(suppliers[i].Webaddress, suppliersResultList[i].Webaddress);
            }

            //Checks the result is what it is desired to be
            suppliersMock.Verify();
            suppliersMock.Verify(m => m.GetSuppliers(), Times.Once);
        }

        [TestMethod]
        // Pass valid int pass invalid int pass negative int, pass null
        public async Task TestGetSupplierProducts()
        {
            //Sets up the controlle rand the method that is to be tested
            suppliersMock.Setup(s => s.GetSupplierProducts(1)).ReturnsAsync(products);
            var ResController = new SuppliersController(suppliersMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetSupplierProducts(1);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var ProductsResult = objResult.Value as IEnumerable<Products>;
            Assert.IsNotNull(ProductsResult);
            var productsResultList = ProductsResult.ToList();
            Assert.AreEqual(products.Count, productsResultList.Count);

            //Nesting all the individul values
            for (int i = 0; i < products.Count; ++i)
            {
                Assert.AreEqual(products[i].Id, productsResultList[i].Id);
                Assert.AreEqual(products[i].Description, productsResultList[i].Description);
                Assert.AreEqual(products[i].Name, productsResultList[i].Name);
                Assert.AreEqual(products[i].BrandId, productsResultList[i].BrandId);
                Assert.AreEqual(products[i].BrandName, productsResultList[i].BrandName);
                Assert.AreEqual(products[i].CategoryId, productsResultList[i].CategoryId);
                Assert.AreEqual(products[i].CategoryName, productsResultList[i].CategoryName);
                Assert.AreEqual(products[i].Ean, productsResultList[i].Ean);
                Assert.AreEqual(products[i].ExpectedRestock, productsResultList[i].ExpectedRestock);
                Assert.AreEqual(products[i].InStock, productsResultList[i].InStock);
                Assert.AreEqual(products[i].Price, productsResultList[i].Price);
            }

            //Checks the result is what it is desired to be
            suppliersMock.Verify();
            suppliersMock.Verify(m => m.GetSupplierProducts(1), Times.Once);
        }

        [TestMethod]
        // Pass valid int pass invalid int pass negative int, pass null
        public async Task TestGetSupplierProducts2()
        {
            //Sets up the controlle rand the method that is to be tested
            suppliersMock.Setup(s => s.GetSupplierProducts(2)).ReturnsAsync(products);
            var ResController = new SuppliersController(suppliersMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetSupplierProducts(2);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var ProductsResult = objResult.Value as IEnumerable<Products>;
            Assert.IsNotNull(ProductsResult);
            var productsResultList = ProductsResult.ToList();
            Assert.AreEqual(products.Count, productsResultList.Count);

            //Nesting all the individul values
            for (int i = 0; i < products.Count; ++i)
            {
                Assert.AreEqual(products[i].Id, productsResultList[i].Id);
                Assert.AreEqual(products[i].Description, productsResultList[i].Description);
                Assert.AreEqual(products[i].Name, productsResultList[i].Name);
                Assert.AreEqual(products[i].BrandId, productsResultList[i].BrandId);
                Assert.AreEqual(products[i].BrandName, productsResultList[i].BrandName);
                Assert.AreEqual(products[i].CategoryId, productsResultList[i].CategoryId);
                Assert.AreEqual(products[i].CategoryName, productsResultList[i].CategoryName);
                Assert.AreEqual(products[i].Ean, productsResultList[i].Ean);
                Assert.AreEqual(products[i].ExpectedRestock, productsResultList[i].ExpectedRestock);
                Assert.AreEqual(products[i].InStock, productsResultList[i].InStock);
                Assert.AreEqual(products[i].Price, productsResultList[i].Price);
            }

            //Checks the result is what it is desired to be
            suppliersMock.Verify();
            suppliersMock.Verify(m => m.GetSupplierProducts(2), Times.Once);
        }

        [TestMethod]
        // Pass valid int pass invalid int pass negative int, pass null
        public async Task TestGetSupplierProductsFailing()
        {
            //Sets up the controlle rand the method that is to be tested
            suppliersMock.Setup(s => s.GetSupplierProducts(3)).ReturnsAsync(products);
            var ResController = new SuppliersController(suppliersMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetSupplierProducts(3);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var ProductsResult = objResult.Value as IEnumerable<Products>;
            Assert.IsNotNull(ProductsResult);
            var productsResultList = ProductsResult.ToList();
            Assert.AreEqual(products.Count, productsResultList.Count);

            //Nesting all the individul values
            for (int i = 0; i < products.Count; ++i)
            {
                Assert.AreEqual(products[i].Id, null);
                Assert.AreEqual(products[i].Description, null);
                Assert.AreEqual(products[i].Name, null);
                Assert.AreEqual(products[i].BrandId, null);
                Assert.AreEqual(products[i].BrandName, null);
                Assert.AreEqual(products[i].CategoryId, null);
                Assert.AreEqual(products[i].CategoryName, null);
                Assert.AreEqual(products[i].Ean, null);
                Assert.AreEqual(products[i].ExpectedRestock, null);
                Assert.AreEqual(products[i].InStock, null);
                Assert.AreEqual(products[i].Price, null);
            }

            //Checks the result is what it is desired to be
            suppliersMock.Verify();
            suppliersMock.Verify(m => m.GetSupplierProducts(3), Times.Once);
        }
    }
}
