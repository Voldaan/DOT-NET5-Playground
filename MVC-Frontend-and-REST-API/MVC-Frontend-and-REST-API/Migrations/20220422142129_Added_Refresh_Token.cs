using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Frontend_and_REST_API.Migrations
{
    public partial class Added_Refresh_Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("4624393b-e929-4304-9a96-2223c1066f39"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("96b65129-64ac-4848-9858-0d0724286540"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("e161b950-d563-4958-b9fb-f8b4af3d0e97"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7409720a-4f3c-47ad-94b2-b04f95c9b14a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97996e2b-acaa-4e1e-9b62-79995975ac25");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("afdb4890-08ae-4620-8cf7-65778255cac0"), "Franck Hueso, better known by his stage name Carpenter Brut, is a French darksynth artist from Poitiers, France. Carpenter Brut claims his relative anonymity is a deliberate artistic choice in order to place more importance on the music itself, rather than the identity of the musician behind it. He started writing music as Carpenter Brut with the intention of mixing sounds from horror films, metal, rock, and electronic music. In live performances Carpenter Brut is joined on stage by guitarist Adrien Grousset and drummer Florent Marcadet, both from the French metal band Hacride and in 2016 Brut toured the United States with the Swedish heavy metal band Ghost.", "Carpenter Brut" },
                    { new Guid("fb09be72-d69d-4f22-8812-6e334177c29c"), "Three Days Grace is a Canadian rock band formed in Norwood, Ontario in 1997. The band's original iteration was called \"Groundswell\" and played in various local Norwood back-yard parties and area establishments from 1993 to 1996. Based in Toronto, the band's original line-up consisted of guitarist and lead vocalist Adam Gontier, drummer and backing vocalist Neil Sanderson, and bassist Brad Walst. In 2003, Barry Stock was recruited as the band's lead guitarist, making them a four - member band.In 2013, Gontier left the band and was replaced by My Darkest Days' vocalist Matt Walst, who is also the younger brother of Brad Walst.", "Three Days Grace" },
                    { new Guid("6fef5695-c273-42d7-a117-daf3907cb2ad"), "If These Trees Could Talk is an instrumental post-rock band from Akron, Ohio. The band self-released their self-titled debut EP in 2006. Independent record label The Mylene Sheath re-released the EP on vinyl in 2007, and went on to release the band's debut studio album, Above the Earth, Below the Sky, on vinyl also, in 2009. The band self-released their second album Red Forest in March 2012, whilst the album's vinyl release went through Science of Silence Records. They went on to follow up the release of \"Red Forest\" with a self-promoted tour throughout Europe in April 2012. The band released its third album, The Bones of a Dying World, in June, 2016 on Metal Blade Records.", "If These Trees Could Talk" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b9f8bec-9ddb-4be7-816f-29d90dee2253", "9e035738-0130-4ae1-bfa7-3e5345ac9291", "Admin", "admin" },
                    { "ff07633b-595c-4770-ac52-a6726ebded82", "b17d0631-aa70-46bf-bf43-8a2891d5e1ad", "User", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("6fef5695-c273-42d7-a117-daf3907cb2ad"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("afdb4890-08ae-4620-8cf7-65778255cac0"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fb09be72-d69d-4f22-8812-6e334177c29c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b9f8bec-9ddb-4be7-816f-29d90dee2253");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff07633b-595c-4770-ac52-a6726ebded82");

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("96b65129-64ac-4848-9858-0d0724286540"), "Franck Hueso, better known by his stage name Carpenter Brut, is a French darksynth artist from Poitiers, France. Carpenter Brut claims his relative anonymity is a deliberate artistic choice in order to place more importance on the music itself, rather than the identity of the musician behind it. He started writing music as Carpenter Brut with the intention of mixing sounds from horror films, metal, rock, and electronic music. In live performances Carpenter Brut is joined on stage by guitarist Adrien Grousset and drummer Florent Marcadet, both from the French metal band Hacride and in 2016 Brut toured the United States with the Swedish heavy metal band Ghost.", "Carpenter Brut" },
                    { new Guid("4624393b-e929-4304-9a96-2223c1066f39"), "Three Days Grace is a Canadian rock band formed in Norwood, Ontario in 1997. The band's original iteration was called \"Groundswell\" and played in various local Norwood back-yard parties and area establishments from 1993 to 1996. Based in Toronto, the band's original line-up consisted of guitarist and lead vocalist Adam Gontier, drummer and backing vocalist Neil Sanderson, and bassist Brad Walst. In 2003, Barry Stock was recruited as the band's lead guitarist, making them a four - member band.In 2013, Gontier left the band and was replaced by My Darkest Days' vocalist Matt Walst, who is also the younger brother of Brad Walst.", "Three Days Grace" },
                    { new Guid("e161b950-d563-4958-b9fb-f8b4af3d0e97"), "If These Trees Could Talk is an instrumental post-rock band from Akron, Ohio. The band self-released their self-titled debut EP in 2006. Independent record label The Mylene Sheath re-released the EP on vinyl in 2007, and went on to release the band's debut studio album, Above the Earth, Below the Sky, on vinyl also, in 2009. The band self-released their second album Red Forest in March 2012, whilst the album's vinyl release went through Science of Silence Records. They went on to follow up the release of \"Red Forest\" with a self-promoted tour throughout Europe in April 2012. The band released its third album, The Bones of a Dying World, in June, 2016 on Metal Blade Records.", "If These Trees Could Talk" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7409720a-4f3c-47ad-94b2-b04f95c9b14a", "dece547e-1025-4547-9a47-49ae3b2360dc", "Admin", "admin" },
                    { "97996e2b-acaa-4e1e-9b62-79995975ac25", "cf327917-afa4-4eb9-b4e1-8a49101fd765", "User", "user" }
                });
        }
    }
}
