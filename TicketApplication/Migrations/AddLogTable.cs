using Microsoft.AspNetCore.Http.HttpResults;
using FluentMigrator;

namespace TicketApplication.Migrations
{
    [Migration(20240810000000)]
    public partial class AddGhiseuAndBonTables : Migration
    {
        public override void Up()
        {
            Create.Schema("bon");

            Create.Table("Ghiseu").InSchema("bon")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Cod").AsString().NotNullable()
                .WithColumn("Denumire").AsString().NotNullable()
                .WithColumn("Descriere").AsString().Nullable()
                .WithColumn("Icon").AsString().Nullable()
                .WithColumn("Activ").AsBoolean().NotNullable();

            Create.Table("Bon").InSchema("bon")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("IdGhiseu").AsInt32().NotNullable()
                .WithColumn("Stare").AsString().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("ModifiedAt").AsDateTime().NotNullable();

            Create.ForeignKey("FK_Bon_Ghiseu")
                .FromTable("Bon").InSchema("bon").ForeignColumn("IdGhiseu")
                .ToTable("Ghiseu").InSchema("bon").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Bon_Ghiseu").OnTable("Bon").InSchema("bon");
            Delete.Table("Bon").InSchema("bon");
            Delete.Table("Ghiseu").InSchema("bon");
            Delete.Schema("bon");
        }
    }
}