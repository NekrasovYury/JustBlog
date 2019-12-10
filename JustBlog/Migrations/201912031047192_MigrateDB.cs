namespace JustBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Categorys = c.String(),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tags = c.String(),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            DropColumn("dbo.Posts", "Category");
            DropColumn("dbo.Posts", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String());
            AddColumn("dbo.Posts", "Category", c => c.String(nullable: false));
            DropForeignKey("dbo.Categories", "Id", "dbo.Posts");
            DropForeignKey("dbo.Tags", "PostId", "dbo.Posts");
            DropIndex("dbo.Tags", new[] { "PostId" });
            DropIndex("dbo.Categories", new[] { "Id" });
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
        }
    }
}
