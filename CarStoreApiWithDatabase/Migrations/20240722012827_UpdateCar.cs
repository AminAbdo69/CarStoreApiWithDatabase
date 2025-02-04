﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 211, 59, 245, 130, 53, 188, 199, 202, 94, 192, 213, 199, 224, 166, 131, 73, 85, 100, 249, 243, 0, 170, 25, 121, 69, 188, 136, 188, 221, 151, 129, 38, 110, 86, 62, 231, 21, 28, 202, 147, 191, 174, 119, 23, 46, 244, 80, 66, 209, 236, 149, 218, 102, 151, 85, 159, 70, 159, 232, 45, 112, 188, 238, 156 }, new byte[] { 165, 221, 149, 44, 38, 12, 66, 6, 56, 204, 83, 202, 29, 123, 10, 38, 54, 27, 128, 122, 66, 172, 93, 96, 71, 200, 231, 196, 82, 255, 96, 124, 78, 103, 124, 236, 167, 183, 211, 232, 137, 57, 171, 56, 42, 223, 72, 91, 196, 120, 89, 31, 233, 244, 51, 252, 69, 73, 81, 124, 252, 196, 22, 4, 206, 32, 242, 70, 189, 160, 93, 63, 119, 241, 234, 179, 136, 185, 54, 243, 169, 237, 1, 147, 66, 60, 218, 217, 3, 29, 252, 231, 200, 228, 232, 57, 168, 92, 109, 108, 223, 30, 128, 32, 221, 151, 191, 193, 105, 169, 161, 43, 223, 126, 61, 125, 245, 212, 83, 241, 122, 109, 27, 202, 178, 5, 216, 175 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 198, 198, 234, 121, 98, 75, 116, 178, 104, 91, 68, 92, 214, 241, 6, 190, 59, 254, 233, 56, 205, 118, 246, 196, 213, 134, 63, 84, 79, 38, 171, 216, 224, 187, 249, 2, 242, 22, 180, 213, 58, 51, 231, 239, 152, 237, 140, 84, 155, 124, 161, 166, 147, 121, 111, 101, 17, 133, 146, 245, 22, 51, 52, 144 }, new byte[] { 116, 44, 239, 235, 99, 131, 160, 202, 30, 198, 130, 127, 178, 145, 179, 141, 90, 187, 149, 116, 74, 179, 220, 85, 119, 247, 142, 195, 125, 235, 96, 196, 145, 5, 233, 92, 4, 95, 242, 122, 37, 114, 231, 185, 231, 200, 41, 98, 5, 199, 182, 204, 215, 120, 253, 74, 110, 101, 141, 107, 66, 13, 166, 54, 243, 17, 242, 95, 164, 52, 123, 48, 195, 127, 67, 186, 203, 153, 130, 190, 197, 119, 240, 197, 201, 66, 10, 40, 128, 101, 107, 89, 129, 243, 249, 19, 110, 184, 1, 32, 17, 14, 71, 178, 3, 94, 146, 86, 244, 169, 151, 254, 108, 58, 6, 232, 254, 180, 85, 38, 213, 141, 85, 93, 59, 0, 203, 23 } });
        }
    }
}
