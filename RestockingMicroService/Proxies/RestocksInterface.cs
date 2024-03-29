﻿using RestockingMicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestockingMicroService.Proxies
{
    public interface RestocksInterface
    {
        public Task<List<Restocks>> GetRestocks();

        public Task<List<Restocks>> GetRestock(int? Id,
                                         string? AccountName,
                                         int? SupplierID,
                                         bool? Approved);

        public Task CreateRestock(string AccountName,
                                  int ProductID,
                                  int Qty,
                                  
                                  
                                  
                                  int SupplierID);

        public Task DeleteRestock(int Id);

        public Task UpdateRestock(int Id,
                                  string? AccountName,
                                  string? CardNumber,
                                  bool? Approved);
    }
}
