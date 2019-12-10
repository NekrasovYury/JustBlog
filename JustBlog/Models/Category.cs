using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class Category
    {
        [Key]
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public string Categorys { get; set; }
        
        public Post Post { get; set; }
    }
}