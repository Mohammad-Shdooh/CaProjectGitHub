# Implementation Details : 

## Backend (.Net Core 7)
Database: Created using Code First approach with migrations.
Design Pattern: Repository design pattern with a layered architecture.
API Layer: Handles API requests and responses.
BAL (Business Logic Layer):
Contains two folders: "Interfaces" and "Services."
Interfaces for Car and Orders are implemented in the Services folder (CarService and OrderService).
DAL (Data Access Layer):
DBContext: Contains CarRentalContext with model builders for the database.
Migrations: Folder containing database migrations.
Entities: Folder with Car and Order entities that are migrated to tables.
Repository: Folder with Car and Order repositories, each containing an interface and its implementation (CarRepository and OrderRepository).
Entities Layer:
Contains a DTOs folder for models and a Tools folder for utility classes like the ErrorCodes enum.


## Frontend (React with TypeScript)
Manage Orders Page:
Displays a table with all orders fetched from the API.
Edit, delete, and add new orders without reloading the page.
Components:
Add New Order: Adds a new order to the database and updates the orders table dynamically.
Edit Order: Updates an existing order in the database and the orders table without reloading the page.
Delete Order: Removes an order from the database and updates the orders table without reloading the page.
Entities Component: Contains objects for requests, responses, API endpoints, and models like Car and Order.
Home Component: Serves as the parent component.


## Database Relationship
One-to-Many Relationship:
Reason: Multiple orders can be created for the same car. This relationship allows managing orders for the same car at different dates.


- ## important Note :- please install the _node_modules_ packages before start the React Project using this command (npm install) .then, (npm start) for operate the project .  


