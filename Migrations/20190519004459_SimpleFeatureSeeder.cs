﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore_Angular_Demo.Migrations
{
    public partial class SimpleFeatureSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature1')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature3')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature4')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature5')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('Feature1', 'Feature2', 'Feature3', 'Feature4', 'Feature5')");
        }
    }
}
