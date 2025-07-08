# POS_SYSTEM

A WPF-based Point of Sale (POS) system for supermarket operations, built with .NET Framework 4.7.2 and SQL Server, following the MVVM architecture.

## Features

- **Barcode Scanning:** Scan or enter product barcodes to add items to the cart.
- **Cart Management:** Add, remove, and update quantities of products in the cart.
- **Real-Time Totals:** Automatic calculation of totals and subtotals.
- **Checkout:** Processes sales, updates inventory, and records transactions.
- **Inventory Management:** (Planned) Admin features for managing products and categories.
- **Sales History:** (Planned) View past sales and generate reports.
- **User Feedback:** Friendly error and status messages for cashier actions.

## Technologies

- **WPF (.NET Framework 4.7.2)**
- **Entity Framework 6 (Code-First)**
- **SQL Server**
- **MVVM Pattern**

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone https://github.com/Fidelrock/POS_SYSTEM.git
   ```

2. **Open the solution in Visual Studio.**

3. **Configure your SQL Server connection string** in `App.config` if needed.

4. **Build and run the application.**
   - On first run, the database and sample products will be created automatically.

## Project Structure

- `Models/` - Data models (Product, Category, Sale, SaleItem, CartItem)
- `ViewModels/` - MVVM ViewModels and commands
- `Views/` - WPF XAML UI files
- `Services/` - Database and business logic
- `Utilities/` - Helpers and converters

## Contributors

- [Your Name Here]

## License

[MIT](LICENSE) (or your chosen license)
