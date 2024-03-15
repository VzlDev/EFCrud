using EFCrud.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCrud.Data
{
    public partial class TvShowContext : DbContext
    {
        public TvShowContext(DbContextOptions
        <TvShowContext> options)
            : base(options)
        {
        }
        public DbSet<TvShow> TvShows { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TvShow>(entity =>
            {
                entity.HasKey(k => k.Id);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
