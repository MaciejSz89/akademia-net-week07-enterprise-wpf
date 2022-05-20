namespace EnterpriseWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalaryTypeChangedToMoney : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Salaries", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Salaries", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
