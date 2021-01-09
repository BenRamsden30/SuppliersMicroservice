using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestockingMicroService.Test
{
    [TestClass]
    public class RestockFakeTests
    {
        private RestockFakeProxy testFake;
        List<Restocks> restocks;

        [TestInitialize]
        public void Creator()
        {
            restocks = new List<Restocks>
            {
                new Restocks{RestockId = 1, AccountName = null,  ProductID = 1, Gty = 6, Date = DateTime.Now, ProductEan = "What is this?", ProductName = "Test Item 1", SupplierID = 1, TotalPrice = 27.50, Approved = false },
                new Restocks{RestockId = 2, AccountName = "Help",  ProductID = 6, Gty = 18, Date = DateTime.Now, ProductEan = "Still don't know", ProductName = "Item test 2", SupplierID = 2, TotalPrice = 69.69, Approved = true }
            };

            testFake = new RestockFakeProxy(restocks);
        }

        //Testing get Restocks
        [TestMethod]
        public async Task TestGetRestocks()
        {
            var result = await testFake.GetRestocks();

            for (int i = 0; i < restocks.Count; ++i)
            {
                Assert.AreEqual(restocks[i].SupplierID, result[i].SupplierID);
                Assert.AreEqual(restocks[i].AccountName, result[i].AccountName);
                Assert.AreEqual(restocks[i].Approved, result[i].Approved);
                Assert.AreEqual(restocks[i].Date, result[i].Date);
                Assert.AreEqual(restocks[i].Gty, result[i].Gty);
                Assert.AreEqual(restocks[i].ProductEan, result[i].ProductEan);
                Assert.AreEqual(restocks[i].ProductID, result[i].ProductID);
                Assert.AreEqual(restocks[i].Approved, result[i].Approved);
                Assert.AreEqual(restocks[i].ProductName, result[i].ProductName);
                Assert.AreEqual(restocks[i].RestockId, result[i].RestockId);
                Assert.AreEqual(restocks[i].TotalPrice, result[i].TotalPrice);
            }
        }

        //Tests delete restock with a valid id
        [TestMethod]
        public async Task TestDeleteRestocks()
        {
            await testFake.DeleteRestock(1);

            Assert.AreEqual(1, restocks.Count);
        }

        //Tests delete restock with a valid id
        [TestMethod]
        public async Task TestDeleteRestocksInValid()
        {
            await testFake.DeleteRestock(6);

            Assert.AreEqual(2, restocks.Count);
        }


    }
}

