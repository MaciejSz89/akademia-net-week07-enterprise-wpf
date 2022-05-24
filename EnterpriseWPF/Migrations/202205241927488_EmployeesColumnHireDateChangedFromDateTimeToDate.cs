namespace EnterpriseWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesColumnHireDateChangedFromDateTimeToDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "HireDate", c => c.DateTime(nullable: false));
        }
    }
}
