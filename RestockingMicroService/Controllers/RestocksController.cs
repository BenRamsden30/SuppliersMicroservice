using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;

namespace RestockingMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "ManegerOAuthorised")]
    public class RestocksController : ControllerBase
    {
        private readonly RestocksInterface restocks;


        public RestocksController(RestocksInterface context)
        {
            restocks = context;
        }

        [HttpPost ("/CreateRestock/")]
        public async Task<IActionResult> CreateRestock(string AccountName, int ProductID, int Qty, string ProductName, string ProductEan, double TotalPrice, int SupplierID)
        {
            await restocks.CreateRestock(AccountName, ProductID, Qty, ProductName, ProductEan, TotalPrice, SupplierID);
            return Ok();
        }

        [HttpPost("/DeleteRestock/(id)")]
        public async Task<Restocks> DeleteRestock(int Id)
        {
            //return await restocks.DeleteRestock(Id);
            throw new NotImplementedException();
        }

        [HttpGet("/GetRestocks")]
        public async Task<ActionResult<IEnumerable<Restocks>>> GetRestocks()
        {
            return await restocks.GetRestocks();
        }

        [HttpGet("/GetRestock/(id)")]
        public async Task<List<Restocks>> GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            return await restocks.GetRestock(Id, AccountName, SupplierID, Approved);
        }

        [HttpPost("/UpdateRestock/(id)")]
        public async Task<List<Restocks>> UpdateRestock(int Id,
                                                        string AccountName,
                                                        int? ProductID,
                                                        int? Qty,
                                                        string ProductName,
                                                        string ProductEan,
                                                        double? TotalPrice,
                                                        int? SupplierID,
                                                        string CardNumber,
                                                        bool? Approved)
        {
            return await restocks.UpdateRestock(Id, AccountName, ProductID, Qty, ProductName, ProductEan, TotalPrice, SupplierID, CardNumber, Approved);
        }
    }
}
