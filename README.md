# FloodApp - Sri Lanka Flood Risk Assessment

A .NET 8 Blazor Web App designed to help users assess flood risks in specific locations across Sri Lanka.

## Features
- **Location Selection**: Cascading dropdowns for Province, District, and Town.
- **Interactive Maps**: Leaflet-based map integration to visualize risk zones and pick locations.
- **Risk Analysis**: Point-in-polygon calculation to determine flood risk levels (High, Medium, Low) based on GeoJSON data.
- **Modern UI**: Dark-themed, responsive design with a custom component library.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## How to Run

1. **Navigate to the project directory**:
   ```bash
   cd floodApp
   ```

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```
   *For hot reloading during development, use:*
   ```bash
   dotnet watch
   ```

4. **Access the App**:
   Open your browser and navigate to the URL shown in the terminal (usually `http://localhost:5169` or `http://localhost:5000`).

## Project Structure
- **Pages**: Razor components acting as routable pages.
- **Components**: Reusable UI, Map, and Location components.
- **Services**: Logic for Location data, GeoJSON processing, and Risk calculations.
- **wwwroot**: Static assets including CSS, Leaflet interop JS, and data files.
