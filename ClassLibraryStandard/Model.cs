using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;

public class Model : DbContext, IContext
{
    private string connectionString;
    public Model(string connectionString) : base()
    {
        this.connectionString = connectionString;
    }

    public virtual DbSet<detail> detalle { get; set; }
    public virtual DbSet<master> maestro { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // fluent API

        modelBuilder.Entity<master>()
            .Property(e => e.Name);

        modelBuilder.Entity<master>()
           .HasKey(x => x.Id);

        modelBuilder.Entity<master>().Property(x => x.Id).ValueGeneratedOnAdd(); //autoincrement

        modelBuilder.Entity<master>()
            .HasMany(e => e.detail).WithOne(e => e.master).IsRequired(true)
            .HasForeignKey(e => e.IdMaster).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<detail>()
           .Property(e => e.Name);

        modelBuilder.Entity<detail>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<detail>()
           .Property(x => x.Id).ValueGeneratedOnAdd(); //autoincrement

        modelBuilder.Entity<detail>()
            .Property(e => e.Name)
            .HasMaxLength(25);
    }
}