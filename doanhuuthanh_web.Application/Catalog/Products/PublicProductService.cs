
using Azure.Core;
using doanhuuthanh_web.Data.EF;
using doanhuuthanh_web.ViewModel.Catalog.Products;
using doanhuuthanh_web.ViewModel.Catalog.Products.Dtos;
using doanhuuthanh_web.ViewModel.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace doanhuuthanh_web.Application.Catalog.Products
{
    public class PublicProductService :  IPublicProductService
    {
        private readonly doanhuuthanhDbContext _context;
        public PublicProductService(doanhuuthanhDbContext context)
        {
            _context = context;
        }

       

        public async Task<PagedResult<ProductViewModels>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            //2.filterhai


            if (request.CategoryId.HasValue && request.CategoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
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

       
    }
}
