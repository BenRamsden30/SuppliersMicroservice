using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestockingMicroService.Data;
using RestockingMicroService.Proxies;

namespace RestockingMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "StaffOAuthorised")]
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierInterface suppliers;
        
        public SuppliersController(SupplierInterface context)
        {
            suppliers = context;
        }

        // GET: api/Suppliers
        [HttpGet("/GetAllSuppliers")]
        public async Task<ActionResult<IEnumerable<Suppliers>>> GetSuppliers()
        {
            return Ok(await suppliers.GetSuppliers());
        }

        // GET: api/Suppliers/5
        [HttpGet("/GetSupplierProducts/{id}")]
        public async Task<List<Products>> GetSupplierProducts(int id)
        {
            return await suppliers.GetSupplierProducts(id);
        }
    }
}
