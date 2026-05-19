export interface LineItem {
    id: number,
    timestamp: string,
    amount: number,
    type: string,
    description: string
}

export interface LineItemUpdate {
    timestamp: string,
    amount: number,
    type: string,
    description: string
}