using doanhuuthanh_web.ViewModel.Catalog.Products;
using doanhuuthanh_web.ViewModel.Catalog.Products.Dtos;
using doanhuuthanh_web.ViewModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModels>> GetAllByCategoryId(string languageId,GetPublicProductPagingRequest request);

        
    }
}
