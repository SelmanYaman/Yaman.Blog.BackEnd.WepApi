using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.Interfaces;

namespace Yaman.Blog.BackEnd.Dtos.CategoryDtos
{
    public class CategoryUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
