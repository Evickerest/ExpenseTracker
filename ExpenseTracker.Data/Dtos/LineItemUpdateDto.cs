using ExpenseTracker.Data.Models;

namespace ExpenseTracker.Data.Dtos;

public class LineItemUpdateDto
{ 
	public DateTimeOffset Timestamp { get; set; } 
	public decimal Amount { get; set; }
	public ItemType Type { get; set; } 
	public string? Description { get; set; }
}
