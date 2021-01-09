
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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


        public async Task UpdateRestock(int Id, string AccountName, int? ProductID, int? Qty, string ProductName, string ProductEan, double? TotalPrice, int? SupplierID, string CardNumber, bool? Approved)
        {
            var Up = await _context.Restocks.FirstOrDefaultAsync(a => a.RestockId == Id);
            var Sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == Up.SupplierID);
            string Address = Sup.Webaddress;

            if (Up.Approved == false)
            {
                //Builds the location to be aimed for
                var client = new HttpClient();
                var RequestBuilderGet = new UriBuilder(Address);
                RequestBuilderGet.Path = "GET/Api/Order/" + Id + "/";
                String urlGet = RequestBuilderGet.ToString();

                //Checks to see if the deisred location was reached
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                var responseGet = await client.GetAsync(urlGet);

                //The Order is present
                while (responseGet.IsSuccessStatusCode)
                {
                    //Setting up a delete for the order adn deleting it
                    var RequestBuilderDelete = new UriBuilder(Address);
                    RequestBuilderDelete.Path = "DELETE/Api/Order/" + Id + "/";
                    String urlDelete = RequestBuilderDelete.ToString();
                    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                    var responseDelete = await client.GetAsync(urlDelete);
                }

                //Now we add the new order adn update it
                var Updates = new List<KeyValuePair<string, string>>();
                Updates.Add(new KeyValuePair<string, string>("Id", Id.ToString()));
                Updates.Add(new KeyValuePair<string, string>("AccountName", AccountName));
                Updates.Add(new KeyValuePair<string, string>("CardNumber", CardNumber));
                Updates.Add(new KeyValuePair<string, string>("Quantity", Qty.ToString()));
                Updates.Add(new KeyValuePair<string, string>("When", DateTime.Now.ToString()));
                Updates.Add(new KeyValuePair<string, string>("ProductName", ProductName));
                Updates.Add(new KeyValuePair<string, string>("ProductEan", ProductEan));
                Updates.Add(new KeyValuePair<string, string>("TotalPrice", TotalPrice.ToString()));


                Up.ProductID = (int)ProductID;
                Up.Gty = (int)Qty;
                Up.ProductEan = ProductEan;
                Up.TotalPrice = (double)TotalPrice;
                Up.ProductName = ProductName;
                Up.SupplierID = (int)SupplierID;
                Up.Date = DateTime.Now;
                Up.AccountName = AccountName;
                Up.Approved = (bool)Approved;

                if (Up.Approved == true)
                {
                    var RequestBuilderPost = new UriBuilder(Address);
                    RequestBuilderPost.Path = "POST/Api/Order/";
                    String urlPost = RequestBuilderPost.ToString();

                    var clientPost = new HttpClient();
                    await clientPost.PostAsync(urlPost, new FormUrlEncodedContent(Updates));
                    _context.Update(Up);
                }
                else
                {
                    _context.Update(Up);
                }
            }

            else
            {
                throw new InvalidOperationException("There is no restock under this restockID");
            }

        }



        public async Task DeleteRestock(int Id)
        {
            var Rm = await _context.Restocks.FirstOrDefaultAsync(a => a.RestockId == Id);
            var Sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == Rm.SupplierID);
            string Address = Sup.Webaddress;

            //Builds the location to be aimed for
            var client = new HttpClient();
            var RequestBuilder = new UriBuilder(Address);
            RequestBuilder.Path = "GET/Api/Order/" + Id + "/";
            String url = RequestBuilder.ToString();

            //Checks to see if the deisred location was reached
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var response = await client.GetAsync(url);

            while (response.IsSuccessStatusCode)
            {
                var client2 = new HttpClient();
                //Builds the location to be aimed for
                var RequestBuilder2 = new UriBuilder(Address);
                RequestBuilder2.Path = "/DELETE/Api/Order/" + Id + "/";
                url = RequestBuilder2.ToString();

                //Checks to see if the deisred location was reached
                client2.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                var response2 = await client.GetAsync(url);
            }
            _context.Restocks.Remove(Rm);
        }

        public async Task CreateRestock(string AccountName, int ProductID, int Qty, int SupplierID)
        {
            string ProductEan = "";
            string ProductName ="";
            double TotalPrice = 0.00;

            var Sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == SupplierID);
            string Address = Sup.Webaddress.ToString();


            //Builds the location to be aimed for
            var client = new HttpClient();
            var RequestBuilder = new UriBuilder(Address);
            RequestBuilder.Path = "api/Product/" + ProductID.ToString();
            String url = RequestBuilder.ToString();

            //Checks to see if the deisred location was reached
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Products product = await response.Content.ReadAsAsync<Products>();
                ProductEan = product.Ean;
                ProductName =product.Name;
                TotalPrice = (Qty * product.Price);
            }

            
            Restocks Order = new Restocks { AccountName = AccountName, ProductID = ProductID, Approved = false, Date = DateTime.Now, Gty = Qty, ProductEan = ProductEan, ProductName = ProductName, SupplierID = SupplierID, TotalPrice = TotalPrice };
            _context.Restocks.Add(Order);
            
        }
    }
}
