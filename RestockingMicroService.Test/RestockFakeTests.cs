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

        [TestMethod]
        public async Task TestCreateRestockValid()
        {
            await testFake.CreateRestock("Test", 1, 2, 1);
            Assert.AreEqual(3, restocks.Count);
        }

        [TestMethod]
        public async Task TestUpdateRestockValidd()
        {
            await testFake.UpdateRestock(1, null, null, true);
            Assert.AreEqual(2, restocks.Count);
            Assert.AreEqual("Help", restocks[0].AccountName);
            Assert.AreEqual(true, restocks[0].Approved);
            Assert.AreEqual(18, restocks[0].Gty);
            Assert.AreEqual(6, restocks[0].ProductID);
            Assert.AreEqual("Still don't know", restocks[0].ProductEan);
            Assert.AreEqual("Item test 2", restocks[0].ProductName);
            Assert.AreEqual(69.69, restocks[0].TotalPrice);
            Assert.AreEqual(2, restocks[0].SupplierID);
        }

        [TestMethod]
        public async Task TestGetRestockBlank()
        {
            var result = await testFake.GetRestock(null, null, null, null);

            Assert.AreEqual(2, result.Count);
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

        [TestMethod]
        public async Task TestGetRestockIdValid()
        {
            int i = 0;
            var result = await testFake.GetRestock(1, null, null, null);

            Assert.AreEqual(1, result.Count);
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

        [TestMethod]
        public async Task TestGetRestockIdInValid()
        {
            var result = await testFake.GetRestock(18, null, null, null);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task TestGetRestockAccountNameValid()
        {
            int i = 0;
            int r = 1;
            var result = await testFake.GetRestock(null, "Help", null, null);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }

        [TestMethod]
        public async Task TestGetRestockAccountNameInValid()
        {
            var result = await testFake.GetRestock(null, "Error", null, null);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task TestGetRestockProductIDValid()
        {
            int i = 0;
            int r = 0;
            var result = await testFake.GetRestock(null, null, 1, null);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }

        [TestMethod]
        public async Task TestGetRestockProductIDInValid()
        {
            var result = await testFake.GetRestock(null, null, 18, null);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task TestGetRestockApprovedValid()
        {
            int i = 0;
            int r = 0;
            var result = await testFake.GetRestock(null, null, null, false);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }

        [TestMethod]
        public async Task TestGetRestockCombo1Valid()
        {
            int i = 0;
            int r = 1;
            var result = await testFake.GetRestock(null, "Help", null, true);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }

        [TestMethod]
        public async Task TestGetRestockCombo2Valid()
        {
            int i = 0;
            int r = 1;
            var result = await testFake.GetRestock(null, "Help", 2, null);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }

        [TestMethod]
        public async Task TestGetRestockCombo3Valid()
        {
            int i = 0;
            int r = 0;
            var result = await testFake.GetRestock(null, null, 1, false);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(restocks[r].SupplierID, result[i].SupplierID);
            Assert.AreEqual(restocks[r].AccountName, result[i].AccountName);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].Date, result[i].Date);
            Assert.AreEqual(restocks[r].Gty, result[i].Gty);
            Assert.AreEqual(restocks[r].ProductEan, result[i].ProductEan);
            Assert.AreEqual(restocks[r].ProductID, result[i].ProductID);
            Assert.AreEqual(restocks[r].Approved, result[i].Approved);
            Assert.AreEqual(restocks[r].ProductName, result[i].ProductName);
            Assert.AreEqual(restocks[r].RestockId, result[i].RestockId);
            Assert.AreEqual(restocks[r].TotalPrice, result[i].TotalPrice);
        }
    }
}
