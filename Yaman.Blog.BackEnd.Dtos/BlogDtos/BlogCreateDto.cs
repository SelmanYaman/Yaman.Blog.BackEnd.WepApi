using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.Interfaces;

namespace Yaman.Blog.BackEnd.Dtos.BlogDtos
{
    public class BlogCreateDto : IDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public IFormFile Image { get; set; }
        public int AppUserId { get; set; }
        public int CategoryId { get; set; }
    }
}
