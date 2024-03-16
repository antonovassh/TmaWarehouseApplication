namespace TmaWarehouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemMeasurement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupId = c.Int(nullable: false),
                        MeasurementId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Status = c.String(),
                        StorageLocation = c.String(),
                        ContactPerson = c.String(),
                        Photo = c.String(),
                        Measurment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemGroup", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ItemMeasurement", t => t.Measurment_Id)
                .Index(t => t.GroupId)
                .Index(t => t.Measurment_Id);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        ItemId = c.Int(nullable: false),
                        MeasurementId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Comment = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemMeasurement", t => t.MeasurementId, cascadeDelete: true)
                .Index(t => t.MeasurementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "MeasurementId", "dbo.ItemMeasurement");
            DropForeignKey("dbo.Item", "Measurment_Id", "dbo.ItemMeasurement");
            DropForeignKey("dbo.Item", "GroupId", "dbo.ItemGroup");
            DropIndex("dbo.Request", new[] { "MeasurementId" });
            DropIndex("dbo.Item", new[] { "Measurment_Id" });
            DropIndex("dbo.Item", new[] { "GroupId" });
            DropTable("dbo.Request");
            DropTable("dbo.Item");
            DropTable("dbo.ItemMeasurement");
            DropTable("dbo.ItemGroup");
        }
    }
}
