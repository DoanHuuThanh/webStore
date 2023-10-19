using Azure.Core;
using doanhuuthanh_web.Application.common;
using doanhuuthanh_web.Data.Configurations;
using doanhuuthanh_web.Data.EF;
using doanhuuthanh_web.Data.Entities;
using doanhuuthanh_web.Utilities;
using doanhuuthanh_web.ViewModel.Catalog.ProductImage;
using doanhuuthanh_web.ViewModel.Catalog.Products;
using doanhuuthanh_web.ViewModel.Catalog.Products.Dtos;
using doanhuuthanh_web.ViewModel.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace doanhuuthanh_web.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly doanhuuthanhDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(doanhuuthanhDbContext context,IStorageService storageService )
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImages(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage() { 
               Caption = request.Caption,
               DateCreated = DateTime.Now,
               IsDefault = request.IsDefault,
               ProductId = productId,
               SortOder = request.SortOrder,
            };
            if(request.ImageFile != null)
            {
                productImage.ImagePath =  await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
             _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.Price,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation> {
                    new ProductTranslation()
                    {
                       Name = request.Name,
                       Description = request.Description,
                       Details = request.Details,
                       SeoDescription = request.SeoDescription,
                       SeoAlias = request.SeoAlias,
                       SeoTitle = request.SeoTitle,
                       LanguageId = request.LanguageId
                     }
                }
            };

            if(request.ThumbNewImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize  = request.ThumbNewImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbNewImage),
                        IsDefault = true,
                        SortOder = 1
                    }
                };
            }
            _context.Products.Add(product);
             await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await (_context.Products.FindAsync(productId));
            if (product == null)
            {
                throw new webException($"Cannot find a product: {productId} ");
            }
            var images = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach(var image in images)
            {
               await _storageService.DeleteFileAsync(image.ImagePath);

            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

       


        public async Task<PagedResult<ProductViewModels>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1.select 
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            //2.filterhai
            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                query = query.Where(x => x.pt.Name.Contains(request.KeyWord));
            }

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            //3.paging

            int totalRow = await query.CountAsync(); // trả về tổng số bản ghi thỏa mãn điều kiện của truy vấn, và giá trị đó sẽ được gán cho biến totalRow

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize) //take lấy ra số lượng bản ghi có trong query sau khi skip
                .Select(x => new ProductViewModels() // select lấy ra thông của những bản ghi 
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4.select and projection
            var pageResult = new PagedResult<ProductViewModels>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<ProductViewModels> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId );

            var productviewModel = new ProductViewModels()
            {
                Id = productId,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId =  productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription :null,
                SeoAlias = productTranslation !=null ? productTranslation.SeoAlias : null,
                SeoTitle = productTranslation !=null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount

            };

            return productviewModel;

        }

        public async Task<ProductImagheViewModel> GetImageById(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new webException($"cannot find a image with id: {imageId}");
            }

            var viewModel = new ProductImagheViewModel()
            {
                Caption = productImage.Caption,
                DateCreated = productImage.DateCreated,
                Id = productImage.Id,
                FileSize = productImage.FileSize,
                ImagePath = productImage.ImagePath,
                IsDefault = productImage.IsDefault,
                ProductId = productImage.ProductId,
                SortOrder = productImage.SortOder
            };
            return viewModel;
        }

        public async Task<List<ProductImagheViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                                               .Select(x => new ProductImagheViewModel()
                                               {
                                                   Caption = x.Caption,
                                                   DateCreated = x.DateCreated,
                                                   Id = x.Id,
                                                   FileSize = x.FileSize,
                                                   ImagePath = x.ImagePath,
                                                   IsDefault = x.IsDefault,
                                                   ProductId = x.ProductId,
                                                   SortOrder = x.SortOder
                                               }
                                               ).ToListAsync();
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new webException($"cannot find a image with id: {imageId}");
            }
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null)
            {
                throw new webException($"cannot find a product with id: {request.Id}");
            }
            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.Description = request.Description;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Details = request.Details;

            

            if (request.ThumbNewImage != null)
            {
                var thumbnailImage = _context.ProductImages.FirstOrDefault(x => x.IsDefault == true && x.ProductId == request.Id);
                if (thumbnailImage != null)
                {


                    thumbnailImage.FileSize = request.ThumbNewImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbNewImage);
                   _context.ProductImages.Update(thumbnailImage);
                }
               
            }
            return await _context.SaveChangesAsync();



        }


        public async Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null) {
                throw new webException($"cannot find a image with id: {imageId}");
            }
           
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new webException($"cannot find a product with id: {productId}");
            }
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new webException($"cannot find a product with id: {productId}");
            }
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
            

        }
    }
}
