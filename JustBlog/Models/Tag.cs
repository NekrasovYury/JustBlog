using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Tags { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}