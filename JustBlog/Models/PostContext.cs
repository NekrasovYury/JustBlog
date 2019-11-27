using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class PostContext : DbContext
    {
        public PostContext() : base("JustBlog")
        { }

        public DbSet<Post> Posts { get; set; }
    }
}