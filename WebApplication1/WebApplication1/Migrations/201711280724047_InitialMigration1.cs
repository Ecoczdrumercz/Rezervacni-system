namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Credits = c.Int(nullable: false),
                        Email = c.String(),
                        Identity_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Identity_Id)
                .Index(t => t.Identity_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hour = c.DateTime(nullable: false),
                        Place_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playgrounds", t => t.Place_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Place_Id)
                .Index(t => t.Order_Id);
            
            AddColumn("dbo.Playgrounds", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Reservations", "Place_Id", "dbo.Playgrounds");
            DropForeignKey("dbo.Orders", "Owner_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Identity_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "Order_Id" });
            DropIndex("dbo.Reservations", new[] { "Place_Id" });
            DropIndex("dbo.Orders", new[] { "Owner_Id" });
            DropIndex("dbo.Customers", new[] { "Identity_Id" });
            DropColumn("dbo.Playgrounds", "Price");
            DropTable("dbo.Reservations");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
