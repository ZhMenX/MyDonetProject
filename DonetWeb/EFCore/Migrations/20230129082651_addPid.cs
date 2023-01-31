using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModel.Migrations
{
    /// <inheritdoc />
    public partial class addPid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pid",
                table: "Z_Musics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pid",
                table: "Z_Musics");
        }
    }
}
