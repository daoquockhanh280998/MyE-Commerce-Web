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
  //  [Authorize]
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


        [HttpPost, Route("all")]
        public async Task<IActionResult> GetAllUser([FromBody] DataTableParameters parameters)
        {
            var result = await _userService.GetAllAsync(parameters);
            return Ok(result);
        }


        [HttpGet, Route("find/{id}")]
        public async Task<IActionResult> GetUsersById(Guid id)
        {
            var response = await _userService.GetAsync(id);
            var model = _mapper.Map<Users>(response);
            return Ok(new ApiOkResponse(model));
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromForm] UserRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var result = await _userService.AddAsync(request);

            return Ok(new ApiOkResponse(result));
        }
    }
}