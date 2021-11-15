using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportDataAuth.DbContext.Models;


namespace SportDataAuth.DbContext
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        #region DbSet

        // Token
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // Token
            modelBuilder.Entity<RefreshToken>(b =>
            {
                //Columns
                b.Property(x => x.Id).HasColumnType("bigint").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn); ;
                b.Property(x => x.Token).HasColumnType("nvarchar(max)").IsRequired();
                b.Property(x => x.UserId).HasColumnType("nvarchar(max)").IsRequired();

                //Keys
                b.HasKey(x => x.Id);

                //Table Name
                b.ToTable("RefreshToken");

                //Relations
                
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}