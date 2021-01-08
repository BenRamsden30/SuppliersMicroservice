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
    public class SupplierControllerTEsts
    {
        
        private Mock<RestocksInterface> restocksMock;
        List<Restocks> restocks;

        //Sets up the variables to be sued in testing and is ran before any test is ran.
        [TestInitialize]
        public void Creator()
        {
            restocks = new List<Restocks>
            {
                new Restocks{RestockId = 1, AccountName = null,  ProductID = 1, Gty = 6, Date = DateTime.Now, ProductEan = "What is this?", ProductName = "Test Item 1", SupplierID = 1, TotalPrice = 27.50, Approved = false },
                new Restocks{RestockId = 2, AccountName = null,  ProductID = 6, Gty = 18, Date = DateTime.Now, ProductEan = "Still don't know", ProductName = "Item test 2", SupplierID = 2, TotalPrice = 69.69, Approved = true }
            };

            restocksMock = new Mock<RestocksInterface>(MockBehavior.Strict);
        }

        //Testing get Restocks
        [TestMethod]
        public async Task TestGetRestocks()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestocks()).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestocks();

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var restocksResult = objResult.Value as IEnumerable<Restocks>;
            Assert.IsNotNull(restocksResult);
            var restocksResultList = restocksResult.ToList();
            Assert.AreEqual(restocks.Count, restocksResultList.Count);
            for (int i = 0; i < restocks.Count; ++i)
            {
                Assert.AreEqual(restocks[i].SupplierID, restocksResultList[i].SupplierID);
                Assert.AreEqual(restocks[i].AccountName ,restocksResultList[i].AccountName );
                Assert.AreEqual(restocks[i].Approved , restocksResultList[i].Approved);
                Assert.AreEqual(restocks[i].Date, restocksResultList[i].Date);
                Assert.AreEqual(restocks[i].Gty, restocksResultList[i].Gty);
                Assert.AreEqual(restocks[i].ProductEan, restocksResultList[i].ProductEan);
                Assert.AreEqual(restocks[i].ProductID, restocksResultList[i].ProductID);
                Assert.AreEqual(restocks[i].Approved, restocksResultList[i].Approved);
                Assert.AreEqual(restocks[i].ProductName, restocksResultList[i].ProductName);
                Assert.AreEqual(restocks[i].RestockId, restocksResultList[i].RestockId);
                Assert.AreEqual(restocks[i].TotalPrice, restocksResultList[i].TotalPrice);
            }

            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.GetRestocks(), Times.Once);
        }

        //Testing delete a restock with a valid Id
        [TestMethod]
        public async Task TestDeleteRestocks()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.DeleteRestock(1)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.DeleteRestock(1);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNull(objResult);
            

            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.DeleteRestock(1), Times.Once);
        }

        //Testing delete a restock with a valid Id
        [TestMethod]
        public async Task TestDeleteRestocksFail()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.DeleteRestock(-1)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.DeleteRestock(-1);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNull(objResult);


            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.DeleteRestock(-1), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockBlank()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, null, null, null)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, null, null, null);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var restocksResult = objResult.Value as IEnumerable<Restocks>;
            Assert.IsNotNull(restocksResult);
            var restocksResultList = restocksResult.ToList();
            Assert.AreEqual(restocks.Count, restocksResultList.Count);
            for (int i = 0; i < restocks.Count; ++i)
            {
                Assert.AreEqual(restocks[i].SupplierID, restocksResultList[i].SupplierID);
                Assert.AreEqual(restocks[i].AccountName, restocksResultList[i].AccountName);
                Assert.AreEqual(restocks[i].Approved, restocksResultList[i].Approved);
                Assert.AreEqual(restocks[i].Date, restocksResultList[i].Date);
                Assert.AreEqual(restocks[i].Gty, restocksResultList[i].Gty);
                Assert.AreEqual(restocks[i].ProductEan, restocksResultList[i].ProductEan);
                Assert.AreEqual(restocks[i].ProductID, restocksResultList[i].ProductID);
                Assert.AreEqual(restocks[i].Approved, restocksResultList[i].Approved);
                Assert.AreEqual(restocks[i].ProductName, restocksResultList[i].ProductName);
                Assert.AreEqual(restocks[i].RestockId, restocksResultList[i].RestockId);
                Assert.AreEqual(restocks[i].TotalPrice, restocksResultList[i].TotalPrice);
            }

            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.GetRestock(null, null, null, null), Times.Once);
        }



    }
}