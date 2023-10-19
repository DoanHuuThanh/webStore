using doanhuuthanh_web.Application.Catalog.Products;
using doanhuuthanh_web.ViewModel.Catalog.ProductImage;
using doanhuuthanh_web.ViewModel.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doanhuuthanh_web.backendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService) {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        [HttpGet]
       

        [HttpGet("public-paging/{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId,[FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(languageId,request);
            return Ok(product);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId,languageId);
            if (product == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {   //ModelState Khi bạn cần kiểm tra xem dữ liệu gửi từ client lên server có hợp lệ hay không
            //Khi một yêu cầu POST hoặc PUT được gửi lên server, thông tin từ form hoặc các trường dữ liệu sẽ được gửi cùng với yêu cầu
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _manageProductService.GetById(productId,request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);


        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();

        }

        [HttpDelete("productId")]
        public async Task<IActionResult> Update(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();

        }

        [HttpPatch("{productId}/{newPrice}")] //HttpPatch update 1 phần của bản ghi
        public async Task<IActionResult> UpdatePrice([FromRoute] int productId, decimal newPrice) 
        {
            var isSuccesful = await _manageProductService.UpdatePrice(productId,newPrice);
           if(isSuccesful) 
            return Ok();

           return BadRequest();

        }

        //Images
        [HttpGet("images/{imageId}")]
        public async Task<IActionResult> GetImageById( int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(image);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> Create(int productId ,[FromForm] ProductImageCreateRequest request)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImages(productId,request);
            if (imageId == 0)
            {
                return BadRequest();
            }
            var image = await _manageProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById),new {id = imageId}, image);

        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _manageProductService.UpdateImages(imageId, request);
            if (image == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]

        public async Task<IActionResult> RemoveImage(int imageId)
        {
            var result = await _manageProductService.RemoveImages(imageId);
            if(result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
