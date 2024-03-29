﻿using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public class RestockFakeProxy : RestocksInterface
    {
        List<Restocks> restocks;

        List<Restocks> Desired;

        public RestockFakeProxy()
        {
            restocks = new List<Restocks>
            {
                new Restocks{RestockId = 1, AccountName = null,  ProductID = 1, Gty = 6, Date = DateTime.Now, ProductEan = "What is this?", ProductName = "Test Item 1", SupplierID = 1, TotalPrice = 27.50, Approved = false },
                new Restocks{RestockId = 2, AccountName = null,  ProductID = 6, Gty = 18, Date = DateTime.Now, ProductEan = "Still don't know", ProductName = "Item test 2", SupplierID = 2, TotalPrice = 69.69, Approved = true }
            };
        }

        public RestockFakeProxy(List<Restocks> restocks)
        {
            this.restocks = restocks;
        }

        public Task CreateRestock(string AccountName, int ProductID, int Qty, int SupplierID)
        {
            restocks.Add(new Restocks { RestockId = 3, AccountName = null, ProductID = 3, Gty = 27, Date = DateTime.Now, ProductEan = "I give up", ProductName = "TAdded", SupplierID = 1, TotalPrice = 42.00, Approved = false });
            return Task.FromResult(restocks);
            
        }

        public Task DeleteRestock(int Id)
        {
            var rm = restocks.Find(c => c.RestockId == Id);
            restocks.Remove(rm);
            return Task.FromResult(restocks);
        }

        public Task<List<Restocks>> GetRestocks()
        {
            return Task.FromResult(restocks);
        }

        public Task<List<Restocks>> GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            Desired = restocks.Where(d =>
            (!Id.HasValue || d.RestockId == Id.Value)
            &&
            (String.IsNullOrEmpty(AccountName) || d.AccountName == AccountName)
            &&
            (!SupplierID.HasValue || d.SupplierID == SupplierID.Value)
            &&
            (!Approved.HasValue || d.Approved == Approved.Value)
            ).ToList();
            

            return Task.FromResult(Desired);
        }

        public Task UpdateRestock(int Id, string AccountName, string CardNumber, bool? Approved)
        {
            var rm = restocks.Find(c => c.RestockId == Id);
            
            restocks.Remove(rm);
            restocks.Add(new Restocks { RestockId = Id, AccountName = AccountName, ProductID = rm.ProductID, ProductEan = rm.ProductEan.ToString(), ProductName = rm.ProductName, Date = DateTime.Now, Gty = rm.Gty, SupplierID = rm.SupplierID, TotalPrice = rm.TotalPrice, Approved = (bool)Approved });
            return Task.FromResult(restocks);
        }
    }
}
