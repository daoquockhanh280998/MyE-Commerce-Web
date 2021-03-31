using AutoMapper;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerWebApplication.Middleware;
using System;
using System.Threading.Tasks;

namespace ServerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IExportExcelService _exportExcelService;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, IExportExcelService exportExcelService)
        {
            _productService = productService;
            _exportExcelService = exportExcelService;
            _mapper = mapper;
        }

        [HttpGet, Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return Ok(new ApiOkResponse(response));
        }

        [HttpPost, Route("all")]
        public async Task<IActionResult> GetAllProduct([FromBody] DataTableParameters parameters)
        {
            var result = await _productService.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromForm] ProductRequest request)
        {
           
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));
            request.CreateBy =  User.Identity.Name;
            var result = await _productService.AddAsync(request);

            return Ok(new ApiOkResponse(result));
        }

        [HttpGet, Route("find/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = await _productService.GetAsync(id);
            var model = _mapper.Map<Product>(response);
            return Ok(new ApiOkResponse(model));
        }

        [HttpPost, Route("update/{id}")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest request, Guid id)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));
            var u = User.Identity.Name + "123456789";
            request.CreateBy = u;
            var response = await _productService.UpdateProductAsync(request, id);
            return Ok(new ApiOkResponse(response));
        }

        [HttpPost, Route("delete/{id}")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound(new ApiResponse(false, 404, $"Product is not found with id {id}"));
            }

            var model = await _productService.GetAsync(id);
            if (model == null)
            {
                return NotFound(new ApiResponse(false, 404, $"Product not found with id {id}"));
            }

            await _productService.DeleteAsync(id);

            return Ok(new ApiOkResponse(null));
        }

        [HttpPost, Route("change-status")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusRequest request)
        {
            Product response;
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));
            response = await _productService.ChangeStatus(request.Id);
            return Ok(new ApiOkResponse(response));
        }

        [HttpPost, Route("export")]
        public async Task<IActionResult> Export()
        {
            var result = await _productService.Export();
            var export = _exportExcelService.ExportProduct(result);
            return Ok(new ApiOkResponse(export));
        }

        [HttpPost, Route("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var import = await _exportExcelService.ImportProduct(file);
            var addListProduct = await _productService.AddListAsync(import);
            return Ok(new ApiOkResponse(addListProduct));
        }
    }
}