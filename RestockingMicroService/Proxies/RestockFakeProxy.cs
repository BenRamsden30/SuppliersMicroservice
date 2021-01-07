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
                new Restocks{RestockId = 1, AccountName = null,  ProductID = 1, Gty = 6, CardNumber = null, Date = DateTime.Now, ProductEan = "What is this?", ProductName = "Test Item 1", SupplierID = 1, TotalPrice = 27.50, Approved = false },
                new Restocks{RestockId = 2, AccountName = null,  ProductID = 6, Gty = 18, CardNumber = null, Date = DateTime.Now, ProductEan = "Still don't know", ProductName = "Item test 2", SupplierID = 2, TotalPrice = 69.69, Approved = true }
            };
        }

        public Task CreateRestock(string AccountName, int ProductID, int Qty, string ProductName, string ProductEan, decimal TotalPrice, int SupplierID)
        {
            restocks.Add(new Restocks { RestockId = 3, AccountName = null, ProductID = 3, Gty = 27, CardNumber = null, Date = DateTime.Now, ProductEan = "I give up", ProductName = "TAdded", SupplierID = 1, TotalPrice = 42.00, Approved = false });
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

        Task<List<Restocks>> RestocksInterface.GetRestock(int? Id, string AccountName, int? SupplierID, bool? Approved)
        {
            if (Id.HasValue)
            {
                var d = restocks.Find(a => a.RestockId == Id);
                Desired.Add(d);
            }
            if (AccountName != null)
            {
                var d = restocks.Find(a => a.AccountName == AccountName);
                Desired.Add(d);
            }
            if (SupplierID != null)
            {
                var d = restocks.Find(a => a.SupplierID == SupplierID);
                Desired.Add(d);
            }
            if (Approved != null)
            {
                var d = restocks.Find(a => a.Approved == Approved);
                Desired.Add(d);
            }

            return Task.FromResult(Desired);
        }

        Task<List<Restocks>> RestocksInterface.UpdateRestock(int Id, string AccountName, int? ProductID, int? Qty, string ProductName, string ProductEan, double? TotalPrice, int? SupplierID, string CardNumber, bool? Approved)
        {
            var rm = restocks.Find(c => c.RestockId == Id);
            restocks.Remove(rm);
            restocks.Add(new Restocks { RestockId = Id, AccountName = AccountName, ProductID = (int)ProductID, ProductEan = ProductEan, ProductName = ProductName, Date = DateTime.Now, CardNumber = CardNumber, Gty = (int)Qty, SupplierID = (int)SupplierID, TotalPrice = (double)TotalPrice, Approved = (bool)Approved });
            return Task.FromResult(restocks);
        }
    }
}