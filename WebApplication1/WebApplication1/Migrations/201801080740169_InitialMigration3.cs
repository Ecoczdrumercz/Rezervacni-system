namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "Identity_Id" });
            AlterColumn("dbo.Customers", "Identity_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "Identity_Id");
            AddForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "Identity_Id" });
            AlterColumn("dbo.Customers", "Identity_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Customers", "Identity_Id");
            AddForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
