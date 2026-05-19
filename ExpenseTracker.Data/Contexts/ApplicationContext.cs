using ExpenseTracker.Data.Extensions;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Contexts;

public class ApplicationContext : DbContext
{
	public ApplicationContext() { }

	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

	public DbSet<LineItem> LineItems { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=expensetracker; Username=postgres; Password=root"); 
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.SeedEnumTable<ItemType, LineItemType>(
			(id, name) => new LineItemType { Id = id, Name = name }); 
	}

}
