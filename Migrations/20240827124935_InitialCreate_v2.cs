using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationTables",
                table: "ReservationTables");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTableID",
                table: "ReservationTables",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationTables",
                table: "ReservationTables",
                column: "ReservationTableID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTables_ReservationID",
                table: "ReservationTables",
                column: "ReservationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationTables",
                table: "ReservationTables");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTables_ReservationID",
                table: "ReservationTables");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationTableID",
                table: "ReservationTables",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationTables",
                table: "ReservationTables",
                columns: new[] { "ReservationID", "TableID" });
        }
    }
}
