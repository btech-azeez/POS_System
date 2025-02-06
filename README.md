# Point of Sale (POS) System

## Overview
This **Windows Desktop POS System** is built using **C# .NET (WinForms or WPF)** with **SQL Server** as the database. The application allows users to:
- View a list of products.
- Add items to the cart with quantity selection.
- Apply discounts and calculate tax.
- Checkout and store orders in the database.

## Features
- **Product Listing:** Displays available products in a `DataGridView`.
- **Cart Management:** Users can add selected products to the cart.
- **Discount & Tax Calculation:** Automatic calculation of total price with applied discounts and taxes.
- **Order Processing:** Stores the finalized orders in the database.
- **Database Integration:** Uses SQL Server for data storage.

## Technologies Used
- **.NET Framework / .NET Core** (WinForms or WPF)
- **C#**
- **SQL Server**
- **Entity Framework (EF Core)** (Optional for database interactions)

## Installation & Setup
1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-repo/POS-System.git
   ```
2. **Open the project in Visual Studio.**
3. **Configure the database:**
   - Update the `connectionString` in `App.config` or `appsettings.json`.
   - Run the provided SQL scripts to create the necessary tables.
4. **Build and run the project.**

## Database Schema
- **Products Table** (`ProductId`, `ProductName`, `Price`, `Stock`)
- **Orders Table** (`OrderId`, `TotalAmount`, `Discount`, `Tax`, `OrderDate`)
- **OrderDetails Table** (`OrderDetailId`, `OrderId`, `ProductId`, `Quantity`, `Price`)

## Usage Guide
1. **Select a product** from the `DataGridView`.
2. **Enter quantity** and click **Add to Cart**.
3. **Apply discount (if any)**.
4. **Click Checkout** to save the order.
5. **Review total amount** with tax and discount.

## Future Enhancements
- **Receipt Generation:** Generate a printable invoice.
- **Stock Management:** Reduce stock upon purchase.
- **User Authentication:** Implement login/logout functionality.
- **Reporting:** Sales summary and daily reports.

## Author
[Shaik Abdul Azeez]



