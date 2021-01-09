using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestockingMicroService.Proxies;

namespace RestockingMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RestocksController : ControllerBase
    {
        private readonly RestocksInterface restocks;


        public RestocksController(RestocksInterface context)
        {
            restocks = context;
        }

        [HttpPost ("/CreateRestock/")]
        public async Task<IActionResult> CreateRestock([FromForm] string AccountName, [FromForm] int ProductID, [FromForm] int Qty, [FromForm]  int SupplierID)
        {
            await restocks.CreateRestock(AccountName, ProductID, Qty, SupplierID);
            return Ok(restocks);
        }

        [HttpPost("/DeleteRestock/{Id}")]
        public async Task<IActionResult> DeleteRestock(int Id)
        {
            await restocks.DeleteRestock(Id);
            return Ok();
        }

        [HttpGet("/GetRestocks")]
        public async Task<IActionResult> GetRestocks()
        {
            return Ok(await restocks.GetRestocks());
        }

        [HttpGet("/GetRestock")]
        public async Task<IActionResult> GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            return Ok(await restocks.GetRestock(Id, AccountName, SupplierID, Approved));
        }

        [HttpPost("/UpdateRestock/{Id}")]
        public async Task<IActionResult> UpdateRestock(int Id,
                                                        [FromForm] string AccountName,
                                                        [FromForm]  string CardNumber,
                                                        [FromForm]  bool? Approved)
        {
            await restocks.UpdateRestock(Id, AccountName, CardNumber, Approved);
            return Ok();
        }
    }
}
