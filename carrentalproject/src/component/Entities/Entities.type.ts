
export interface Car { 
    ID : number ;
    Name : string ;
}
export interface IOrder { 
    ID : number ; 
    Car : Car ;
    From : Date ;
    To : Date ;
    UserName : string ; 
    MobileNumber : string ; 
    Comments : string ; 
}



