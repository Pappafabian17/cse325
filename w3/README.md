# W03 Assignment: Build Web Applications with Blazor

Blazor web application developed for Week 03 of CSE 325, based on the Microsoft Learn module **"Use pages, routing, and layouts to improve Blazor navigation"** (Blazing Pizza).

## Overview of Implemented Features

- **Navigation & Routing**: Implemented `NavLink` components in the header, configured `@page` directives, and setup the default layout using `MainLayout.razor` and `App.razor`.
- **Order Placement & State**: Created `Checkout.razor` to place orders, updated `OrderState.cs` with reset handling, and linked it to the backend `OrdersController.cs` with Entity Framework Core SQLite persistence.
- **Order History & Tracking**: Built `MyOrders.razor` for reviewing order history and `OrderDetail.razor` with route parameter constraints (`/myorders/{orderId:int}`) to track order status.
- **Currency Customization**: Updated all price displays in `site.css` and Razor components from British Pounds (`£`) to dollars/pesos (`$`).
- **Footer Customization**: Added student name and country ("Fabian Pappa, Argentina") to the footer in `MainLayout.razor`.

## Running the Application

From `w3/BlazingPizza`:
```bash
dotnet run --launch-profile BlazingPizza
```
Access the application at `http://localhost:5000`.
