export interface LineItem {
    id: number,
    timestamp: string,
    amount: number,
    type: string,
    description: string
}

export const defaultLineItem: LineItem = {
    id: 0, timestamp: "", amount: 0, type: "", description: ""
}

export interface LineItemUpdate {
    timestamp: string,
    amount: number,
    type: string,
    description: string
}
