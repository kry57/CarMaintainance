using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProductErrorProvider;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Products;
using CarMaintenance.Infrastructre.Context;
using Microsoft.CodeAnalysis;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProductRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IImageService imageService) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IImageService _imageService = imageService;

        public async Task<Result<ProductResonse>> AddAsync(int providerId, ProductRequest request)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider is null || provider.IsDeleted)
                return Result<ProductResonse>.Failure(ProductError.NotAssignedToPvovider);

            var normalizedName = request.Name.Trim().ToLower().Replace(" ", "");

            var isDuplicate = await _context.Products.AnyAsync(p => p.ProviderId == providerId && p.Name.Trim().ToLower().Replace(" ", "") == normalizedName && p.PartType == request.PartType);
            if (isDuplicate)
                return Result<ProductResonse>.Failure(ProductError.Duplicated);

            var product = _mapper.Map<Product>(request);

            product.ProviderId = providerId;

            await _context.Products.AddAsync(product);

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result<ProductResonse>.Failure(ProductError.NotAdded);

            var response = _mapper.Map<ProductResonse>(product);

            return Result<ProductResonse>.Success(response);
        }

        public async Task<Result<ProductImageResponse>> AddImageAsync(int providerId, int productId, ProductImageRequest request)
        {
            var provider = await _context.Providers.FindAsync(providerId);
            if (provider is null || provider.IsDeleted)
                return Result<ProductImageResponse>.Failure(ProductError.NotAssignedToPvovider);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && p.ProviderId == providerId && !p.IsDeleted);
            if (product is null)
                return Result<ProductImageResponse>.Failure(ProductError.NotFound);

            var uploadResult = await _imageService.UploadImage(request.Image);
            var image = new ProductImage
            {
                ProductId = productId,
                ImageUrl = uploadResult.ImageUrl,
                PublicId = uploadResult.PublicId,
                IsPrimary = request.IsPrimary
            };


            if (request.IsPrimary)
            {
                var oldImages = await _context.ProductImages.Where(p => p.ProductId == productId && p.IsPrimary).ToListAsync();
                foreach(var oldImage in oldImages)
                    oldImage.IsPrimary = false;
            }

            await _context.ProductImages.AddAsync(image);

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result<ProductImageResponse>.Failure(ProductError.NotAdded);

            var response = _mapper.Map<ProductImageResponse>(image);

            return Result<ProductImageResponse>.Success(response);
        }

        public async Task<Result> DeleteAsync(int providerId, int id)
        {
            var provider = await _context.Providers.FindAsync(providerId);
            if (provider is null || provider.IsDeleted)
                return Result.Failure(ProviderError.NotFound);

            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id && p.ProviderId == providerId);

            if (product is null || product.IsDeleted)
                return Result.Failure(ProductError.NotFound);

            product.IsDeleted = true;
            product.IsActive = false;

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result.Failure(ProductError.NotDeleted);

            return Result.Success();
        }

        public async Task<Result> DeleteImageAsync(int providerId, int productId, int imageId)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider is null || provider.IsDeleted)
                return Result.Failure(ProductError.NotAssignedToPvovider);

            var product = await _context.Products.FirstOrDefaultAsync(p =>p.Id == productId && p.ProviderId == providerId && !p.IsDeleted);

            if (product is null)
                return Result.Failure(ProductError.NotFound);

            var image = await _context.ProductImages.FirstOrDefaultAsync(i =>i.Id == imageId && i.ProductId == productId);
            if (image is null)
                return Result.Failure(ProductError.NotFound);

            var deletedFromCloudinary =
     await _imageService.DeleteImage(image.PublicId);

            if (!deletedFromCloudinary)
                return Result.Failure(ProductError.NotDeleted);


            _context.ProductImages.Remove(image);

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result.Failure(ProductError.NotDeleted);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<ProviderProductResponse>>> GetAllAsync(int? providerId)
        {
            var check = await _context.Products.AnyAsync(p => (providerId == null || p.ProviderId == providerId) && !p.IsDeleted && p.IsActive);
            if (!check)
                return Result<IEnumerable<ProviderProductResponse>>.Failure(ProductError.NotFound);

            var result = await (
                from p in _context.Products
                where !p.IsDeleted && p.IsActive &&
      (providerId == null || p.ProviderId == providerId)
                group p by p.ProviderId into g
                select new ProviderProductResponse
                {
                    ProviderId = g.Key,
                    ProductResonses = g.Select(p => new ProductResonse
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Brand = p.Brand,
                        PartType = p.PartType,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        IsDeleted = p.IsDeleted,
                        IsActive = p.IsActive,
                        ProviderId = p.ProviderId
                    })
                }).AsNoTracking().ToListAsync();


            if (!result.Any())
                return Result<IEnumerable<ProviderProductResponse>>
                    .Failure(ProductError.NotFound);

            return Result<IEnumerable<ProviderProductResponse>>
                .Success(result);
        }

        public async Task<Result<ProductResonse>> GetByIdAsync(int providerId, int id)
        {
            var product = await _context.Products
                  .FirstOrDefaultAsync(p =>
                      p.Id == id &&
                      p.ProviderId == providerId &&
                      !p.IsDeleted && p.IsActive);

            if (product is null)
                return Result<ProductResonse>.Failure(ProductError.NotFound);

            var response = _mapper.Map<ProductResonse>(product);

            return Result<ProductResonse>.Success(response);
        }

        public async Task<Result> ToggleStatus(int providerId, int id)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider is null || provider.IsDeleted)
                return Result.Failure(ProductError.NotAssignedToPvovider);

            var product = await _context.Products
                .FirstOrDefaultAsync(p =>
                    p.ProviderId == providerId &&
                    p.Id == id &&
                    !p.IsDeleted);

            if (product is null)
                return Result.Failure(ProductError.NotFound);

            product.IsActive = !product.IsActive;

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result.Failure(ProductError.NotUpdated);

            return Result.Success();
        }

        public async Task<Result<ProductResonse>> UpdateAsync(int providerId, int productId, ProductRequest request)
        {
            var provider = await _context.Providers.FindAsync(providerId);

            if (provider is null || provider.IsDeleted)
                return Result<ProductResonse>.Failure(ProductError.NotAssignedToPvovider);

            var isDuplicate = await _context.Products.AnyAsync(p => p.Id != productId && p.ProviderId == providerId && p.Name.Trim().ToLower().Replace(" ", "") == request.Name.Trim().ToLower().Replace(" ", "") && p.PartType == request.PartType);

            if (isDuplicate)
                return Result<ProductResonse>.Failure(ProductError.Duplicated);


            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProviderId == providerId && p.Id == productId);

            if (product is null || product.IsDeleted)
                return Result<ProductResonse>.Failure(ProductError.NotFound);

            _mapper.Map(request, product);


            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result<ProductResonse>.Failure(ProductError.NotUpdated);

            var response = _mapper.Map<ProductResonse>(product);

            return Result<ProductResonse>.Success(response);

        }

        public async Task<Result<ProductResonse>> UpdateStockAsync(int providerId, int productId, int quantity)
        {
            if (await _context.Products.FindAsync(providerId) is not { } provider)
                return Result<ProductResonse>.Failure(ProviderError.NotFound);

            if (quantity == 0)
                return Result<ProductResonse>.Failure(ProductError.InvalidStockQuantity);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProviderId == providerId && p.Id == productId && !p.IsDeleted);

            if (product is null)
                return Result<ProductResonse>.Failure(ProductError.NotFound);

            if (product.Quantity + quantity < 0)
                return Result<ProductResonse>.Failure(ProductError.InvalidStockQuantity);

            product.Quantity += quantity;

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return Result<ProductResonse>.Failure(ProductError.NotUpdated);

            var response = _mapper.Map<ProductResonse>(product);

            return Result<ProductResonse>.Success(response);




        }
    }
}
