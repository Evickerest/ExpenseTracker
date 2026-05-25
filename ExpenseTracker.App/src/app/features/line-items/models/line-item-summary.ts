export interface LineItemSummary {
    monthlyExpenses: number;
    monthlyEarnings: number;
    annualExpenses: number;
    annualEarnings: number;
}

export const defaultLineItemSummary: LineItemSummary = {
    monthlyExpenses: 0,
    monthlyEarnings: 0,
    annualExpenses: 0,
    annualEarnings: 0,
};