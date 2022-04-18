using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_Frontend_and_REST_API.Migrations
{
    public partial class AddedRefreshTokens : Migration
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
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    { new Guid("6a69b145-5cf4-4dc5-875f-5b092f7724cc"), "Franck Hueso, better known by his stage name Carpenter Brut, is a French darksynth artist from Poitiers, France. Carpenter Brut claims his relative anonymity is a deliberate artistic choice in order to place more importance on the music itself, rather than the identity of the musician behind it. He started writing music as Carpenter Brut with the intention of mixing sounds from horror films, metal, rock, and electronic music. In live performances Carpenter Brut is joined on stage by guitarist Adrien Grousset and drummer Florent Marcadet, both from the French metal band Hacride and in 2016 Brut toured the United States with the Swedish heavy metal band Ghost.", "Carpenter Brut" },
                    { new Guid("986bf532-f3fd-4a69-ba7d-ac872c40dd8f"), "Three Days Grace is a Canadian rock band formed in Norwood, Ontario in 1997. The band's original iteration was called \"Groundswell\" and played in various local Norwood back-yard parties and area establishments from 1993 to 1996. Based in Toronto, the band's original line-up consisted of guitarist and lead vocalist Adam Gontier, drummer and backing vocalist Neil Sanderson, and bassist Brad Walst. In 2003, Barry Stock was recruited as the band's lead guitarist, making them a four - member band.In 2013, Gontier left the band and was replaced by My Darkest Days' vocalist Matt Walst, who is also the younger brother of Brad Walst.", "Three Days Grace" },
                    { new Guid("9476140b-7777-41c9-8a85-91d7f864aab9"), "If These Trees Could Talk is an instrumental post-rock band from Akron, Ohio. The band self-released their self-titled debut EP in 2006. Independent record label The Mylene Sheath re-released the EP on vinyl in 2007, and went on to release the band's debut studio album, Above the Earth, Below the Sky, on vinyl also, in 2009. The band self-released their second album Red Forest in March 2012, whilst the album's vinyl release went through Science of Silence Records. They went on to follow up the release of \"Red Forest\" with a self-promoted tour throughout Europe in April 2012. The band released its third album, The Bones of a Dying World, in June, 2016 on Metal Blade Records.", "If These Trees Could Talk" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c0636851-3cbe-4c9b-a06b-e887eded2c9e", "870a73af-c6f8-4dcd-8f4c-48461de7aa7d", "Admin", "admin" },
                    { "b6416a7b-41a8-4a94-a39d-acdaeca3b4db", "e4111ec4-c605-4f8b-853e-38b59fb3fd33", "User", "user" }
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
                keyValue: new Guid("6a69b145-5cf4-4dc5-875f-5b092f7724cc"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("9476140b-7777-41c9-8a85-91d7f864aab9"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("986bf532-f3fd-4a69-ba7d-ac872c40dd8f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6416a7b-41a8-4a94-a39d-acdaeca3b4db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0636851-3cbe-4c9b-a06b-e887eded2c9e");

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
