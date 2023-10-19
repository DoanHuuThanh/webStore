
using doanhuuthanh_web.ViewModel.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanhuuthanh_web.ViewModel.Catalog.ProductImage;
using doanhuuthanh_web.ViewModel.Catalog.Products;
using doanhuuthanh_web.ViewModel.Catalog.Products.Dtos;


namespace doanhuuthanh_web.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int productId, decimal  newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<int> Delete(int productId);
        Task<ProductViewModels> GetById(int productId, string languageId);
       
        Task<PagedResult<ProductViewModels>> GetAllPaging(GetManageProductPagingRequest request); // trả về 1 list ProductViewModels

        Task<int> AddImages(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request);
        Task<ProductImagheViewModel>  GetImageById (int imageId);
        Task<List<ProductImagheViewModel>> GetListImage(int productId);
    }
}
