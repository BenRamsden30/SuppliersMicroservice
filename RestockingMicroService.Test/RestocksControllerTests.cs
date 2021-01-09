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
                new Restocks{RestockId = 2, AccountName = "Help",  ProductID = 6, Gty = 18, Date = DateTime.Now, ProductEan = "Still don't know", ProductName = "Item test 2", SupplierID = 2, TotalPrice = 69.69, Approved = true }
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

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithID()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(1, null, null, null)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(1, null, null, null);

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
            restocksMock.Verify(m => m.GetRestock(1, null, null, null), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithAccountName()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, "Help", null, null)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, "Help", null, null);

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
            restocksMock.Verify(m => m.GetRestock(null, "Help", null, null), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithSupplierID()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, null, 1, null)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, null, 1, null);

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
            restocksMock.Verify(m => m.GetRestock(null, null, 1, null), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithApproved()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, null, null, true)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, null, null, true);

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
            restocksMock.Verify(m => m.GetRestock(null, null, null, true), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithCombo1()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, "Help", 1, null)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, "Help", 1, null);

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
            restocksMock.Verify(m => m.GetRestock(null, "Help", 1, null), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithCombo2()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, "Help", null, false)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, "Help", null, false);

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
            restocksMock.Verify(m => m.GetRestock(null, "Help", null, false), Times.Once);
        }

        //Testing for get restock with filtering
        [TestMethod]
        public async Task TestGetRestockWithCombo3()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.GetRestock(null, null, 2, true)).ReturnsAsync(restocks);
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.GetRestock(null, null, 2, true);

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
            restocksMock.Verify(m => m.GetRestock(null, null, 2, true), Times.Once);
        }

        //Testing for Create restock when valid data is passed
        [TestMethod]
        public async Task TestCreaterestockValid()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.CreateRestock("Test", 1, 2, 1)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.CreateRestock("Test", 1, 2, 1);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);


            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.CreateRestock("Test", 1, 2, 1), Times.Once);
        }



        //Testing for Create restock when passed bad data
        [TestMethod]
        public async Task TestCreaterestockBadData()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.CreateRestock(null, -1, -2, -1)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.CreateRestock(null, -1, -2, -1);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNotNull(objResult);


            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.CreateRestock(null, -1, -2, -1), Times.Once);
        }

        //Testing for Update restock with valid data
        [TestMethod]
        public async Task TestUpdateRestockValid()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.UpdateRestock(1, null, 1, 6, "Test Update Item 1", "What is this?", 27.50, 1, null, true)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.UpdateRestock(1, null, 1, 6, "Test Update Item 1", "What is this?", 27.50, 1, null, true);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNull(objResult);
            

            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.UpdateRestock(1, null, 1, 6, "Test Update Item 1", "What is this?", 27.50, 1, null, true), Times.Once);
        }

        //Testing for Update restock with invalid data
        [TestMethod]
        public async Task TestUpdateRestockInValid()
        {
            //Sets up the controlle rand the method that is to be tested
            restocksMock.Setup(s => s.UpdateRestock(-6, null, -1, -6, "Bad Update Test", "Bad Test", 27.50, 1, null, true)).Returns(Task.Run(() => { }));
            var ResController = new RestocksController(restocksMock.Object);

            //Sets up the result from the controller
            var result = await ResController.UpdateRestock(-6, null, -1, -6, "Bad Update Test", "Bad Test", 27.50, 1, null, true);

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var objResult = result as OkObjectResult;
            Assert.IsNull(objResult);


            //Checks the result is what it is desired to be
            restocksMock.Verify();
            restocksMock.Verify(m => m.UpdateRestock(-6, null, -1, -6, "Bad Update Test", "Bad Test", 27.50, 1, null, true), Times.Once);
        }
    }
}

