using ExpenseTracker.API.Dtos;
using ExpenseTracker.Data.Contexts;
using ExpenseTracker.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Controllers;

[Route("api/line-items")]
[ApiController]
public class LineItemController(ApplicationContext _dbContext) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<List<LineItem>>> GetAll(CancellationToken ct) =>
		await _dbContext.LineItems.AsNoTracking().
			OrderByDescending(l => l.Timestamp).
			ToListAsync(ct);

	[HttpGet("summary")]
	public async Task<ActionResult<LineItemSummaryDto>> GetSummary(CancellationToken ct)
	{
		int currentMonth = DateTime.Today.Month;

		return await _dbContext.LineItems.AsNoTracking().
			Where(l => l.Timestamp.Year == DateTime.Today.Year).
			GroupBy(l => 1, (_, lines) => new LineItemSummaryDto
			{
				AnnualEarnings = lines.
					Where(l => l.Amount > 0).
					Sum(l => l.Amount),
				AnnualExpenses = lines.
					Where(l => l.Amount < 0).
					Sum(l => l.Amount),
				MonthlyEarnings = lines.
					Where(l => l.Amount > 0 && l.Timestamp.Month == currentMonth).
					Sum(l => l.Amount),
				MonthlyExpenses = lines.
					Where(l => l.Amount < 0 && l.Timestamp.Month == currentMonth).
					Sum(l => l.Amount)
			}).
			FirstAsync(ct);
	} 

	[HttpPost]
	public async Task<ActionResult<LineItem>> Insert([FromBody] LineItemUpdateDto dto)
	{
		LineItem line = new();
		_dbContext.Entry(line).CurrentValues.SetValues(dto); 
		_dbContext.LineItems.Add(line);
		await _dbContext.SaveChangesAsync();
		return line;
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, [FromBody] LineItemUpdateDto dto, CancellationToken ct)
	{
		var line = await _dbContext.LineItems.
			SingleOrDefaultAsync(l => l.Id == id, ct);

		if (line is null)
			return NotFound(); 

		_dbContext.Entry(line).CurrentValues.SetValues(dto);
		await _dbContext.SaveChangesAsync(CancellationToken.None);
		return Ok(); 
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var count = await _dbContext.LineItems.
			Where(l => l.Id == id).  
			ExecuteDeleteAsync();

		return count == 0 ? NotFound() : Ok(); 
	}
}
