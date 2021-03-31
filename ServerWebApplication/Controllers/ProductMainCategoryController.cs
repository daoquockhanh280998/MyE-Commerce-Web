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
    public class ProductMainCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductMainCategoryService _productMainCategoryService;
        private readonly IExportExcelService _exportExcelService;

        public ProductMainCategoryController(IProductMainCategoryService productMainCategoryService, IMapper mapper, IExportExcelService exportExcelService)
        {
            _productMainCategoryService = productMainCategoryService;
            _exportExcelService = exportExcelService;
            _mapper = mapper;
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromQuery] ProductMainCategoryRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var result = await _productMainCategoryService.AddAsync(request);

            return Ok(new ApiOkResponse(result));
        }
    }
}
