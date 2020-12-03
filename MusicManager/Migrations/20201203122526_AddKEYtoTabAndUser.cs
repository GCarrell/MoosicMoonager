using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicManager.Migrations
{
    public partial class AddKEYtoTabAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tabs",
                columns: table => new
                {
                    TabId = table.Column<int>(type: "int", nullable: false),
                    TabName = table.Column<string>(type: "varchar(48)", unicode: false, maxLength: 48, nullable: false),
                    BandName = table.Column<string>(type: "varchar(48)", unicode: false, maxLength: 48, nullable: false),
                    Instrument = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    TabUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TabCreator = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.TabId);
                    table.ForeignKey(
                        name: "FK__Tabs__TabCreator__38996AB5",
                        column: x => x.TabCreator,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    TabId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Favourite__TabId__3A81B327",
                        column: x => x.TabId,
                        principalTable: "Tabs",
                        principalColumn: "TabId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Favourite__UserI__3B75D760",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TabId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Ratings__TabId__3D5E1FD2",
                        column: x => x.TabId,
                        principalTable: "Tabs",
                        principalColumn: "TabId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Ratings__UserId__3E52440B",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_TabId",
                table: "Favourites",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TabId",
                table: "Ratings",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_TabCreator",
                table: "Tabs",
                column: "TabCreator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tabs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
