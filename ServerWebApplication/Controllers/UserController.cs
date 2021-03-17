using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEndAPI.Middleware;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerWebApplication.Middleware;

namespace ServerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet, Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllAsync();
            return Ok(new ApiOkResponse(response));
        }


        [HttpGet, Route("find/{id}")]
        public async Task<IActionResult> GetUsersById(Guid id)
        {
            var response = await _userService.GetAsync(id);
            var model = _mapper.Map<Users>(response);
            return Ok(new ApiOkResponse(model));
        }

        //[HttpPost, Route("update/{id}")]
        //public async Task<IActionResult> UpdateUsers([FromBody] RegisterRequest request, Guid id)
        //{
        //    if (!ModelState.IsValid || request == null)
        //        return BadRequest(new ApiBadRequestResponse(ModelState));

        //    var model = _mapper.Map<Users>(request);
        //    var response = await _userService.UpdateAsync(model, id);
        //    return Ok(new ApiOkResponse(response));
        //}

        //[HttpPost, Route("delete/{id}")]
        //public async Task<IActionResult> DeleteUsersById(Guid id)
        //{
        //    if (id == null || id == Guid.Empty)
        //    {
        //        return NotFound(new ApiResponse(false, 404, $"Users is not found with id {id}"));
        //    }

        //    var model = await _userService.GetAsync(id);
        //    if (model == null)
        //    {
        //        return NotFound(new ApiResponse(false, 404, $"Users not found with id {id}"));
        //    }

        //    await _userService.DeleteAsync(id);

        //    return Ok(new ApiOkResponse(null));
        //}
    }
}