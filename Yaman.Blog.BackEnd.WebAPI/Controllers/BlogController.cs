using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;
using Yaman.Blog.BackEnd.WebAPI.Models;

namespace Yaman.Blog.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responseData = await _blogService.GetAllAsync();
            return Ok(responseData.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _blogService.GetByIdAsync<BlogListDto>(id);
            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] BlogCreateDto dto)
        {
            dto.AppUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<string> errors = new();
            if (dto.Image == null)
            {
                errors.Add("Resim Eklemek Zorundasınız.");
                return BadRequest(errors);
            }
            else if (dto.Image.ContentType != "image/jpeg")
            {
                errors.Add("Resim uzantısı jpg/jpeg olabilir. Uygunsuz Dosya Uzantısı.");
                return BadRequest(errors);
            }

            var newName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
            dto.ImagePath = newName;
            var responseData = await _blogService.CreateAsync(dto);
            if (responseData.ValidationErrors != null)
            {
                foreach (var error in responseData.ValidationErrors)
                {
                    errors.Add(error.ErrorMessage);
                }
                return BadRequest(errors);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newName);
            var stream = new FileStream(path, FileMode.Create);
            await dto.Image.CopyToAsync(stream);
            return Created("", dto);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] BlogUpdateDto dto)
        {
            dto.AppUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var image = await _blogService.GetByIdAsync<BlogListDto>(dto.Id);
            if (dto.Image == null)
            {
                dto.ImagePath = image.Data.ImagePath;
            }
            else
            {
                //eski resmi sil
                System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/img/" + image.Data.ImagePath);
                var newName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                dto.ImagePath = newName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + newName);
                var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);
            }
            var responseData = await _blogService.UpdateAsync(dto);
            if (string.IsNullOrEmpty(responseData.Data.Title))
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
            var image = await _blogService.GetByIdAsync<BlogListDto>(id);
            var responseData = await _blogService.RemoveAsync(id);
            if (!string.IsNullOrEmpty(responseData.Message))
            {
                return BadRequest(responseData.Message.ToString());
            }
            System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/img/" + image.Data.ImagePath);
            return NoContent();
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastFive()
        {
            var data = await _blogService.GetLastFiveAsync();
            return Ok(data);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            return Ok(await _blogService.GetAllByCategoriesIdAsync(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery]string s)
        {
            var data = await _blogService.SearchAsync(s);
            var ss = data;
            return Ok(ss);
        }
    }
}
