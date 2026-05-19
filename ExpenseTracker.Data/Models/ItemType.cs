using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Data.Models;

public enum ItemType
{ 
	Gas = 0,
	Groceries = 1,
	[Description("Fast Food")]
	FastFood = 2,
	Entertainment= 3,
	Bills= 4,
	Cash= 5,
	Shopping= 6,
	Gifts= 7,
	Automotive= 8,
	Health= 9,
	Home= 10,
	Travel= 11,
	[Description("Professional Services")]
	ProfessionalServices = 12,
	Education = 13
};

public class LineItemType
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public ItemType Id { get; set; }
	public required string Name { get; set; } 
}
