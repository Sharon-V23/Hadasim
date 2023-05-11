using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corona_Details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posutuve_result = table.Column<DateTime>(type: "datetime2", nullable: false),
                    recovery_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corona_Details", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patient1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    telephone = table.Column<int>(type: "int", nullable: false),
                    mobile_phone = table.Column<int>(type: "int", nullable: false),
                    id_Corona_Detail = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient1_Corona_Details_id_Corona_Detail",
                        column: x => x.id_Corona_Detail,
                        principalTable: "Corona_Details",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    id_vec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_p = table.Column<int>(type: "int", nullable: false),
                    veccine_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.id_vec);
                    table.ForeignKey(
                        name: "FK_Vaccines_Patient1_id_p",
                        column: x => x.id_p,
                        principalTable: "Patient1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient1_id_Corona_Detail",
                table: "Patient1",
                column: "id_Corona_Detail");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_id_p",
                table: "Vaccines",
                column: "id_p");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "Patient1");

            migrationBuilder.DropTable(
                name: "Corona_Details");
        }
    }
}
