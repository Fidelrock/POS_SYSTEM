# POS_SYSTEM

A WPF-based Point of Sale (POS) system for supermarket operations, built with .NET Framework 4.7.2 and SQL Server, following the MVVM architecture.

## Screenshots
<!-- Add screenshots of the POS and Inventory/Admin interfaces here -->

## Features

- Cashier POS interface with barcode scanning, cart management, and real-time totals
- Checkout with stock update and transaction logging
- Admin inventory management: add, edit, delete products and categories
- Update product stock levels
- User-friendly validation and error messages
- Demo data auto-inserts on first run for easy testing

## How to Use

- Run the application. Demo products and categories will be created if the database is empty.
- Use the POS view to scan barcodes, add products to the cart, and checkout.
- Switch to the Inventory view to manage products and categories.
- Add or edit products/categories using the provided dialogs.

## Project Structure

- `Models/` - Data models (Product, Category, Sale, SaleItem, CartItem)
- `ViewModels/` - MVVM ViewModels (POSViewModel, InventoryViewModel, etc.)
- `Views/` - WPF XAML UI files (POSView, InventoryView, ProductDialog, CategoryDialog)
- `Services/` - Database and business logic
- `Utilities/` - Helpers and converters

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone https://github.com/Fidelrock/POS_SYSTEM.git
   ```
2. **Open the solution in Visual Studio.**
3. **Configure your SQL Server connection string** in `App.config` if needed.
4. **Build and run the application.**
   - On first run, the database and sample products will be created automatically.

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](LICENSE)
