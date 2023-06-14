using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashingCar.Migrations
{
    /// <inheritdoc />
    public partial class EditTableVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Services_ServiceId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_NumberPlate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Vehicles",
                newName: "ServiceID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ServiceId",
                table: "Vehicles",
                newName: "IX_Vehicles_ServiceID");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceID",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Id",
                table: "Vehicles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Services_ServiceID",
                table: "Vehicles",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Services_ServiceID",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Id",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ServiceID",
                table: "Vehicles",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ServiceID",
                table: "Vehicles",
                newName: "IX_Vehicles_ServiceId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_NumberPlate",
                table: "Vehicles",
                column: "NumberPlate",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Services_ServiceId",
                table: "Vehicles",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
