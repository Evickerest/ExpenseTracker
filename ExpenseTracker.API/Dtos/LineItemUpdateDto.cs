using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker.API.Dtos;

public class LineItemUpdateDto
{ 
	public required DateTimeOffset Timestamp { get; set; } 
	public required decimal Amount { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public required ItemType Type { get; set; } 

	[Unicode(false)]
	[StringLength(1500)]
	public required string? Description { get; set; }
}
