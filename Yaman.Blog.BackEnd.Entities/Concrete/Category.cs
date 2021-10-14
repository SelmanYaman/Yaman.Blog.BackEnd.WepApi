using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.Entities.Concrete
{
    public class Category : BaseTable, ITable
    {
        public string Name { get; set; }
        public List<Blog> Blog { get; set; }

    }
}
