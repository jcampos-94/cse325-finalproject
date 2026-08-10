# Inventory Manager

A web-based inventory management application built with .NET and Blazor. The application allows authenticated users to manage products and categories, search their inventory, and maintain accurate product information.

## Features

### Product Management

- Add new products
- View products in an organized table
- Edit existing products
- Delete products with confirmation
- Store product name, description, price, quantity, and category
- Display prices using U.S. dollar currency formatting

### Category Management

- Add categories
- View existing categories
- Edit category names
- Delete categories

### Search

- Search products by:
  - Product name
  - Description
  - Category

- Clear searches and return to the complete product list

### Authentication and Authorization

- User registration and login through ASP.NET Core Identity
- Protected product pages require authentication
- Products are associated with the user who created them
- Users can only view, search, edit, and delete their own products

### Data Validation

- Product names are required
- Product prices must be greater than zero
- Product quantities cannot be negative
- A category must be selected for each product
- Category names are required
- Users receive feedback when validation fails or an operation succeeds

## Technologies

- **.NET 10**
- **Blazor**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQLite**
- **Bootstrap**
- **C#**

## Project Structure

The project is organized into several main areas:

- `Components/Pages/` — Blazor pages and user interface
- `Models/` — Application data models
- `Services/` — Services responsible for database operations
- `Data/` — Entity Framework Core database context and migrations
- `wwwroot/` — Static application resources

## Getting Started

### Prerequisites

Make sure the following are installed:

- .NET 10 SDK
- Git

### Clone the Repository

Clone the repository and navigate to the project directory:

```bash
git clone <repository-url>
cd cse325-finalproject
```

### Restore Dependencies

Run:

```bash
dotnet restore
```

### Build the Application

Run:

```bash
dotnet build
```

### Set Up the Database

The project uses SQLite and Entity Framework Core migrations.

Run:

```bash
dotnet ef database update
```

This creates or updates the local `inventory.db` database using the project's existing migrations.

### Run the Application

Run:

```bash
dotnet run
```

Open the URL provided by the application in a web browser.

## Database

The application uses SQLite for data storage.

The database connection is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=inventory.db"
}
```

The project includes the SQLite database file because it is required for the current deployment.

Entity Framework Core migrations are also included in the repository to maintain and update the database schema when changes are made.

When database changes are introduced, create and apply a migration using:

```bash
dotnet ef database update
```

## Using the Application

### 1. Create an Account

Register a user account through the application's registration page and log in.

### 2. Manage Categories

Navigate to the Categories page to create, edit, or delete product categories.

### 3. Manage Products

Navigate to the Products page to:

- Add products
- Select an existing category
- Edit product information
- Delete products
- Search the inventory

### 4. Search Products

Enter a search term and select **Search**. The application searches product names, descriptions, and categories.

Select **Clear** to return to the complete product list.

## Security

Product ownership is enforced through the authenticated user's identity. Each product is associated with the user who created it, and product queries are filtered using the current user's ID.

This ensures that users cannot access or modify products belonging to another user through the application's product operations.

## Deployment

The application is deployed using Render for demonstration and presentation purposes.

The deployed application uses the SQLite database included with the project. Entity Framework Core migrations are also maintained in the repository so the database schema can be updated when necessary.

For local development, the application can be run using:

```bash
dotnet restore
dotnet build
dotnet ef database update
dotnet run
```

## Development Team

This application was developed as a team project for **CSE325**.

- Juan Alonso Campos Guerra
- Kwadjo Owusu Ansah Quarshie
- Lifegate Justice De-Tom
- Maxwell Chukwuemeka Iwe

The project demonstrates the use of .NET, Blazor, Entity Framework Core, database management, authentication, validation, and responsive web application design.
