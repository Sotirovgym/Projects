namespace TeamBuilder.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Models;

    public class TeamBuilderDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        public DbSet<TeamEvent> EventTeams { get; set; }

        TeamBuilderDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public TeamBuilderDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(true);

            builder.Entity<User>()
                .HasAlternateKey(e => e.Username);

            builder.Entity<Team>()
                .HasAlternateKey(e => e.Name);

            builder.Entity<UserTeam>()
                .HasKey(e => new { e.TeamId, e.UserId });

            builder.Entity<User>()
                .Ignore(e => e.UserTeams);

            builder.Entity<TeamEvent>()
                .HasKey(e => new { e.EventId, e.TeamId });

            builder.Entity<Invitation>()
                .HasOne(i => i.InvitedUser)
                .WithMany(iu => iu.ReceivedInvitations)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Invitation>()
                .HasOne(i => i.Team)
                .WithMany(t => t.Invitations)
                .HasForeignKey(i => i.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(ut => ut.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeamEvent>()
                .HasOne(te => te.Team)
                .WithMany(t => t.EventTeams)
                .HasForeignKey(te => te.TeamId)
                .OnDelete(DeleteBehavior.Restrict);           

            builder.Entity<TeamEvent>()
                .HasOne(te => te.Event)
                .WithMany(e => e.EventTeams)
                .HasForeignKey(te => te.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(c => c.CreatedEvents)
                .HasForeignKey(e => e.CreatorId);

        }
    }
}
