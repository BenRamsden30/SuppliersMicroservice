using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;

namespace RestockingMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierInterface suppliers;
        
        public SuppliersController(SupplierInterface context)
        {
            suppliers = context;
        }

        // GET: api/Suppliers
        [HttpGet("/GetAllSuppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            return Ok(await suppliers.GetSuppliers());
        }

        // GET: api/Suppliers/5
        [HttpGet("/GetSupplierProducts/{id}")]
        public async Task<IActionResult> GetSupplierProducts(int id)
        {
            return Ok(await suppliers.GetSupplierProducts(id));
        }
    }
}
