export interface IProduct {
    id: number
    category: string
    name: string
    price: number
    qty: number
    saleQty: number
    image: string
    description: string
    location: string
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
    lendQty: number 
    combinedQty: number
    image?: string
    location: string
    locationId: number
}

export interface IUserLendItem{
  email: string
  lentItems: ILentItem[]
}
export interface IEmployee{
    email: string
    name: string
    avatar: string
  }
 
  export interface IEmployeesResponse{
    employees: IEmployee[]
  }
  export interface IModifiedUser{
    email: string
    name: string
    avatar: string
   lentItems: ILentItem[]

  }
  export interface ISidebarUser{
    email: string
    name: string
    avatar: string
   memberType: string

  }
export interface ILentItem{
    id: number
    qty: number
    startDate: string
    endDate: string | null
    productName: string
    productCode: string
}
export interface IMyLentItem{
    qty: number
    startDate: string
    endDate: string | null
    productName: string
    productCode: string
}
export interface IReturnedValue{
    returnedValue: number | string
}
export interface IFormInputs{
    code: string
    categoryId: number | string
    locationId:  number | string
    name: string
    price: number | null
    combinedQty: number | null
    saleQty: number | null
    lendQty: number | null
    image: string
    description: string
}
export interface ILendItemsFormInputs{
   qty: number | null, 
   lentBy: string
}
export interface IOrder {
    id: number
    productCode: string
    qty: number
    price: number
    orderedBy: string
    date: string
}
export interface IEmployee {
   name: string
   email: string
   avatar: string
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