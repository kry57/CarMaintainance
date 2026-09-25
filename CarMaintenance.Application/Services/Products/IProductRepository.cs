using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Products
{
    public interface IProductRepository
    {
        Task<Result<ProductResonse>> AddAsync(int providerId,ProductRequest request);
        Task<Result<ProductResonse>> UpdateAsync(int providerId, int productId, ProductRequest request);
        Task<Result<IEnumerable<ProviderProductResponse>>> GetAllAsync(int? providerId);

        Task<Result<ProductResonse>> GetByIdAsync(int providerId, int id);
        Task<Result> DeleteAsync(int providerId,int id);
        Task<Result> ToggleStatus(int providerId, int id);
        Task<Result<ProductResonse>> UpdateStockAsync( int providerId,int productId,int quantity);
        Task<Result<ProductImageResponse>> AddImageAsync( int providerId,int productId,ProductImageRequest request);
        Task<Result> DeleteImageAsync(int providerId, int productId, int imageId);
    }
}
