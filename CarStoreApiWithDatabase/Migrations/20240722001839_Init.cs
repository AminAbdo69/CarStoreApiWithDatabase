using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recommendtion",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    carsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendtion", x => new { x.UsersId, x.carsId });
                    table.ForeignKey(
                        name: "FK_Recommendtion_Cars_carsId",
                        column: x => x.carsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendtion_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "IsAdmin", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 2, false, true, new byte[] { 198, 198, 234, 121, 98, 75, 116, 178, 104, 91, 68, 92, 214, 241, 6, 190, 59, 254, 233, 56, 205, 118, 246, 196, 213, 134, 63, 84, 79, 38, 171, 216, 224, 187, 249, 2, 242, 22, 180, 213, 58, 51, 231, 239, 152, 237, 140, 84, 155, 124, 161, 166, 147, 121, 111, 101, 17, 133, 146, 245, 22, 51, 52, 144 }, new byte[] { 116, 44, 239, 235, 99, 131, 160, 202, 30, 198, 130, 127, 178, 145, 179, 141, 90, 187, 149, 116, 74, 179, 220, 85, 119, 247, 142, 195, 125, 235, 96, 196, 145, 5, 233, 92, 4, 95, 242, 122, 37, 114, 231, 185, 231, 200, 41, 98, 5, 199, 182, 204, 215, 120, 253, 74, 110, 101, 141, 107, 66, 13, 166, 54, 243, 17, 242, 95, 164, 52, 123, 48, 195, 127, 67, 186, 203, 153, 130, 190, 197, 119, 240, 197, 201, 66, 10, 40, 128, 101, 107, 89, 129, 243, 249, 19, 110, 184, 1, 32, 17, 14, 71, 178, 3, 94, 146, 86, 244, 169, 151, 254, 108, 58, 6, 232, 254, 180, 85, 38, 213, 141, 85, 93, 59, 0, 203, 23 }, "amin" });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendtion_carsId",
                table: "Recommendtion",
                column: "carsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommendtion");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
