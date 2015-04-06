namespace MiniStackOverflow.DataDeployed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Name = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        isAuthenticated = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        Description = c.String(),
                        Votes = c.Int(nullable: false),
                        isCorrect = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerUserId = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Votes = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        QCounterAnswer = c.Int(nullable: false),
                        QCorrectAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Accounts");
        }
    }
}
