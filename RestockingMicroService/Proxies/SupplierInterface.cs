using RestockingMicroService.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public interface SupplierInterface
    {
        public Task<List<Suppliers>> GetSuppliers();

        public Task<Suppliers> GetSupplier(int Id);

        public Task<List<Products>> GetSupplierProducts(int Id);
    }
}
