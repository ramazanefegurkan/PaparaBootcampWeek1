# Papara Bootcamp Week 1 Project

This project is a simple ASP.NET Core API application developed during Papara Bootcamp Week 1.

## Features

- CRUD operations for Product model
- Listing and sorting products
- Validation using FluentValidation
- Error handling with standard HTTP status codes

## Installation

To run this project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/ramazanefegurkan/PaparaBootcampWeek1.git
    cd PaparaBootcampWeek1
    ```

2. Install the dependencies:
    ```bash
    dotnet restore
    ```

3. Run the application:
    ```bash
    dotnet run
    ```

## Usage

### API Endpoints

#### List all products
  ```http
  GET /api/products/list
  ```

#### List products with a specific name
  ```http
  GET /api/products/list?name=example
  ```

#### Sort Products by Price

```http
GET /api/products/sortByPrice
```

#### Get Product

```http
GET /api/products/{id}
```

#### Create Product

```http
POST /api/products
Content-Type: application/json

{
    "name": "Product Name",
    "description": "Product Description",
    "price": 100,
    "stock": 50
}
```

#### Update Product

```http
PUT /api/products/{id}
Content-Type: application/json

{
    "name": "Updated Product Name",
    "description": "Updated Product Description",
    "price": 150,
    "stock": 30
}
```

#### Delete Product

```http
DELETE /api/products/{id}
```
