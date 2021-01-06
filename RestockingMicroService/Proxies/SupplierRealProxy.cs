using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class SupplierRealProxy : SupplierInterface
    {
        private readonly RestockingMicroServiceContext _context;

        public SupplierRealProxy(RestockingMicroServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<Suppliers> GetSupplier(int Id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(a => a.SupplierID == Id);
        }

        public async Task<List<Suppliers>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
