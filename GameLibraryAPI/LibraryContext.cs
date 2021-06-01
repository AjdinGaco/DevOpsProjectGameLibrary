using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagsLink> TagLink { get; set; }
        public DbSet<GameScores> GameScores { get; set; }
    }
}
