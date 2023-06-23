using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__COURSES__3214EC27CCBD0248", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TEACHERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TEACHERS__3214EC2798CA96E2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUPS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COURSE_ID = table.Column<int>(type: "int", nullable: true),
                    NAME = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TEACHER_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GROUPS__3214EC27B9D9CF4D", x => x.ID);
                    table.ForeignKey(
                        name: "FK__GROUPS__COURSE_I__4BAC3F29",
                        column: x => x.COURSE_ID,
                        principalTable: "COURSES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "GROUPS_TEACHERS_ID_fk",
                        column: x => x.TEACHER_ID,
                        principalTable: "TEACHERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GROUP_ID = table.Column<int>(type: "int", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STUDENTS__3214EC273809DBFF", x => x.ID);
                    table.ForeignKey(
                        name: "FK__STUDENTS__GROUP___5CD6CB2B",
                        column: x => x.GROUP_ID,
                        principalTable: "GROUPS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "GROUPS_pk",
                table: "GROUPS",
                column: "TEACHER_ID",
                unique: true,
                filter: "[TEACHER_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_COURSE_ID",
                table: "GROUPS",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_GROUP_ID",
                table: "STUDENTS",
                column: "GROUP_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "GROUPS");

            migrationBuilder.DropTable(
                name: "COURSES");

            migrationBuilder.DropTable(
                name: "TEACHERS");
        }
    }
}
