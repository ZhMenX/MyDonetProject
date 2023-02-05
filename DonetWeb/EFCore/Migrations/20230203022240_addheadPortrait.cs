using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreModel.Migrations
{
    /// <inheritdoc />
    public partial class addheadPortrait : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "headPortrait",
                table: "Z_Musics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "headPortrait",
                table: "Z_Musics");
        }
    }
}
