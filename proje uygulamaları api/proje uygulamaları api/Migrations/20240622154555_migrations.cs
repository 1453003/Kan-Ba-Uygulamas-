using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje_uygulamaları_api.Migrations
{
    /// <inheritdoc />
    public partial class migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hasta",
                columns: table => new
                {
                    ıd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KanGrubu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneAdı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HastaneSifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KacUnite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hasta", x => x.ıd);
                });

            migrationBuilder.CreateTable(
                name: "Kullanıcı",
                columns: table => new
                {
                    kullanıcıId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kullanıcıAdı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kullanıcıSoyadı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kullanıcıKanGrubu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kullanıcıSifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kullanıcıTC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullanıcıKanVermeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanıcı", x => x.kullanıcıId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hasta");

            migrationBuilder.DropTable(
                name: "Kullanıcı");
        }
    }
}
