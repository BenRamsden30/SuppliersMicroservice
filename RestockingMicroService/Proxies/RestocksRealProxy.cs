using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class RestocksRealProxy : RestocksInterface
    {
        public Task CreateRestock(string AccountName, int ProductID, int Qty, string ProductName, string ProductEan, decimal TotalPrice, int SupplierID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRestock(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Restocks>> GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            throw new NotImplementedException();
        }

        public Task<List<Restocks>> GetRestocks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Restocks>> UpdateRestock(int Id, string AccountName, int? ProductID, int? Qty, string ProductName, string ProductEan, double? TotalPrice, int? SupplierID, string CardNumber, bool? Approved)
        {
            throw new NotImplementedException();
        }
    }
}
