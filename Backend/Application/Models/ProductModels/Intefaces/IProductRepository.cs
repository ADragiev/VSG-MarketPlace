﻿using Application.Models.ExportModels;
using Application.Models.GenericRepo;
using Application.Models.ProductModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Intefaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<ProductMarketPlaceGetDto>> GetAllIndexProductsAsync();
        Task<List<ProductInventoryGetDto>> GetAllInventoryProductsAsync();
        Task<Product> GetByCodeAndLocationAsync(string code, int locationId);
        Task<List<ProductExportDto>> GetProductsForExport();
    }
}
