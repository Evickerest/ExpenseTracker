namespace ExpenseTracker.API.Dtos;

public class LineItemSummaryDto
{
	public decimal MonthlyEarnings { get; set; }
	public decimal MonthlyExpenses { get; set; }
	public decimal AnnualEarnings { get; set; }
	public decimal AnnualExpenses { get; set; }
}
