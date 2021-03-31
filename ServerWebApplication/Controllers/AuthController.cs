using AutoMapper;
using CS.Core.Service.Interfaces;
using CS.EF.Models;
using CS.VM.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerWebApplication.Middleware;
using System.Threading.Tasks;

namespace ServerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost, Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var U = _mapper.Map<Users>(request);
            var response = await _userService.Register(U);

            return Ok(new ApiOkResponse(response));
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Authetate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var response = await _userService.Authetate(request);

            return Ok(new ApiOkResponse(response));
        }
    }
}
