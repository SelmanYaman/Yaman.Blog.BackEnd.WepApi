using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.Entities.Concrete
{
    public class AppUser : BaseTable, ITable
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
