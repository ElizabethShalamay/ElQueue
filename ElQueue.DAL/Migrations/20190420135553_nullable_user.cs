using Microsoft.EntityFrameworkCore.Migrations;

namespace ElQueue.DAL.Migrations
{
    public partial class nullable_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TimeSlots",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TimeSlots",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
