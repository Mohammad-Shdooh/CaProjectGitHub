export interface AddOrderRequest { 
    UserName :string ; 
    MobileNumber : string ;
    Comments : string ;
    From :Date ;
    To :Date ;
    CarID : number ;
}


export interface UpdateOrderRequest { 
    ID : Number ;
    UserName :string ; 
    MobileNumber : string ;
    Comments : string ;
    From :Date ;
    To :Date ;
    CarID : number ;
}

export interface DeleteOrderRequest { 
    OrderID : Number ;
}