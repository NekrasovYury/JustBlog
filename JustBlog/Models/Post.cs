using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Short_Description { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "image")]
        public byte[] Hero_image { get; set; }
        [Required]
        public string Category { get; set; }
        public string Tags { get; set; }
        [Required]
        public DateTime PostDate { get; set; }

    }
}