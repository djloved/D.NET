using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ONAKU.NET.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueApplications",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false),
                    area = table.Column<string>(nullable: true),
                    battleTag1 = table.Column<string>(nullable: false),
                    battleTag2 = table.Column<string>(nullable: true),
                    battleTag3 = table.Column<string>(nullable: true),
                    battleTag4 = table.Column<string>(nullable: true),
                    champ1 = table.Column<string>(nullable: false),
                    champ2 = table.Column<string>(nullable: true),
                    champ3 = table.Column<string>(nullable: true),
                    discordTag = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    maxForLastSeason = table.Column<int>(nullable: false),
                    maxForThisSeason = table.Column<int>(nullable: false),
                    nickname = table.Column<string>(nullable: false),
                    time1 = table.Column<string>(nullable: true),
                    time2 = table.Column<string>(nullable: true),
                    time3 = table.Column<string>(nullable: true),
                    useMic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueApplications", x => x.uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueApplications");
        }
    }
}
