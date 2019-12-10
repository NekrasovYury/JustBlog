namespace JustBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "PostId");
            RenameColumn(table: "dbo.Categories", name: "Id", newName: "PostId");
            RenameIndex(table: "dbo.Categories", name: "IX_Id", newName: "IX_PostId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_PostId", newName: "IX_Id");
            RenameColumn(table: "dbo.Categories", name: "PostId", newName: "Id");
            AddColumn("dbo.Categories", "PostId", c => c.Int(nullable: false));
        }
    }
}
