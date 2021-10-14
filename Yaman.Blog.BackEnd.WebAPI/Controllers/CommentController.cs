using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;

namespace Yaman.Blog.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("{blogId}/[action]")]
        public async Task<IActionResult> GetComments(int blogId, int? parentCommentId)
        {
            var data = await _commentService.GetAllWithSubCommentsAsync(blogId, parentCommentId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentCreateDto dto)
        {
            var responseData = await _commentService.CreateAsync(dto);
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

        [HttpGet]
        public async Task<IActionResult> GetUnApprovedComments()
        {
            return Ok(await _commentService.GetUnApprovedCommentsAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCommentIsApprovedAsync(int id)
        {
            var newData = await _commentService.GetByIdAsync<CommentUpdateDto>(id);
            newData.Data.IsApproved = true;
            var responseData = await _commentService.UpdateAsync(newData.Data);
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var responseData = await _commentService.RemoveAsync(id);
            if (!string.IsNullOrEmpty(responseData.Message))
            {
                return BadRequest(responseData.Message.ToString());
            }
            return NoContent();
        }
    }
}
