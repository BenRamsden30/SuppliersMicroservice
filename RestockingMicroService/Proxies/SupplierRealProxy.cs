using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        


        public async Task<List<Products>> GetSupplierProducts(int Id)
        {
            var Sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == Id);
            string Address = Sup.Webaddress;

            //Builds the location to be aimed for
            var client = new HttpClient();
            var RequestBuilder = new UriBuilder(Address);
            RequestBuilder.Path = "/Api/Product";
            String url = RequestBuilder.ToString();

            //Checks to see if the deisred location was reached
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Products>>();
            }
            else
            {
                return null;
            }
            
        }

        public async Task<List<Suppliers>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
