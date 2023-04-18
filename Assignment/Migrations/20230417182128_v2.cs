using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CartDetails_Cart_CartID",
            //    table: "CartDetails");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "CartID",
            //    table: "CartDetails",
            //    type: "uniqueidentifier",
            //    nullable: true,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CartDetails_Cart_CartID",
            //    table: "CartDetails",
            //    column: "CartID",
            //    principalTable: "Cart",
            //    principalColumn: "CartID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CartDetails_Cart_CartID",
            //    table: "CartDetails");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "CartID",
            //    table: "CartDetails",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier",
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CartDetails_Cart_CartID",
            //    table: "CartDetails",
            //    column: "CartID",
            //    principalTable: "Cart",
            //    principalColumn: "CartID",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
