using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.Entities.Concrete
{
    public class Blog : BaseTable, ITable
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public List<Comment> Comments { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
