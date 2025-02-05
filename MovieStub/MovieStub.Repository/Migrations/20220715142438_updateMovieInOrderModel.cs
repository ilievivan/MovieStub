﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStub.Repository.Migrations
{
    public partial class updateMovieInOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MovieInOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MovieInOrder");
        }
    }
}
