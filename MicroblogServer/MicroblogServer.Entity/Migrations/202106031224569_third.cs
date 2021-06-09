namespace MicroblogServer.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TweetTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TweetId = c.Guid(nullable: false),
                        TagName = c.String(),
                        SearchCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TweetTags");
        }
    }
}
