import { useEffect, useState } from "react";
import "./ManageOrders.style.css";
import 'bootstrap/dist/css/bootstrap.min.css'; 
import { IOrder } from "../Entities/Entities.type";
import axios from "axios";
import EditOrder from "../EditOrders/EditOrder";
import DeleteOrderModal from "../DeleteOrder/DeleteOrder";
import { ApiEndPoints } from "../Entities/APIsEndPoints.type";
import { json } from "stream/consumers";
import { DeleteOrderRequest } from "../Entities/APIsRequest.type";

interface ManageOrdersProps {
  orders: IOrder[];
}

const ManageOrders = ({orders} : ManageOrdersProps)=>{ 
  const [filteredOrders, setFilteredOrders] = useState<IOrder[]>([]);
  const [searchDate, setSearchDate] = useState<Date | null>(null);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState<IOrder | null>(null);
  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [orderToDelete, setOrderToDelete] = useState<IOrder | null>(null);

useEffect(() => {
    setFilteredOrders(orders);
  }, [orders]);


  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const date = event.target.value ? new Date(event.target.value) : null;
    setSearchDate(date);
  };

  const handleSearch = () => {
    if (searchDate) {
      // set all hours time to the same time for the DB records and the search field to compare it at the date only .
      const searchDateMidnight = new Date(searchDate);
      searchDateMidnight.setHours(0, 0, 0, 0);

      const filtered = orders.filter(order => {
        const orderDateMidnight = new Date(order.From);
        orderDateMidnight.setHours(0, 0, 0, 0);

        return orderDateMidnight.getTime() === searchDateMidnight.getTime();
      });

      setFilteredOrders(filtered);
    } else {
      setFilteredOrders(orders);
    }
  };

  const handleEditClick = (order: IOrder) => {
    setSelectedOrder(order);
    setIsEditModalOpen(true);
  };

  const handleSave = (updatedOrder: IOrder) => {
    // Update the orders state with the edited order
    const updatedOrders = orders.map(order =>
      order.ID === updatedOrder.ID ? updatedOrder : order
    );
    setFilteredOrders(updatedOrders);
    setIsEditModalOpen(false);
  };


  const handleDeleteClick = (order: IOrder) => {
    setOrderToDelete(order);
    setIsDeleteModalOpen(true);
  };

  const handleDelete = () => {
    if (orderToDelete) {
      const deleteOrderRequest: DeleteOrderRequest = {
        OrderID: orderToDelete.ID,
      };

      axios.post(ApiEndPoints.deleteOrder, deleteOrderRequest)
        .then(() => {
          setFilteredOrders(filteredOrders.filter(order => order.ID !== orderToDelete.ID));
          //setOrderToDelete(null);  // Clear selection after deletion
          setIsDeleteModalOpen(false);
        })
        .catch(error => {
          console.error("There was an error deleting the order!", error);
        });
    }
  };

  const calculateDuration = (from: Date, to: Date) => {
    const fromDate = new Date(from);
    const toDate = new Date(to);
    const duration = Math.ceil((toDate.getTime() - fromDate.getTime()) / (1000 * 3600 * 24));
    return duration;
  };

  return (
    <div className="container">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h1 className="my-4">Manage Orders</h1>
        <div className="search-bar d-flex align-items-center">
          <input
            type="datetime-local"
            value={searchDate ? searchDate.toISOString().slice(0, 16) : ''}
            onChange={handleSearchChange}
            className="form-control me-2"
          />
          <button className="btn btn-primary" onClick={handleSearch}>Search</button>
        </div>
      </div>
      
      {filteredOrders.length === 0 ? (
        <div className="alert alert-info" role="alert">
          There are no orders to display.
        </div>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>ID</th>
              <th>Car</th>
              <th>From</th>
              <th>To</th>
              <th>Duration (days)</th>
              <th>User Name</th>
              <th>Mobile Number</th>
              <th>Comments</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredOrders.map((order) => (
              <tr key={order.ID}>
                <td>{order.ID}</td>
                <td>{order.Car ? order.Car.Name : 'No car assigned'}</td>
                <td>{new Date(order.From).toLocaleDateString()}</td>
                <td>{new Date(order.To).toLocaleDateString()}</td>
                <td>{calculateDuration(order.From, order.To)}</td>
                <td>{order.UserName}</td>
                <td>{order.MobileNumber}</td>
                <td>{order.Comments}</td>
                <td>
                  <button className="btn btn-primary me-2" onClick={() => handleEditClick(order)}>Edit</button>
                  <button className="btn btn-danger" onClick={() => handleDeleteClick(order)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
  
      {isEditModalOpen && selectedOrder && (
        <EditOrder
          isOpen={isEditModalOpen}
          onClose={() => setIsEditModalOpen(false)}
          onSave={handleSave}
          order={selectedOrder}
        />
      )}
  
      {isDeleteModalOpen && orderToDelete && (
        <DeleteOrderModal
          isOpen={isDeleteModalOpen}
          onClose={() => setIsDeleteModalOpen(false)}
          onDelete={handleDelete}
          order={orderToDelete}
        />
      )}
    </div>
  );
};

export default ManageOrders ;