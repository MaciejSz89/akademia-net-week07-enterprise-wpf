namespace EmployeesWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesSalaryColumnNameChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Salary", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Employees", "Salaries");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Salaries", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.Employees", "Salary");
        }
    }
}
