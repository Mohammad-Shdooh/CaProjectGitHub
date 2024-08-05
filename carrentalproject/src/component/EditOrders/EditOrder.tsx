import { useEffect, useState } from "react";
import { Car, IOrder } from "../Entities/Entities.type";
import axios from "axios";
import { ApiEndPoints } from "../Entities/APIsEndPoints.type";
import { GetCarsResponse, UpdateOrderResponse } from "../Entities/APIsResponse.type";
import { UpdateOrderRequest } from "../Entities/APIsRequest.type";

interface EditOrderProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (order: IOrder) => void;
  order: IOrder;
}

const EditOrder = ({ isOpen, onClose, onSave, order }: EditOrderProps) => {
  const [editedOrder, setEditedOrder] = useState<IOrder>({ ...order });
  const [carTypes, setCarTypes] = useState<Car[]>([]);

  useEffect(() => {
    setEditedOrder({ ...order });
  }, [order]);

  useEffect(() => {
    const fetchCarTypes = async () => {
      try {
        const response = await axios.get<GetCarsResponse>(ApiEndPoints.getCars); 
        setCarTypes(response.data.cars);
      } catch (error) {
        console.error('Error fetching car types:', error);
      }
    };

    fetchCarTypes();
  }, []);

  const handleInputChange = (
    e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setEditedOrder((prevOrder) => ({
      ...prevOrder,
      [name]: value,
    }));
  };

  const handleCarChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const { value } = e.target;
    setEditedOrder((prevOrder) => ({
      ...prevOrder,
      Car: {
        ...prevOrder.Car,
        ID: Number(value),
      },
    }));
  };

  const handleSubmit = async () => {
    try {
      const updatedOrder: UpdateOrderRequest = {
        ID: editedOrder.ID,
        UserName: editedOrder.UserName,
        MobileNumber: editedOrder.MobileNumber,
        Comments: editedOrder.Comments,
        From: new Date(editedOrder.From),
        To: new Date(editedOrder.To),
        CarID: editedOrder.Car.ID,
      };

      const response = await axios.post<UpdateOrderResponse>(ApiEndPoints.updateOrder, updatedOrder);
      onSave(response.data.order);
      onClose();
    } catch (error) {
      console.error("Error updating order:", error);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Edit Order</h2>
        <form>
          <div className="form-group">
            <label htmlFor="CarID">Car Type</label>
            <select
              name="CarID"
              value={editedOrder.Car.ID}
              onChange={handleCarChange}
              className="form-control"
            >
              <option value={0}>Select Car</option>
              {carTypes.map((car) => (
                <option key={car.ID} value={car.ID}>
                  {car.Name}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="From">From</label>
            <input
              type="datetime-local"
              name="From"
              value={new Date(editedOrder.From).toISOString().slice(0, 16)}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>
          <div className="form-group">
            <label htmlFor="To">To</label>
            <input
              type="datetime-local"
              name="To"
              value={new Date(editedOrder.To).toISOString().slice(0, 16)}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>
          <div className="form-group">
            <label htmlFor="UserName">User Name</label>
            <input
              type="text"
              name="UserName"
              value={editedOrder.UserName}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>
          <div className="form-group">
            <label htmlFor="MobileNumber">Mobile Number</label>
            <input
              type="text"
              name="MobileNumber"
              value={editedOrder.MobileNumber}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>
          <div className="form-group">
            <label htmlFor="Comments">Comments</label>
            <textarea
              name="Comments"
              value={editedOrder.Comments}
              onChange={handleInputChange}
              className="form-control"
            />
          </div>
          <button type="button" onClick={handleSubmit} className="btn btn-primary">
            Save
          </button>
          <button type="button" onClick={onClose} className="btn btn-secondary">
            Cancel
          </button>
        </form>
      </div>
    </div>
  );
};

export default EditOrder;
