using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Data.Models;

public class LineItem
{
	[Key]
	public int Id { get; set; } 
	public DateTimeOffset Timestamp { get; set; }

	[Precision(18, 2)]
	public decimal Amount { get; set; }
	public ItemType Type { get; set; }

	[Unicode(false)]
	[StringLength(1500)]
	public string? Description { get; set; }
}

