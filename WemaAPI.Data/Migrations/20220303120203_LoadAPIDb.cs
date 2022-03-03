using Microsoft.EntityFrameworkCore.Migrations;

namespace WemaAPI.Data.Migrations
{
    public partial class LoadAPIDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateOfResidence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateOfResidence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LGA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalGovernmentArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfResidenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGA_StateOfResidence_StateOfResidenceId",
                        column: x => x.StateOfResidenceId,
                        principalTable: "StateOfResidence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfResidenceId = table.Column<int>(type: "int", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_LGA_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_StateOfResidence_StateOfResidenceId",
                        column: x => x.StateOfResidenceId,
                        principalTable: "StateOfResidence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LGAId",
                table: "Customer",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateOfResidenceId",
                table: "Customer",
                column: "StateOfResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_LGA_StateOfResidenceId",
                table: "LGA",
                column: "StateOfResidenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "LGA");

            migrationBuilder.DropTable(
                name: "StateOfResidence");
        }
    }
}
