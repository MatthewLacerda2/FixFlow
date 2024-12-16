using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixFlow.Server.Migrations {
	/// <inheritdoc />
	public partial class intprice : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AlterColumn<int>(
				name: "price",
				table: "Schedules",
				type: "int",
				nullable: false,
				oldClrType: typeof(double),
				oldType: "double");


			migrationBuilder.AlterColumn<int>(
				name: "price",
				table: "Logs",
				type: "int",
				nullable: false,
				oldClrType: typeof(double),
				oldType: "double");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.AlterColumn<double>(
				name: "price",
				table: "Schedules",
				type: "double",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<double>(
				name: "price",
				table: "Logs",
				type: "double",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int");
		}
	}
}
