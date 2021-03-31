using AutoMapper;
using CS.Core.Service.Interfaces;
using CS.VM.Request;
using Microsoft.AspNetCore.Mvc;
using ServerWebApplication.Middleware;
using System.Threading.Tasks;

namespace ServerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IExportExcelService _exportExcelService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IMapper mapper, IExportExcelService exportExcelService)
        {
            _productCategoryService = productCategoryService;
            _exportExcelService = exportExcelService;
            _mapper = mapper;
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromQuery] ProductCategoryRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var result = await _productCategoryService.AddAsync(request);

            return Ok(new ApiOkResponse(result));
        }
    }
}
