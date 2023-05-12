export interface IProduct {
    id: number
    category: string
    name: string
    price: number
    qty: number
    saleQty: number
    image: string
    description: string
}
export interface IInventoryItem {
    id: number
    code: string
    description: string
    price: number
    name: string
    category: string
    categoryId: number
    saleQty: number
    combinedQty: number
    image?: string
    location: string
    locationId: number
}

export interface IOrder {
    id: number
    productCode: string
    qty: number
    price: number
    orderedBy: string
    date: string
}
export interface ICategory{
    name: string
    id: number
}
export interface ILocation{
    name: string
    id: number
}
export interface IPendingOrder{
    id: number
    productCode: string
    qty: number
    price: number
    orderedBy: string
    date: string
}
export interface IMyOrder{
    id: number
  productName: string
  qty: number
  price: number
  date: string
  status: string
}