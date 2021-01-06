using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public interface SupplierInterface
    {
        public Task<List<Suppliers>> GetSuppliers();

        public Task<Suppliers> GetSupplier(int Id);
    }
}
