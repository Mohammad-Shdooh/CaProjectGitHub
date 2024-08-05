import { useEffect, useState } from "react";
import { Car, IOrder } from "../Entities/Entities.type";
import axios from "axios";
import "./AddOrder.style.css";
import { ApiEndPoints } from "../Entities/APIsEndPoints.type";
import { AddOrderResponse, GetCarsResponse } from "../Entities/APIsResponse.type";
import { AddOrderRequest } from "../Entities/APIsRequest.type";

interface AddNewOrderModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (order: IOrder) => void;
}

const AddOrder = ({ isOpen, onClose, onSave }: AddNewOrderModalProps) => {
  const [newOrder, setNewOrder] = useState({
    id: Date.now(),
    carId: 0,
    from: '',
    to: '',
    userName: '',
    mobileNumber: '',
    comments: ''
  });

  const [carTypes, setCarTypes] = useState<Car[]>([]);

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

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setNewOrder(prevOrder => ({
      ...prevOrder,
      [name]: name === 'carId' ?  Number(value) : value
    }));
  };

  const handleSubmit = async () => {
    try {
      const selectedCar = carTypes.find(car => car.ID === newOrder.carId);
      if (!selectedCar) {
        throw new Error('Selected car type is invalid');
      }

      const order: AddOrderRequest = {
        UserName: newOrder.userName,
        MobileNumber: newOrder.mobileNumber,
        Comments: newOrder.comments,
        From: new Date(newOrder.from),
        To: new Date(newOrder.to),
        CarID : newOrder.carId
      };

      var response  = await axios.post<AddOrderResponse>(ApiEndPoints.addNewOrder, order); 

      onSave(response.data.Order);

      setNewOrder({
        id: 0,
        carId: 0,
        from: '',
        to: '',
        userName: '',
        mobileNumber: '',
        comments: ''
      });

      onClose();
    } catch (error) {
      console.error('Error saving order:', error);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Add New Order</h2>
        <form>
          <div className="form-group">
            <label htmlFor="carId">Car Type</label>
            <select name="carId" value={newOrder.carId} onChange={handleChange} className="form-control">
              <option value={0}>Select a car type</option>
              {carTypes.map(car => (
                <option key={car.ID} value={car.ID}>
                  {car.Name}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="from">From</label>
            <input type="datetime-local" name="from" value={newOrder.from} onChange={handleChange} className="form-control" />
          </div>
          <div className="form-group">
            <label htmlFor="to">To</label>
            <input type="datetime-local" name="to" value={newOrder.to} onChange={handleChange} className="form-control" />
          </div>
          <div className="form-group">
            <label htmlFor="userName">User Name</label>
            <input type="text" name="userName" value={newOrder.userName} onChange={handleChange} className="form-control" />
          </div>
          <div className="form-group">
            <label htmlFor="mobileNumber">Mobile Number</label>
            <input type="text" name="mobileNumber" value={newOrder.mobileNumber} onChange={handleChange} className="form-control" />
          </div>
          <div className="form-group">
            <label htmlFor="comments">Comments</label>
            <textarea name="comments" value={newOrder.comments} onChange={handleChange} className="form-control" />
          </div>
          <button type="button" onClick={handleSubmit} className="btn btn-primary">Save</button>
          <button type="button" onClick={onClose} className="btn btn-secondary">Cancel</button>
        </form>
      </div>
    </div>
  );
};

export default AddOrder;
