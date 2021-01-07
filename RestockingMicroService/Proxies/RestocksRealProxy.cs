using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class RestocksRealProxy : RestocksInterface
    {
        private readonly RestockingMicroServiceContext _context;

        public RestocksRealProxy(RestockingMicroServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<List<Restocks>> GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            var Desired = new List<Restocks> { };
                if(Id != null)
            {
                Desired = await _context.Restocks.Where(e => e.RestockId == Id).ToListAsync();
            }
            else if (AccountName != null)
            {
                Desired = await _context.Restocks.Where(e => e.AccountName == AccountName).ToListAsync();
            }
                else if (SupplierID != null)
            {
                Desired = await _context.Restocks.Where(e => e.SupplierID == SupplierID).ToListAsync();
            }
                else if (Approved != null)
            {
                Desired = await _context.Restocks.Where(e => e.Approved == Approved).ToListAsync();
            }
            else
            {
                return null;
            }
            return await Task.FromResult(Desired);
        }

        public async Task<List<Restocks>> GetRestocks()
        {
            return await _context.Restocks.ToListAsync();
        }





        public Task<List<Restocks>> UpdateRestock(int Id, string AccountName, int? ProductID, int? Qty, string ProductName, string ProductEan, double? TotalPrice, int? SupplierID, string CardNumber, bool? Approved)
        {
            throw new NotImplementedException();
        }

        public Task CreateRestock(string AccountName, int ProductID, int Qty, string ProductName, string ProductEan, decimal TotalPrice, int SupplierID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRestock(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
