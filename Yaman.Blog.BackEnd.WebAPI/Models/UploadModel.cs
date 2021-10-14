using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.WebAPI.Enums;

namespace Yaman.Blog.BackEnd.WebAPI.Models
{
    public class UploadModel
    {
        public string NewName { get; set; }
        public string ErrorMessage { get; set; }
        public UploadState UploadState { get; set; }
    }
}
