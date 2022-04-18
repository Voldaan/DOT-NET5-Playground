using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Frontend_and_REST_API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Artist seed
            builder.Entity<Artist>().HasData(
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Name = "Carpenter Brut",
                    Description = "Franck Hueso, better known by his stage name Carpenter Brut, is a French darksynth artist from Poitiers, France. Carpenter Brut claims his relative anonymity is a deliberate artistic choice in order to place more importance on the music itself, rather than the identity of the musician behind it. He started writing music as Carpenter Brut with the intention of mixing sounds from horror films, metal, rock, and electronic music. In live performances Carpenter Brut is joined on stage by guitarist Adrien Grousset and drummer Florent Marcadet, both from the French metal band Hacride and in 2016 Brut toured the United States with the Swedish heavy metal band Ghost."
                },
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Name = "Three Days Grace",
                    Description = "Three Days Grace is a Canadian rock band formed in Norwood, Ontario in 1997. The band's original iteration was called \"Groundswell\" and played in various local Norwood back-yard parties and area establishments from 1993 to 1996. Based in Toronto, the band's original line-up consisted of guitarist and lead vocalist Adam Gontier, drummer and backing vocalist Neil Sanderson, and bassist Brad Walst. In 2003, Barry Stock was recruited as the band's lead guitarist, making them a four - member band.In 2013, Gontier left the band and was replaced by My Darkest Days' vocalist Matt Walst, who is also the younger brother of Brad Walst."
                },
                new Artist
                {
                    Id = Guid.NewGuid(),
                    Name = "If These Trees Could Talk",
                    Description = "If These Trees Could Talk is an instrumental post-rock band from Akron, Ohio. The band self-released their self-titled debut EP in 2006. Independent record label The Mylene Sheath re-released the EP on vinyl in 2007, and went on to release the band's debut studio album, Above the Earth, Below the Sky, on vinyl also, in 2009. The band self-released their second album Red Forest in March 2012, whilst the album's vinyl release went through Science of Silence Records. They went on to follow up the release of \"Red Forest\" with a self-promoted tour throughout Europe in April 2012. The band released its third album, The Bones of a Dying World, in June, 2016 on Metal Blade Records."
                }
            );
            #endregion
            #region Role seed
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "user"
                }
            );
            #endregion
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //Get better info when something goes wrong add migrations
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
    }
}
