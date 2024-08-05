import { Car, IOrder } from "./Entities.type";

export interface GetOrdersResponse {
    orders: IOrder[];
    Message: string;
    ErrorCode: number;
    Result: boolean;
  }

  export interface GetCarsResponse {
    cars: Car[];
    Message: string;
    ErrorCode: number;
    Result: boolean;
  }

  export interface AddOrderResponse {
    Order: IOrder;
    Message: string;
    ErrorCode: number;
    Result: boolean;
  }

  export interface UpdateOrderResponse {
    order: IOrder;
    Message: string;
    ErrorCode: number;
    Result: boolean;
  }