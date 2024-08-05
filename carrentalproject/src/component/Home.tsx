import { useEffect, useState } from "react";
import { IOrder } from "./Entities/Entities.type";
import "./Home.style.css" ;
import ManageOrders from "./ManageOrders/ManageOrders";
import AddOrder from "./AddOrders/AddOrder";
import axios from "axios";
import { ApiEndPoints } from "./Entities/APIsEndPoints.type";
import { GetOrdersResponse } from "./Entities/APIsResponse.type";

const Home =()=>{ //this is a fanctional component
    
    const [orders, setOrders] = useState<IOrder[]>([]); 
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [loading, setLoading] = useState<boolean>(true); 
    const [error, setError] = useState<string | null>(null); 

    const fetchOrders = async () => { 
      try {
        const response = await axios.get<GetOrdersResponse>(ApiEndPoints.getOrders);
        if (Array.isArray(response.data.orders)){ 
          setOrders(response.data.orders);
        }else { 
          setOrders([]);
        }
      } catch (error) {
        setError('Failed to fetch orders');
      } finally {
        setLoading(false);
      }
    };

    useEffect(() => {
      // get all orders as initial value .
        fetchOrders();
      }, []); 



    const handleAddOrder = (newOrder: IOrder) => {
        try {
          // hit the get orders api to fetch the data after add and add the record to UI table without Reload the page .
          fetchOrders(); 
        } catch (error) {
          console.error('Failed to add order', error);
        }
      };


    return ( 
        <>
        <header className="Homeheader"> 
            <h1>Car Rental Orders</h1>
        </header>

        <section className="HomeContentsection">
        <button className="btn btn-primary my-2" onClick={() => setIsModalOpen(true)}>
          Add New Order
        </button>
            <ManageOrders  orders={orders}/>

            <AddOrder
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleAddOrder}
      />
        </section>
        </>

    );
}; 

export default Home ; // each fanctional component should to have the funcation and it's export

