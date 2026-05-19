using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace ExpenseTracker.Data.Extensions;

public static class ModelBuilderExtensions
{
	extension(ModelBuilder modelBuilder)
	{
		public void SeedEnumTable<TEnum, TEntity>(Func<TEnum, string, TEntity> entitySelector)
			where TEnum : struct, Enum
			where TEntity : class
		{
			var data = Enum.GetValues<TEnum>().
				Select(e => entitySelector(e, GetEnumDescription(e)));

			modelBuilder.Entity<TEntity>().HasData(data); 
		} 
	}

	private static string GetEnumDescription(Enum value) => value
		.GetType()
		.GetField(value.ToString())
		?.GetCustomAttribute<DescriptionAttribute>()
		?.Description ?? value.ToString();
}

