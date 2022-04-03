using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatZ.Api.Migrations
{
    public partial class AddConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Tracks");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Tracks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AlbumTrack",
                columns: table => new
                {
                    AlbumsAlbumId = table.Column<int>(type: "int", nullable: false),
                    TracksTrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumTrack", x => new { x.AlbumsAlbumId, x.TracksTrackId });
                    table.ForeignKey(
                        name: "FK_AlbumTrack_Albums_AlbumsAlbumId",
                        column: x => x.AlbumsAlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumTrack_Tracks_TracksTrackId",
                        column: x => x.TracksTrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistTrack",
                columns: table => new
                {
                    ArtistsArtistId = table.Column<int>(type: "int", nullable: false),
                    TracksTrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistTrack", x => new { x.ArtistsArtistId, x.TracksTrackId });
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Artists_ArtistsArtistId",
                        column: x => x.ArtistsArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Tracks_TracksTrackId",
                        column: x => x.TracksTrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumTrack_TracksTrackId",
                table: "AlbumTrack",
                column: "TracksTrackId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistTrack_TracksTrackId",
                table: "ArtistTrack",
                column: "TracksTrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumTrack");

            migrationBuilder.DropTable(
                name: "ArtistTrack");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Tracks");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Tracks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Tracks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Artists_ArtistId",
                table: "Tracks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId");
        }
    }
}
