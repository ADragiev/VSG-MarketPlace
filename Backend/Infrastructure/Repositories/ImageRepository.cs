﻿using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.GenericRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public async Task<Image> GetImageByProductIdAsync(int productId)
        {
            var sql = @"SELECT * FROM Image
                        WHERE ProductId = @productId";

            return await connection.QueryFirstOrDefaultAsync<Image>(sql, new { productId }, transaction);
        }
    }
}
