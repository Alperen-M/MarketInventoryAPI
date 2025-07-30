using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketInventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carpan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birimler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirimId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urunler_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KullaniciTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_KullaniciTurleri_KullaniciTuruId",
                        column: x => x.KullaniciTuruId,
                        principalTable: "KullaniciTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Barkodlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: true),
                    UrunBirimId = table.Column<int>(type: "int", nullable: false),
                    UrunBirimiId = table.Column<int>(type: "int", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barkodlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barkodlar_Birimler_UrunBirimiId",
                        column: x => x.UrunBirimiId,
                        principalTable: "Birimler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Barkodlar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StokHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(type: "int", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HareketTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirimId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokHareketleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StokHareketleri_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UrunFiyatlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunFiyatlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunFiyatlar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barkodlar_Kod",
                table: "Barkodlar",
                column: "Kod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barkodlar_UrunBirimiId",
                table: "Barkodlar",
                column: "UrunBirimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Barkodlar_UrunId",
                table: "Barkodlar",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_KullaniciTuruId",
                table: "Kullanicilar",
                column: "KullaniciTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_BirimId",
                table: "StokHareketleri",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_UrunId",
                table: "StokHareketleri",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunFiyatlar_UrunId",
                table: "UrunFiyatlar",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_BirimId",
                table: "Urunler",
                column: "BirimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barkodlar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "StokHareketleri");

            migrationBuilder.DropTable(
                name: "UrunFiyatlar");

            migrationBuilder.DropTable(
                name: "KullaniciTurleri");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Birimler");
        }
    }
}
