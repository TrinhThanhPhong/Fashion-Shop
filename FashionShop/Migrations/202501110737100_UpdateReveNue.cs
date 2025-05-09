﻿namespace FashionShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReveNue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Product", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Product", "OriginalPrice");
        }
    }
}
