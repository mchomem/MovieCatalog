using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalog.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_RatingId",
                table: "Movie",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Rating_RatingId",
                table: "Movie",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Rating_RatingId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Movie_RatingId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Movie",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
