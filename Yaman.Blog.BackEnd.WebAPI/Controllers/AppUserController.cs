using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Business.Tools.JWTTool;
using Yaman.Blog.BackEnd.Dtos.AppUserDtos;

namespace Yaman.Blog.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;

        public AppUserController(IAppUserService appUserService, IJwtService jwtService)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appUserService.GetByIdAsync<AppUserListDto>(id);
            return Ok(result.Data);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(AppUserUpdateDto dto)
        {
            var responseData = await _appUserService.UpdateAsync(dto);
            if (responseData.ValidationErrors != null)
            {
                List<string> errorMessage = new();
                foreach (var error in responseData.ValidationErrors)
                {
                    errorMessage.Add(error.ErrorMessage.ToString());
                }
                return BadRequest(errorMessage);
            }
            return NoContent();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {
            var user = await _appUserService.CheckUserAsync(dto);
            if(user != null)
            {
                return Created("", _jwtService.GenerateJwt(user));
            }
            return BadRequest("kullanıcı adı veya şifre hatalı");
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByNameAsync(User.Identity.Name);

            return Ok(new AppUserListDto { Id = user.Id, Name = user.Name, SurName = user.SurName });
        }
    }
}
