using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestockingMicroService.Controllers;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Test
{
    [TestClass]
    public class UnitTest1
    {
        private List<Suppliers> suppliers;
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
                }
            };

            suppliersMock = new Mock<SupplierInterface>(MockBehavior.Strict);
        }

        //Test
        [TestMethod]
        public async Task TestMethod1()
        {
            //Sets up the controlle rand the method that is to be tested
            suppliersMock.Setup(s => s.GetSuppliers()).ReturnsAsync(suppliers);
            var SupController = new SuppliersController(suppliersMock.Object);

            //Sets up the result from the controller
            var result = await SupController.GetSuppliers();

            //Checks the response data section by section
            Assert.IsNotNull(result);
            var suppliersResult = result.Value as IEnumerable<Suppliers>;
            Assert.IsNotNull(suppliersResult);
            var suppliersResultList = suppliersResult.ToList();
            Assert.AreEqual(suppliers.Count, suppliersResultList.Count);
            for (int i=0; i<suppliers.Count; ++i)
            {
                Assert.AreEqual(suppliers[i].SupplierID, suppliersResultList[i].SupplierID);
                Assert.AreEqual(suppliers[i].SupplierName, suppliersResultList[i].SupplierName);
                Assert.AreEqual(suppliers[i].Webaddress, suppliersResultList[i].Webaddress);
            }

            //Checks the result is what it is desired to be
            suppliersMock.Verify();
            suppliersMock.Verify(m => m.GetSuppliers(), Times.Once);
        }

        // Pass valid int pass invalid int pass negative int, pass null

    }
}
