using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoOglasi.Data.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarExtras_Cars_CarId",
                table: "CarExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_TransmissionTypes_TransmissionTypeId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Cars_CarId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_TransmissionTypeId",
                table: "Car",
                newName: "IX_Car_TransmissionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_FuelTypeId",
                table: "Car",
                newName: "IX_Car_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CategoryId",
                table: "Car",
                newName: "IX_Car_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_FuelTypes_FuelTypeId",
                table: "Car",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_TransmissionTypes_TransmissionTypeId",
                table: "Car",
                column: "TransmissionTypeId",
                principalTable: "TransmissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarExtras_Car_CarId",
                table: "CarExtras",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Car_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Car_CarId",
                table: "Posts",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_FuelTypes_FuelTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_TransmissionTypes_TransmissionTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarExtras_Car_CarId",
                table: "CarExtras");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Car_CarId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Car_CarId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Car_TransmissionTypeId",
                table: "Cars",
                newName: "IX_Cars_TransmissionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_FuelTypeId",
                table: "Cars",
                newName: "IX_Cars_FuelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CategoryId",
                table: "Cars",
                newName: "IX_Cars_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarExtras_Cars_CarId",
                table: "CarExtras",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_FuelTypes_FuelTypeId",
                table: "Cars",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_TransmissionTypes_TransmissionTypeId",
                table: "Cars",
                column: "TransmissionTypeId",
                principalTable: "TransmissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarId",
                table: "Images",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Cars_CarId",
                table: "Posts",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
