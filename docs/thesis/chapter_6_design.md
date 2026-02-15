# Chapter 6: Design

## 6.1 Chapter Overview

This chapter presents the comprehensive design approach for the **Flood Management System**, an intelligent flood risk assessment application developed for Sri Lanka Insurance Corporation (SLIC). The design phase is critical as it translates the requirements identified in the Software Requirements Specification (SRS) into a well-structured technical blueprint that guides the implementation process.

The chapter is structured to provide a complete understanding of the system's design from multiple perspectives:

- **Design Goals** outline the key quality attributes that the system architecture must satisfy
- **System Architecture** presents the high-level structural organization of the application
- **Detailed Design** provides in-depth views of component interactions and data flows
- **Algorithm Design** explains the core computational logic for flood risk assessment
- **User Interface Design** demonstrates the visual and interaction design considerations

The design phase ensures that the system is scalable, maintainable, and aligned with both functional and non-functional requirements. Each design decision has been made with consideration for real-time performance, user accessibility, and the specific context of flood risk management in Sri Lanka.

---

## 6.2 Design Goals

The design goals represent the key quality attributes that define the overall quality of the Flood Management System. These goals directly correspond to the Non-Functional Requirements (NFRs) identified in the SRS chapter and serve as guiding principles throughout the system design and implementation phases.

### 6.2.1 Performance

The system must provide real-time responsiveness to ensure critical flood risk information is delivered without delay:

- **Response Time**: Risk assessment calculations must complete within 2 seconds
- **Weather Data Retrieval**: Real-time weather information should load within 3 seconds
- **Map Rendering**: Interactive maps must render and become interactive within 1.5 seconds
- **State Updates**: UI updates in response to user interactions should occur within 500ms

Performance is critical in emergency scenarios where users need immediate access to flood risk information.

### 6.2.2 Usability

The application must be intuitive and accessible to users with varying levels of technical expertise:

- **Intuitive Navigation**: Clear hierarchical location selection (Province → District → Town)
- **Visual Clarity**: Color-coded risk levels (Red for High, Orange for Medium, Green for Low)
- **Minimal Learning Curve**: Users should be able to assess flood risk within 3 clicks
- **Responsive Design**: Interface adapts seamlessly to different screen sizes

### 6.2.3 Accuracy

Risk assessments must be reliable and based on validated geospatial data:

- **Geospatial Precision**: Risk zones defined using accurate GeoJSON polygon coordinates
- **Algorithm Reliability**: Point-in-Polygon algorithm with validated ray-casting implementation
- **Weather Data Integrity**: Real-time weather from verified API source (Open-Meteo)
- **Location Data Correctness**: Verified coordinates for all districts and towns across Sri Lanka

### 6.2.4 Reliability

The system must remain operational even under adverse conditions:

- **Graceful Degradation**: If weather API is unavailable, risk assessment still functions
- **Error Handling**: Comprehensive try-catch blocks with user-friendly error messages
- **Fallback Mechanisms**: Default safe risk level when location data is ambiguous
- **Session Persistence**: User selections maintained throughout the session lifecycle

### 6.2.5 Scalability

The architecture must support future growth and feature additions:

- **Modular Design**: Services are decoupled and independently maintainable
- **Data Extensibility**: Easy addition of new provinces, districts, or risk zones
- **API Integration**: Abstracted service layer allows swapping or adding data sources
- **Component Reusability**: UI components designed for reuse across different contexts

### 6.2.6 Accessibility

The application must be usable by people with diverse abilities:

- **Keyboard Navigation**: Full functionality accessible via keyboard shortcuts
- **Screen Reader Support**: Semantic HTML and ARIA labels for assistive technologies
- **Color Contrast**: WCAG 2.1 AA compliant color schemes
- **Alternative Text**: All visual elements have descriptive text alternatives

---

## 6.3 System Architecture Diagram

The Flood Management System follows a **three-tier layered architecture**, which provides clear separation of concerns and promotes maintainability, testability, and scalability.

### Architecture Overview

The system is organized into three distinct tiers:

```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION TIER                         │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │   Landing    │  │  Location    │  │     Risk     │      │
│  │    Page      │  │  Selection   │  │  Assessment  │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
│                                                               │
│  Components: LocationForm, WeatherWidget, RiskMapView        │
│  Technologies: Blazor, Razor Components, CSS, Leaflet.js     │
└─────────────────────────────────────────────────────────────┘
                            ↕
┌─────────────────────────────────────────────────────────────┐
│                    APPLICATION TIER                          │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │   Location   │  │     Risk     │  │   Weather    │      │
│  │   Service    │  │   Service    │  │   Service    │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
│                                                               │
│  ┌──────────────┐  ┌──────────────┐                         │
│  │   AppState   │  │   GeoJSON    │                         │
│  │  Management  │  │   Service    │                         │
│  └──────────────┘  └──────────────┘                         │
│                                                               │
│  Technologies: C# Services, Dependency Injection             │
└─────────────────────────────────────────────────────────────┘
                            ↕
┌─────────────────────────────────────────────────────────────┐
│                      DATA TIER                               │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │   Location   │  │   GeoJSON    │  │  Open-Meteo  │      │
│  │  Seed Data   │  │  Risk Zones  │  │  Weather API │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
│                                                               │
│  Storage: Static JSON/GeoJSON files, External REST API       │
│  Technologies: GeoJSON, JSON, HTTP Client                    │
└─────────────────────────────────────────────────────────────┘
```

### Technology Stack Visualization

*[Insert diagram with technology logos: .NET 8, C#, Blazor, Leaflet.js, Open-Meteo API, GeoJSON, OpenStreetMap]*

### Tier Descriptions

#### 6.3.1 Presentation Tier

The **Presentation Tier** is responsible for user interface rendering and interaction handling. It consists of:

- **Pages**: Routable Razor components representing distinct views (Landing, LocationSelection, RiskLocation)
- **Components**: Reusable UI widgets such as `LocationForm`, `WeatherWidget`, `RiskMapView`, and `RiskSummaryCard`
- **Client-Side Interactivity**: Blazor Server's SignalR connection enables real-time UI updates
- **Map Integration**: Leaflet.js JavaScript library for interactive geospatial visualization

This tier implements the **Component-Based Architecture** pattern, promoting reusability and maintainability. All presentation logic is isolated from business logic, ensuring clean separation of concerns.

#### 6.3.2 Application Tier

The **Application Tier** contains the core business logic and orchestrates data flow between the presentation and data tiers. Key services include:

- **LocationService**: Manages Sri Lanka's administrative hierarchy (9 provinces, 25 districts, towns)
- **RiskService**: Implements the Point-in-Polygon algorithm for flood risk determination
- **WeatherService**: Fetches real-time weather data from Open-Meteo API
- **GeoJsonService**: Loads and parses GeoJSON risk zone definitions
- **AppState**: Client-scoped state management for user session data

All services are registered with ASP.NET Core's **Dependency Injection** container, promoting loose coupling and testability. Services are implemented as singletons or scoped instances based on their lifecycle requirements.

#### 6.3.3 Data Tier

The **Data Tier** provides persistent and external data sources:

- **Location Seed Data**: Hardcoded C# collections containing provinces, districts, and towns with GPS coordinates
- **GeoJSON Risk Zones**: Static `risk_zones.geojson` file defining flood-prone polygons
- **Open-Meteo Weather API**: External REST API providing real-time meteorological data

This hybrid approach uses static data for administrative boundaries (which rarely change) and external APIs for dynamic real-time information (weather conditions).

---

## 6.4 Detailed Design

This section presents the detailed design diagrams that illustrate component interactions, data flows, and behavioral patterns within the system.

### 6.4.1 Component Diagram

The component diagram shows the structural organization of the system's major components and their dependencies.

```
┌─────────────────────────────────────────────────────────┐
│                    USER INTERFACE                        │
└─────────────────────────────────────────────────────────┘
           │                 │                 │
           ▼                 ▼                 ▼
    ┌──────────┐      ┌──────────┐      ┌──────────┐
    │ Landing  │      │Location  │      │   Risk   │
    │   Page   │      │Selection │      │Assessment│
    └──────────┘      │   Page   │      │   Page   │
                      └──────────┘      └──────────┘
                           │                   │
                           │                   │
          ┌────────────────┴───────────────────┴────────┐
          │                                              │
          ▼                                              ▼
    ┌──────────┐                               ┌──────────────┐
    │Location  │◄──────────────────────────────│  AppState    │
    │  Form    │                               │  (Session)   │
    └──────────┘                               └──────────────┘
          │                                        │      │
          │                                        │      │
          ▼                                        ▼      ▼
    ┌──────────┐     ┌──────────┐     ┌──────────┐  ┌──────────┐
    │Location  │     │   Risk   │     │ Weather  │  │   Map    │
    │ Service  │────▶│ Service  │     │ Service  │  │Component │
    └──────────┘     └──────────┘     └──────────┘  └──────────┘
                           │                │
                           ▼                ▼
                     ┌──────────┐    ┌──────────┐
                     │ GeoJSON  │    │Open-Meteo│
                     │ Service  │    │   API    │
                     └──────────┘    └──────────┘
```

**Key Dependencies:**

1. `LocationSelection Page` depends on `LocationForm`, `AppState`, and `LocationService`
2. `RiskAssessment Page` depends on `RiskService`, `WeatherService`, `AppState`, and `MapComponent`
3. `RiskService` depends on `GeoJsonService` for polygon data
4. `WeatherService` depends on `HttpClient` for external API calls

### 6.4.2 Sequence Diagram - Location Selection Flow

This sequence diagram illustrates the interaction flow when a user selects their location.

```
User          LocationPage    LocationForm    LocationService    AppState
 │                 │               │                 │              │
 │   Navigate      │               │                 │              │
 │────────────────▶│               │                 │              │
 │                 │  Initialize   │                 │              │
 │                 │──────────────▶│                 │              │
 │                 │               │  GetProvinces() │              │
 │                 │               │────────────────▶│              │
 │                 │               │◄────────────────│              │
 │                 │               │   [Provinces]   │              │
 │                 │               │                 │              │
 │  Select Province│               │                 │              │
 │─────────────────┼──────────────▶│                 │              │
 │                 │               │ GetDistricts()  │              │
 │                 │               │────────────────▶│              │
 │                 │               │◄────────────────│              │
 │                 │               │   [Districts]   │              │
 │                 │               │                 │              │
 │  Select District│               │                 │              │
 │─────────────────┼──────────────▶│                 │              │
 │                 │               │  GetTowns()     │              │
 │                 │               │────────────────▶│              │
 │                 │               │◄────────────────│              │
 │                 │               │    [Towns]      │              │
 │                 │               │                 │              │
 │   Select Town   │               │                 │              │
 │─────────────────┼──────────────▶│                 │              │
 │                 │               │                 │              │
 │ Click Confirm   │               │                 │              │
 │─────────────────┼──────────────▶│                 │              │
 │                 │               │  UpdateState()  │              │
 │                 │               │─────────────────┼─────────────▶│
 │                 │               │                 │              │
 │                 │  Navigate("/risk")              │              │
 │                 │◄─────────────────────────────────              │
 │                 │               │                 │              │
```

**Flow Description:**

1. User navigates to the Location Selection page
2. `LocationForm` component initializes and requests province list from `LocationService`
3. User selects a province, triggering a request for districts within that province
4. User selects a district, triggering a request for towns within that district
5. User selects a town and clicks "Confirm"
6. Selected location data (Province, District, Town, Coordinates) is saved to `AppState`
7. Application navigates to the Risk Assessment page

### 6.4.3 Sequence Diagram - Risk Assessment Flow

This sequence diagram shows how flood risk is calculated and displayed to the user.

```
User       RiskPage    AppState    RiskService  GeoJsonService  WeatherService  API
 │             │          │             │              │               │         │
 │  Navigate   │          │             │              │               │         │
 │────────────▶│          │             │              │               │         │
 │             │GetLatLng │             │              │               │         │
 │             │─────────▶│             │              │               │         │
 │             │◄─────────│             │              │               │         │
 │             │[6.9271,79.8612]        │              │               │         │
 │             │          │             │              │               │         │
 │             │   CalculateRisk()      │              │               │         │
 │             │────────────────────────▶│              │               │         │
 │             │          │             │LoadRiskZones()               │         │
 │             │          │             │─────────────▶│               │         │
 │             │          │             │◄─────────────│               │         │
 │             │          │             │ [Polygons]   │               │         │
 │             │          │             │              │               │         │
 │             │          │     Point-in-Polygon       │               │         │
 │             │          │         Algorithm          │               │         │
 │             │          │             │              │               │         │
 │             │          │◄────────────────────────────              │         │
 │             │          │ RiskResult(High/Med/Low)   │               │         │
 │             │          │             │              │               │         │
 │             │   GetWeather(lat,lng)  │              │               │         │
 │             │────────────────────────┼──────────────┼──────────────▶│         │
 │             │          │             │              │               │HTTP GET │
 │             │          │             │              │               │────────▶│
 │             │          │             │              │               │◄────────│
 │             │          │             │              │               │  JSON   │
 │             │◄───────────────────────┼──────────────┼───────────────│         │
 │             │   WeatherResult        │              │               │         │
 │             │          │             │              │               │         │
 │   Display   │          │             │              │               │         │
 │◄────────────│          │             │              │               │         │
 │  Results    │          │             │              │               │         │
```

**Flow Description:**

1. User navigates to Risk Assessment page (automatically after location selection)
2. Page retrieves selected coordinates from `AppState`
3. `RiskService` is invoked to calculate flood risk
4. `RiskService` requests GeoJSON polygon data from `GeoJsonService`
5. Point-in-Polygon algorithm determines which risk zone contains the user's location
6. Risk result (High/Medium/Low) is returned
7. Simultaneously, `WeatherService` fetches real-time weather data from Open-Meteo API
8. Both risk and weather data are displayed to the user

### 6.4.4 Class Diagram - Core Models and Services

```
┌─────────────────────────┐
│      AppState           │
│─────────────────────────│
│ + SelectedProvinceId    │
│ + SelectedDistrictId    │
│ + SelectedTownId        │
│ + SelectedLatLng        │
│ + MapCenter             │
│ + MapZoom               │
│─────────────────────────│
│ + NotifyStateChanged()  │
└─────────────────────────┘
           ▲
           │ uses
           │
┌──────────┴──────────────┐
│   LocationService       │
│─────────────────────────│
│ - provinces: List       │
│ - districts: List       │
│ - towns: List           │
│─────────────────────────│
│ + GetProvinces()        │
│ + GetDistricts(id)      │
│ + GetTowns(id)          │
└─────────────────────────┘

┌─────────────────────────┐         ┌─────────────────────────┐
│    RiskService          │────────▶│   GeoJsonService        │
│─────────────────────────│  uses   │─────────────────────────│
│ - geoJsonService        │         │ - httpClient            │
│─────────────────────────│         │─────────────────────────│
│ + CalculateRiskAsync()  │         │ + LoadRiskZonesAsync()  │
│ - IsPointInPolygon()    │         └─────────────────────────┘
└─────────────────────────┘

┌─────────────────────────┐
│   WeatherService        │
│─────────────────────────│
│ - httpClient            │
│ - baseUrl: string       │
│─────────────────────────│
│ + GetWeatherAsync()     │
└─────────────────────────┘

┌─────────────────────────┐
│     RiskResult          │
│─────────────────────────│
│ + Level: string         │
│ + Message: string       │
│ + ColorCode: string     │
│ + Score: double         │
└─────────────────────────┘

┌─────────────────────────┐
│   WeatherResult         │
│─────────────────────────│
│ + Temperature: double   │
│ + Rain: double          │
│ + WindSpeed: double     │
│ + UnitTemp: string      │
│ + UnitRain: string      │
│ + UnitWind: string      │
└─────────────────────────┘
```

---

## 6.5 Algorithm Design

The core computational logic of the Flood Management System is the **Point-in-Polygon (PIP) algorithm**, which determines whether a given GPS coordinate falls within a defined flood risk zone polygon.

### 6.5.1 Algorithm Overview

The system uses the **Ray Casting Algorithm**, a well-established geometric algorithm for point-in-polygon testing. This algorithm works by casting a ray from the test point to infinity and counting how many times the ray intersects the polygon boundary. If the number of intersections is odd, the point is inside the polygon; if even, the point is outside.

### 6.5.2 Algorithm Pseudocode

```
ALGORITHM: IsPointInPolygon(point, polygon)

INPUT: 
    point = (lat, lng) - GPS coordinates to test
    polygon = [(lat1,lng1), (lat2,lng2), ..., (latn,lngn)] - polygon vertices

OUTPUT:
    Boolean - TRUE if point is inside polygon, FALSE otherwise

BEGIN
    inside ← FALSE
    j ← length(polygon) - 1
    
    FOR i ← 0 TO length(polygon) - 1 DO
        xi ← polygon[i].lng
        yi ← polygon[i].lat
        xj ← polygon[j].lng
        yj ← polygon[j].lat
        
        // Check if horizontal ray from point crosses edge
        IF ((yi > point.lat) != (yj > point.lat)) THEN
            // Calculate intersection x-coordinate
            intersectX ← (xj - xi) * (point.lat - yi) / (yj - yi) + xi
            
            // If intersection is to the right of point, toggle inside flag
            IF (point.lng < intersectX) THEN
                inside ← NOT inside
            END IF
        END IF
        
        j ← i
    END FOR
    
    RETURN inside
END
```

### 6.5.3 Algorithm Flowchart

*[Insert flowchart diagram showing the complete flow from user location input through risk zone determination]*

```
                    START
                      │
                      ▼
         ┌────────────────────────┐
         │  Receive User Location │
         │   (Latitude, Longitude)│
         └────────────┬───────────┘
                      │
                      ▼
         ┌────────────────────────┐
         │ Load GeoJSON Risk Zones│
         │  (High, Medium, Low)   │
         └────────────┬───────────┘
                      │
                      ▼
         ┌────────────────────────┐
         │ Initialize Variables:  │
         │ inside = false         │
         │ j = vertices.length-1  │
         └────────────┬───────────┘
                      │
                      ▼
         ┌────────────────────────┐
         │ For each polygon zone  │◄─────────┐
         └────────────┬───────────┘          │
                      │                       │
                      ▼                       │
         ┌────────────────────────┐          │
         │ For each edge (i,j)    │          │
         └────────────┬───────────┘          │
                      │                       │
                      ▼                       │
         ┌────────────────────────┐          │
         │ Is yi > point.lat      │          │
         │   != yj > point.lat?   │          │
         └────┬───────────┬───────┘          │
              │ No        │ Yes               │
              │           ▼                   │
              │  ┌────────────────┐          │
              │  │ Calculate       │          │
              │  │ intersectX      │          │
              │  └────────┬───────┘          │
              │           │                   │
              │           ▼                   │
              │  ┌────────────────┐          │
              │  │ point.lng <    │          │
              │  │ intersectX?    │          │
              │  └────┬───────────┘          │
              │       │ Yes                  │
              │       ▼                       │
              │  ┌────────────────┐          │
              │  │ Toggle inside  │          │
              │  └────────────────┘          │
              │                               │
              └───────────┬───────────────────┘
                          │
                          ▼
              ┌────────────────────────┐
              │  More edges?           │──Yes──┐
              └────────┬───────────────┘       │
                       │ No                    │
                       ▼                       │
              ┌────────────────────────┐       │
              │  inside = true?        │       │
              └────┬───────────┬───────┘       │
                   │Yes        │No             │
                   ▼           ▼               │
         ┌─────────────┐ ┌──────────────┐    │
         │ Return Risk │ │ Check Next   │────┘
         │   Result    │ │   Polygon    │
         └──────┬──────┘ └──────────────┘
                │
                ▼
         ┌─────────────┐
         │ No match?   │
         │ Return "Low"│
         └──────┬──────┘
                │
                ▼
              END
```

### 6.5.4 Algorithm Complexity

- **Time Complexity**: O(n × m), where n is the number of risk zone polygons and m is the average number of vertices per polygon
- **Space Complexity**: O(1), as the algorithm only uses a constant amount of additional memory

For the current implementation with approximately 3-5 risk zones containing 10-50 vertices each, the algorithm executes in less than 1 millisecond on modern hardware.

---

## 6.6 UI Design

This section presents low-fidelity wireframes and design considerations for the three main pages of the application.

> **Note**: These are conceptual wireframes representing the design phase. The actual implemented UI with full styling and interactivity is documented in Chapter 7 (Implementation).

### 6.6.1 Landing Page Wireframe

```
┌─────────────────────────────────────────────────────┐
│                                                       │
│                                                       │
│                    ┌──────────┐                      │
│                    │          │                      │
│                    │   SLIC   │                      │
│                    │   LOGO   │                      │
│                    │          │                      │
│                    └──────────┘                      │
│                                                       │
│              Flood Management System                 │
│                                                       │
│                                                       │
│               [ Click to Enter → ]                   │
│                                                       │
│                                                       │
│                                                       │
└─────────────────────────────────────────────────────┘
```

**Design Considerations:**

- **Minimalist Approach**: Dark background (#111827) with centered content reduces cognitive load
- **Clear Call-to-Action**: Single entry point prevents confusion
- **Branding**: SLIC logo prominently displayed for brand recognition
- **Accessibility**: High contrast text, keyboard-accessible button

### 6.6.2 Location Selection Page Wireframe

```
┌──────────────────────────────────────────────────────────────┐
│  ← Back                  LOCATION SELECTION                   │
├──────────────────────┬───────────────────────────────────────┤
│                      │                                        │
│  SELECT LOCATION     │                                        │
│                      │         ┌────────────────┐            │
│  Province            │         │                 │           │
│  ┌─────────────────┐│         │                 │           │
│  │ Western       ▼ ││         │   Sri Lanka     │           │
│  └─────────────────┘│         │      Map        │           │
│                      │         │                 │           │
│  District            │         │    [Markers &   │           │
│  ┌─────────────────┐│         │   Risk Zones]   │           │
│  │ Colombo       ▼ ││         │                 │           │
│  └─────────────────┘│         │                 │           │
│                      │         │                 │           │
│  Town                │         └────────────────┘            │
│  ┌─────────────────┐│                                        │
│  │ Colombo       ▼ ││         Legend:                        │
│  └─────────────────┘│         🔴 High Risk                   │
│                      │         🟡 Medium Risk                 │
│  [   CONFIRM   ]    │         🟢 Low Risk                    │
│                      │                                        │
└──────────────────────┴───────────────────────────────────────┘
```

**Design Considerations:**

- **Progressive Disclosure**: Cascading dropdowns reveal options step-by-step
- **Visual Feedback**: Map updates dynamically as user selects locations
- **Spatial Context**: Map provides geographical awareness
- **Clear Hierarchy**: Left panel for input, right panel for visualization
- **Accessibility**: Dropdown menus are keyboard navigable, labels properly associated

### 6.6.3 Risk Assessment Page Wireframe

```
┌──────────────────────────────────────────────────────────────┐
│  ← Change Location        FLOOD RISK ASSESSMENT              │
├──────────────────────┬───────────────────────────────────────┤
│                      │                                        │
│  RISK SUMMARY        │                                        │
│  ┌─────────────────┐│         ┌────────────────┐            │
│  │  🟢 LOW RISK    ││         │                 │           │
│  │                 ││         │   Location Map  │           │
│  │  Safe Zone      ││         │   with Marker   │           │
│  └─────────────────┘│         │                 │           │
│                      │         │   [Risk Zone    │           │
│  WEATHER INFO        │         │    Overlays]    │           │
│  ┌─────────────────┐│         │                 │           │
│  │ 🌡️ 28.5°C      ││         │                 │           │
│  │ 💧 0.0mm       ││         │                 │           │
│  │ 💨 12.4 km/h   ││         └────────────────┘            │
│  └─────────────────┘│                                        │
│                      │         Legend:                        │
│  RECOMMENDATIONS     │         🔴 High Risk                   │
│  • Monitor weather   │         🟡 Medium Risk                 │
│  • Stay updated      │         🟢 Low Risk                    │
│                      │         📍 Your Location               │
│  EMERGENCY CONTACTS  │                                        │
│  📞 119 (Emergency)  │                                        │
│                      │                                        │
└──────────────────────┴───────────────────────────────────────┘
```

**Design Considerations:**

- **At-a-Glance Information**: Risk level prominently displayed with color coding
- **Contextual Data**: Weather information provides additional decision-making context
- **Actionable Guidance**: Recommendations based on risk level
- **Emergency Preparedness**: Quick access to emergency contact numbers
- **Responsive Map**: Interactive map allows zooming and panning for detailed exploration
- **Accessibility**: 
  - Color coding supplemented with text labels (not color-only)
  - Icons include text alternatives
  - Sufficient contrast ratios (WCAG AA compliant)

### 6.6.4 Color Accessibility

All risk level colors have been chosen to be distinguishable even with color blindness:

| Risk Level | Color   | Hex Code | Contrast Ratio |
|------------|---------|----------|----------------|
| High       | Red     | #EF4444  | 4.8:1         |
| Medium     | Orange  | #F59E0B  | 3.2:1         |
| Low        | Green   | #10B981  | 3.1:1         |

All contrast ratios meet WCAG 2.1 Level AA standards for large text.

---

## 6.7 Chapter Summary

This chapter has presented a comprehensive design blueprint for the Flood Management System, establishing the foundation for successful implementation. The key outcomes of the design phase include:

**Design Goals**: Six critical quality attributes—Performance, Usability, Accuracy, Reliability, Scalability, and Accessibility—were defined to guide all design decisions and ensure the system meets both functional and non-functional requirements.

**System Architecture**: A three-tier layered architecture was adopted, providing clear separation between the presentation layer (Blazor components), application layer (C# services), and data layer (static data and external APIs). This architecture promotes maintainability, testability, and future extensibility.

**Detailed Design**: Component diagrams, sequence diagrams, and class diagrams were developed to precisely specify how different parts of the system interact. Two critical workflows—location selection and risk assessment—were modeled in detail to ensure proper data flow and state management.

**Algorithm Design**: The Point-in-Polygon ray casting algorithm was selected and designed for efficient flood risk determination. With O(n×m) time complexity and O(1) space complexity, it provides near-instantaneous risk assessments suitable for real-time applications.

**User Interface Design**: Low-fidelity wireframes were created for all three main pages (Landing, Location Selection, Risk Assessment) with careful consideration of usability principles and accessibility standards. The design ensures that users can assess flood risk within three clicks while maintaining WCAG 2.1 AA compliance.

The design decisions documented in this chapter directly address the requirements identified in the SRS and provide a clear roadmap for the implementation phase. The modular architecture and well-defined interfaces enable parallel development of components and facilitate future enhancements such as additional data sources, mobile applications, or machine learning-based predictions.

The next chapter (Chapter 7: Implementation) details how this design was translated into a working system, documenting technology selections, development tools, and the actual code implementation of key features.
