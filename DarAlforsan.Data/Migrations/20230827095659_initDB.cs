using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarAlforsan.Data.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutSchool",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddressEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    WhatsAppNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DetailsAr = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DetailsEn = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Lng = table.Column<float>(type: "real", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegisterationUrl = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutSchool", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DetailsAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DetailsEn = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MainImg = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Imgs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceModule",
                columns: table => new
                {
                    ModuleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LocalName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceModule", x => x.ModuleID);
                });

            migrationBuilder.CreateTable(
                name: "SaidAboutUs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MessageAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MessageEn = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaidAboutUs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolFruits",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolFruits", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSystem",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MessageAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MessageEn = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSystem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolVision",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MessageAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MessageEn = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolVision", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DetailsAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DetailsEn = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThemeLogin",
                columns: table => new
                {
                    ThemeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    BGImgPath = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    LogoImgPath = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    SideImgPath = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    SideImgPadding = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    LogoImgPadding = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ForeColor = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeLogin", x => x.ThemeID);
                });

            migrationBuilder.CreateTable(
                name: "UILanguage",
                columns: table => new
                {
                    LanguageID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LocalName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LatinName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ISOLanguageName = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UILanguage", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LocalValue = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    LatinValue = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ModuleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceID);
                    table.ForeignKey(
                        name: "FK_Resource_ResourceModule_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "ResourceModule",
                        principalColumn: "ModuleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolFruitsDetails",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FruitsID = table.Column<long>(type: "bigint", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolFruitsDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchoolFruitsDetails_SchoolFruits_FruitsID",
                        column: x => x.FruitsID,
                        principalTable: "SchoolFruits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    PhotoName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DeviceID = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    AddDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UserClassRepeated = table.Column<int>(type: "int", nullable: false),
                    MaxPeriodCountInDay = table.Column<int>(type: "int", nullable: false),
                    UILanguageID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VClassUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Ishidden = table.Column<bool>(type: "bit", nullable: false),
                    SignatureFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_UILanguage_UILanguageID",
                        column: x => x.UILanguageID,
                        principalTable: "UILanguage",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ModuleID",
                table: "Resource",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolFruitsDetails_FruitsID",
                table: "SchoolFruitsDetails",
                column: "FruitsID");

            migrationBuilder.CreateIndex(
                name: "IX_User_BranchID",
                table: "User",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UILanguageID",
                table: "User",
                column: "UILanguageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutSchool");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "SaidAboutUs");

            migrationBuilder.DropTable(
                name: "SchoolFruitsDetails");

            migrationBuilder.DropTable(
                name: "SchoolSystem");

            migrationBuilder.DropTable(
                name: "SchoolVision");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "ThemeLogin");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ResourceModule");

            migrationBuilder.DropTable(
                name: "SchoolFruits");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "UILanguage");
        }
    }
}
