using AdminApp.EF;
using AutoMapper;
using BackEndAPI.Middleware;
using CS.Core.Service.Interfaces;
using CS.VM.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerWebApplication.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }
        [HttpGet, Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            return Ok(new ApiOkResponse(response));
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _mapper.Map<Product>(request);
            var response = await _productService.AddAsync(model);

            return Ok(new ApiOkResponse(response));
        }
        [HttpGet, Route("find/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = await  _productService.GetAsync(id);
            var model = _mapper.Map<Product>(response);
            return Ok(new ApiOkResponse(model));
        }

        [HttpPost, Route("update/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest request , Guid id)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var model = _mapper.Map<Product>(request);
            var response = await _productService.UpdateAsync(model, id);
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
    }
}
