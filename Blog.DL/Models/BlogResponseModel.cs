using Blog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.DL.Models
{
    public class BlogResponseModel : BaseResponseModel
    {
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }
}
