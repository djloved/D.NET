using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ONAKU.NET.Models
{
    public class LeagueApplicationContext : DbContext
    {
        public LeagueApplicationContext(DbContextOptions<LeagueApplicationContext> options)
            : base(options)
        { }
        public LeagueApplicationContext()
            :base()
        {

        }
        public DbSet<LeagueApplication> LeagueApplications { get; set; }
    }

    public class LeagueApplication
    {        
        [Key]
        public Guid uid { get; set; }
        [Required]
        public string nickname { get; set; }
        [Required]
        public string battleTag1 { get; set; }
        public string battleTag2 { get; set; }
        public string battleTag3 { get; set; }
        public string battleTag4 { get; set; }
        [Required]
        public int maxForLastSeason { get; set; }
        [Required]
        public int maxForThisSeason { get; set; }
        [Required]
        public string champ1 { get; set; }
        public string champ2 { get; set; }
        public string champ3 { get; set; }
        public string area { get; set; }
        public string time1 { get; set; }
        public string time2 { get; set; }
        public string time3 { get; set; }
        public bool useMic { get; set; }
        public string discordTag { get; set; }
        public string email { get; set; }
        public DateTime OnCreated { get; set; }
    }
}
