using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ONAKU.NET.Models;

namespace ONAKU.NET.Migrations
{
    [DbContext(typeof(LeagueApplicationContext))]
    partial class LeagueApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ONAKU.NET.Models.LeagueApplication", b =>
                {
                    b.Property<Guid>("uid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OnCreated");

                    b.Property<string>("area");

                    b.Property<string>("battleTag1")
                        .IsRequired();

                    b.Property<string>("battleTag2");

                    b.Property<string>("battleTag3");

                    b.Property<string>("battleTag4");

                    b.Property<string>("champ1")
                        .IsRequired();

                    b.Property<string>("champ2");

                    b.Property<string>("champ3");

                    b.Property<string>("discordTag");

                    b.Property<string>("email");

                    b.Property<int>("maxForLastSeason");

                    b.Property<int>("maxForThisSeason");

                    b.Property<string>("nickname")
                        .IsRequired();

                    b.Property<string>("time1");

                    b.Property<string>("time2");

                    b.Property<string>("time3");

                    b.Property<bool>("useMic");

                    b.HasKey("uid");

                    b.ToTable("LeagueApplications");
                });
        }
    }
}
