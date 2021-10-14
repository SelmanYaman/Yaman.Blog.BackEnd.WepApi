using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;

namespace Yaman.Blog.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var responseData = await _categoryService.CreateAsync(dto);
            if(string.IsNullOrEmpty(responseData.Data.Name))
            {
                List<string> errorMessage = new();
                foreach (var error in responseData.ValidationErrors)
                {
                    errorMessage.Add(error.ErrorMessage.ToString());
                }
                return BadRequest(errorMessage);
            }
            return Created("", dto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync<CategoryListDto>(id);
            return Ok(result.Data);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CategoryUpdateDto dto)
        {
            var responseData = await _categoryService.UpdateAsync(dto);
            if(string.IsNullOrEmpty(responseData.Data.Name))
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var responseData = await _categoryService.RemoveAsync(id);
            if(!string.IsNullOrEmpty(responseData.Message))
            {
                return BadRequest(responseData.Message.ToString());
            }
            return NoContent();
        }
    }
}
