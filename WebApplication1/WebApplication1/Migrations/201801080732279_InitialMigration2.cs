namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "Owner_Id", "dbo.Customers");
            DropForeignKey("dbo.Reservations", "Place_Id", "dbo.Playgrounds");
            DropIndex("dbo.Customers", new[] { "Identity_Id" });
            DropIndex("dbo.Orders", new[] { "Owner_Id" });
            DropIndex("dbo.Reservations", new[] { "Place_Id" });
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Identity_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "Owner_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "Place_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Playgrounds", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Playgrounds", "Owner", c => c.String(nullable: false));
            CreateIndex("dbo.Customers", "Identity_Id");
            CreateIndex("dbo.Orders", "Owner_Id");
            CreateIndex("dbo.Reservations", "Place_Id");
            AddForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Owner_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "Place_Id", "dbo.Playgrounds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Place_Id", "dbo.Playgrounds");
            DropForeignKey("dbo.Orders", "Owner_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "Place_Id" });
            DropIndex("dbo.Orders", new[] { "Owner_Id" });
            DropIndex("dbo.Customers", new[] { "Identity_Id" });
            AlterColumn("dbo.Playgrounds", "Owner", c => c.String());
            AlterColumn("dbo.Playgrounds", "Name", c => c.String());
            AlterColumn("dbo.Reservations", "Place_Id", c => c.Int());
            AlterColumn("dbo.Orders", "Owner_Id", c => c.Int());
            AlterColumn("dbo.Customers", "Identity_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Surname", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            CreateIndex("dbo.Reservations", "Place_Id");
            CreateIndex("dbo.Orders", "Owner_Id");
            CreateIndex("dbo.Customers", "Identity_Id");
            AddForeignKey("dbo.Reservations", "Place_Id", "dbo.Playgrounds", "Id");
            AddForeignKey("dbo.Orders", "Owner_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
