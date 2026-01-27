# FloodApp - Sri Lanka Flood Risk Assessment

A **.NET 8 Blazor Web App** designed to help users assess flood risks in specific locations across Sri Lanka.

## Features
- **🌍 Location Selection**: Cascading dropdowns for Province, District, and Town.
- **🗺️ Interactive Maps**: Leaflet-based map integration.
  - **Smart Navigation**: Automatically zooms to the selected Province or District.
  - **Visual Zones**: Displays risk zones overlays.
- **🌦️ Live Weather**: Real-time integration with **Open-Meteo API** to display current Temperature, Rain, and Wind conditions.
- **🛡️ Risk Analysis**: Point-in-polygon calculation to determine flood risk levels (High, Medium, Low) based on GeoJSON data.
- **🎨 Modern UI**: Dark-themed, responsive design with a custom component library.

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
   dotnet watch
   ```
   *`dotnet watch` provides hot reloading during development.*

4. **Access the App**:
   Open your browser and navigate to `http://localhost:5169`.

## Project Structure
- **Pages**: Razor components acting as routable pages (`RiskLocation`, `LocationSelection`).
- **Components**: Reusable UI (`WeatherWidget`), Map, and Location components.
- **Services**: 
  - `RiskService`: Calculates risk based on polygon data.
  - `WeatherService`: Fetches live data from Open-Meteo.
  - `LocationService`: Manages Province/District/Town data with coordinates.
- **wwwroot**: Static assets including CSS, Leaflet JS, and GeoJSON data.

## Credits
- Weather data provided by [Open-Meteo](https://open-meteo.com/).
- Maps provided by [OpenStreetMap](https://www.openstreetmap.org/) & Leaflet.
