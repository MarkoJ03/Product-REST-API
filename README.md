# Product-User Service

Product-User Service  is a .NET Core Web API application that provides endpoints for managing products and users. The API supports CRUD operations, JWT authentication and authorization, product assignment to users, and statistical endpoints for products.

## Architecture
The implementation of the 3-layered architecture in .NET involves three main layers (or projects):

### Data Layer (DataAccessLayer or DAL)
This ClassLibrary project handles communication with the database. It contains:

* Models: Entity classes.
* DbContext and Migrations: Database context and migration configurations.
* Repository Interfaces and Implementations: Logic for data retrieval, modification, saving, and deletion.


### Business Layer (BusinessLogicLayer or BALL)
This ClassLibrary project contains the application's core logic. It:

* Retrieves data from the database.
* Performs necessary calculations and additional logic.
* Converts models to DTO objects and returns them to the upper layer.
* Contains interfaces and implementations of services, validators, and DTO objects.
* References the Data Layer project.


### Presentation Layer (WebApi)
This WebApi project serves as the entry point of the application. It:

* Contains controllers with HTTP methods.
* Handles necessary configurations, such as JWT token requirements.
* Communicates with the Business Layer via service interfaces.
* Receives DTOs as input parameters, passes them to the service, and returns DTOs to the external world.
* References both the Data Layer (for context and interface registration) and the Business Layer.


## Implementation Details
### Core Requirements
* CRUD Operations: Implement standard CRUD operations (GET, POST, PUT, DELETE) for the product REST API service.
* Documentation: Utilize Swagger for API documentation.
* Persistence: Use SQLite and EntityFramework for data persistence.


### Bonus Features
* Input Validation: Implement input validation using FluentValidation.
* Error Handling: Include comprehensive error handling.
* 3-Layer Architecture: Focus on the "interface segregation" principle.
* Authentication and Authorization: Implement JWT-based authentication and authorization.


### Additional Specifications
* Many-to-Many Relationship: Create a table for linking users and products. Each user can have multiple products, and each product can have multiple users.
* Product Deletion: Only the user who added the product (owner) can delete that product. This means that only the user who created the product can perform the delete operation.
* Product Assignment: Products can be assigned to other users. This functionality allows a single product to be associated with multiple users.
* Product Listing: The API for listing products should return only the products assigned to a specific user. This means that when a user requests a list of products, they will receive only the products assigned to them.

## How to Run

### Backend

1. Clone the repository.
2. Navigate to the project directory.
3. Build the solution using Visual Studio or the .NET CLI.
4. Run the backend project using Visual Studio or the .NET CLI:
```
dotnet build
dotnet run --project PresentationLayer
```
### Frontend

1. Navigate to the frontend directory.
2. Install dependencies and start the React application:
    ```bash
    npm install
    npm start
    ```

## Endpoints

### Product Endpoints

- **GET /api/products**: Retrieve all products assigned to the logged-in user.
- **POST /api/products**: Create a new product and assign it to the logged-in user.
- **PUT /api/products/{id}**: Update an existing product. Only the owner can update the product.
- **DELETE /api/products/{id}**: Delete a product. Only the owner can delete the product.
- **GET /api/products/statistics**: Retrieve basic statistics about products.
- **GET /api/products/popular**: Retrieve a list of the most popular products.

### User Endpoints

- **GET /api/user**: Retrieve all users.
- **GET /api/user/{id}**: Retrieve a user by ID.
- **POST /api/auth/login**: Authenticate a user and return a JWT token.
- **POST /api/user/register**: Register a new user.
- **PUT /api/user/{id}**: Update an existing user.
- **DELETE /api/user/{id}**: Delete a user.

## UserProduct Endpoints

- **POST /api/UserProduct/assign**: Assign a product to a user.
- **GET /api/UserProduct/user/{userId}/products**: Get all products assigned to a specific user.
- **GET /api/UserProduct/isowner**: Check if the logged-in user is the owner of a specific product.
    
## Technologies Used

- **Backend**: .NET Core, EntityFramework, SQLite, FluentValidation, JWT
- **Frontend**: React, Axios, React Router
