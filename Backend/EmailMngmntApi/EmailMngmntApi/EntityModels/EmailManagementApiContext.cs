using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmailMngmntApi.EntityModels
{
    public partial class EmailManagementApiContext : DbContext
    {
        public EmailManagementApiContext()
        {
        }

        public EmailManagementApiContext(DbContextOptions<EmailManagementApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientApp> ClientApps { get; set; } = null!;
        public virtual DbSet<SentEmail> SentEmails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DELL5520;Database=EmailManagementApi;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientApp>(entity =>
            {
                entity.Property(e => e.ClientAppId).ValueGeneratedNever();

                entity.Property(e => e.AppDisplayName).HasMaxLength(100);

                entity.Property(e => e.AppKey).HasMaxLength(100);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.SendGridKey).HasMaxLength(500);
            });

            modelBuilder.Entity<SentEmail>(entity =>
            {
                entity.Property(e => e.SentEmailId).ValueGeneratedNever();

                entity.Property(e => e.From).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.To).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.ClientApp)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ClientAppId)
                    .HasConstraintName("FK_Users_ClientApps");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
