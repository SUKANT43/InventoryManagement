# InventoryManagement

StockFlow is a Windows Forms inventory management desktop app.

## Features

- Admin login with a seeded local account
- Product add, edit, delete, search, and category filtering
- Stock in/out adjustments with validation
- Low-stock and out-of-stock highlighting
- Inventory value, stock, and product summary cards
- CSV export for the current filtered product list
- Local XML persistence in `%LOCALAPPDATA%\StockFlow\inventory-data.xml`

## Default Login

- Email: `admin@stockflow.local`
- Password: `admin123`

## Build

```powershell
dotnet build InventoryManagement.sln
```

## Run

```powershell
dotnet run --project Admin\Admin.csproj
```
