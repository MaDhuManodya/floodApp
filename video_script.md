# Video Demonstration Script - SLIC Flood Management System

**Duration:** 8 minutes (5.5 min code + 2.5 min demo)

---

## PART 1: CODE EXPLANATION (0:00 - 5:30)

### 0:00 - 0:20 | Introduction
*(Scene: VS Code with project open)*

"Welcome to the SLIC Flood Management System demonstration—a real-time flood risk assessment app built with .NET 8 Blazor for Sri Lanka Insurance Corporation. I'll explain the technical implementation, then demonstrate the live application."

---

### 0:20 - 0:50 | Project Structure
*(Scene: Show folder structure)*

"The architecture is modular: **Pages** for routable components, **Components** for reusable UI, **Services** for business logic including LocationService, RiskService, WeatherService, and GeoJsonService. **Models** define data structures, **wwwroot** holds static assets and GeoJSON risk data, and **State** manages application state."

---

### 0:50 - 1:20 | Configuration
*(Scene: `Program.cs`)*

"Program.cs registers all services as Scoped—each user session gets its own instance. Lines 9-13 register our services, and lines 15-16 configure Blazor Server with interactive components for real-time updates without page refreshes."

---

### 1:20 - 2:50 | Risk Analysis Algorithm
*(Scene: `RiskService.cs`)*

"The core algorithm is in RiskService. CalculateRiskAsync takes coordinates and determines flood risk.

Line 18 loads GeoJSON polygon data defining risk zones. Lines 22-35 implement **Point-in-Polygon Ray Casting**—we draw a ray from the point and count boundary crossings. Odd count means inside, even means outside.

We check each zone until we find a match, returning High, Medium, or Low with color codes and messages. This runs in O(n×m) time but exits early on first match."

---

### 2:50 - 3:40 | Weather API Integration
*(Scene: `WeatherService.cs`)*

"WeatherService fetches live data from Open-Meteo API. Line 13 constructs the request with coordinates. Lines 16-19 make an async HTTP call and deserialize JSON.

The try-catch on lines 15-24 handles failures gracefully—if the API is down, we return null instead of crashing, allowing the UI to show fallback content."

---

### 3:40 - 4:30 | Location Hierarchy
*(Scene: `LocationService.cs`)*

"LocationService manages Sri Lanka's three-tier hierarchy: Provinces, Districts, Towns. 

GetProvinces returns all 9 provinces. GetDistricts filters by province ID. GetTowns filters by district ID and returns coordinates. This cascading ensures users drill down efficiently from province to exact location."

---

### 4:30 - 5:30 | State Management
*(Scene: `AppState.cs`)*

"AppState is a Scoped service storing SelectedProvinceId, SelectedDistrictId, SelectedTownId, and SelectedLatLng.

The NotifyStateChanged event implements the Observer pattern—when LocationForm updates state, subscribed components like the map auto-refresh. This eliminates tight coupling while maintaining synchronized UI.

Now let's see it running."

---

## PART 2: PROTOTYPE DEMONSTRATION (5:30 - 8:00)

### 5:30 - 5:50 | Landing Page
*(Scene: Load browser at http://localhost:5200)*

"Here's the application. The Landing page features SLIC branding with a modern dark theme (#111827) for professional aesthetics. Let's enter the dashboard."

---

### 5:50 - 6:50 | Location Selection
*(Scene: Location Selection page)*

"Split-view layout: map on right, selectors on left.

Selecting Western Province filters districts to Colombo, Gampaha, Kalutara. I'll choose Colombo District, then Colombo City. Watch the map auto-pan and zoom to the selected region.

Let's check flood risk."

---

### 6:50 - 7:50 | Risk Analysis Results
*(Scene: Risk Location page)*

"The dashboard shows **Low Risk** in green—this location is safe. High-risk zones would show red with evacuation warnings.

The **Weather Widget** displays live Open-Meteo data: 28°C temperature, 0mm rainfall, 12 km/h wind. This real-time integration is critical since risk changes with weather.

Recommendations provide safety advice, and Emergency Contacts show Disaster Management Center and police numbers.

The map displays the location marker with a color-coded risk legend."

---

### 7:50 - 8:00 | Conclusion
*(Scene: Navigate briefly)*

"In summary, the SLIC Flood Management System combines geospatial analytics with Point-in-Polygon algorithms, real-time weather API integration, and user-centric design using .NET 8 Blazor. This creates a comprehensive flood risk assessment tool for Sri Lanka. Thank you."

---

## Recording Tips

**Code Section:**
- Split screen: code left, file tree right
- Font size: 16-18pt
- Highlight key lines as you explain

**Demo Section:**
- Smooth mouse movements
- Pause briefly after clicks
- 1080p resolution, 100% browser zoom

**Audio:**
- Clear, moderate pace
- Emphasize technical terms
- Consistent volume
