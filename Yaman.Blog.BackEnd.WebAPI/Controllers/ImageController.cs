using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;

namespace Yaman.Blog.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public ImageController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetBlogImageById(int id)
        {
            var blog = await _blogService.GetByIdAsync<BlogListDto>(id);
            if (string.IsNullOrWhiteSpace(blog.Data.ImagePath))
                return NotFound("Resim Yok");
            return File($"/img/{ blog.Data.ImagePath}", "image/jpeg");
        }
    }
}
