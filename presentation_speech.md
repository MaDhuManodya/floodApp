# Presentation Speech Script - SLIC Flood Management System

**Total Duration:** 20 minutes  
**Delivery Pace:** ~140 words per minute (conversational)

---

## [0:00 - 0:40] Slide 1: Title Slide

"Good morning/afternoon. Welcome to my presentation on the SLIC Flood Management System—a real-time flood risk assessment platform developed for the Sri Lanka Insurance Corporation.

My name is [Your Name], student ID [Your IDs], and this project has been supervised by [Supervisor Name].

Today, I'll be presenting the development progress of this system, which aims to revolutionize how insurance providers and the public assess flood risks in Sri Lanka using cutting-edge geospatial analytics and real-time environmental data.

Let's begin."

---

## [0:40 - 1:00] Slide 2: Agenda

"I've structured this presentation into eight key sections.

First, I'll provide the problem background explaining the real-world challenges. Then, I'll outline the research problem and identified research gap based on existing literature.

Next, I'll introduce the project stakeholders, followed by the formal requirements specification showing what has been implemented.

I'll then present the system design and overall architecture, including low-level design diagrams.

After that, I'll share the updated time schedule highlighting any variations from the original plan, and discuss our progress since the PPRS submission.

Finally, I'll conclude with key takeaways and future work.

Let's start with the problem background."

---

## [1:00 - 2:30] Slide 3: Problem Background

"Sri Lanka faces a severe flooding crisis. As a tropical island nation, we experience intense monsoon seasons that bring devastating floods every year.

To put this in perspective: the 2016-2017 floods alone affected one-point-five million people, resulted in over 200 deaths, and caused economic losses exceeding 100 billion rupees, according to the Disaster Management Centre.

The World Bank's 2023 Climate Report projects that by 2050, flood risk in South Asia will increase by 25 percent. This is not a distant problem—it's happening now.

For insurance companies like SLIC, this creates several critical challenges:

First, manual risk assessment is extremely time-consuming and often inconsistent between different agents.

Second, there's a lack of real-time environmental data integration. Current assessments rely on outdated maps and historical data that don't reflect current weather conditions.

Third, policyholders have limited spatial awareness. They can't easily visualize whether their property is in a high-risk zone.

And fourth, delayed emergency response coordination means that by the time warnings are issued, it's often too late.

These challenges motivated the development of our system."

---

## [2:30 - 4:00] Slide 4: Research Problem & Research Gap

"This brings us to our research problem:

**How can insurance providers accurately assess and communicate flood risks in real-time using geospatial analytics and live environmental data?**

To answer this, I conducted an extensive literature review. Let me highlight the research gap I identified.

Looking at existing systems: The Disaster Management Centre provides static flood maps, but these have no real-time updates. Once published, they remain unchanged regardless of current weather.

Manual assessment processes are slow and inconsistent—two agents might give different risk assessments for the same location.

Generic risk zones lack granularity. They might mark an entire district as 'high risk' when only specific coordinates are actually dangerous.

And most importantly, these are expert-only tools with no user accessibility for the general public or insurance clients.

Our solution addresses each of these gaps:

For static maps, we integrate a live weather API that updates every hour.

For slow manual processes, we use an automated point-in-polygon algorithm that calculates risk in under one second.

For limited granularity, we provide coordinate-level precision—you can assess risk for your exact address.

And for accessibility, we built a public-facing web interface that anyone can use.

Key research papers supporting this gap include Zhang et al.'s 2022 work on GIS-based flood modeling, which noted the lack of real-time integration. Silva and Fernando's 2021 paper specifically called for API modernization in Sri Lankan flood systems. And the IPCC's 2023 report emphasized that real-time climate data significantly improves disaster response.

This research gap validation formed the foundation of our project."

---

## [4:00 - 5:15] Slide 5: Project Stakeholders

"Now let's look at who this system serves. I've visualized stakeholders using an onion diagram with four layers.

At the core, we have the **Core Team**: myself as the developer, my academic supervisor providing guidance, and the SLIC project manager representing the client.

The next layer contains **Functional Stakeholders** who keep the system running: System administrators who maintain the infrastructure, data analysts who interpret risk patterns, and IT support staff who handle technical issues.

Moving outward, we have **Operating Stakeholders** who use the system daily: SLIC insurance agents assessing client properties, policy underwriters making coverage decisions, and customer service teams helping clients understand their risk.

Finally, the outermost layer includes **External Stakeholders**: Government agencies like the Disaster Management Centre and Meteorology Department who provide data, the general public who can access risk information for free, and NGOs and relief organizations who coordinate emergency responses.

This multi-layered stakeholder model ensures the system serves both commercial insurance needs and broader public safety goals."

---

## [5:15 - 6:45] Slide 6: Requirements Specification

"Let me now present the formal requirements specification.

Starting with **Functional Requirements**, all six have been successfully implemented:

FR1: Cascading location selection from Province to District to Town—complete.

FR2: Point-in-polygon flood risk calculation using the ray-casting algorithm—complete.

FR3: Real-time weather data integration using the Open-Meteo API—complete.

FR4: Interactive Leaflet map visualization with dynamic zoom and pan—complete.

FR5: Risk level categorization into High, Medium, and Low with color coding—complete.

And FR6: Emergency contact information display for Disaster Management Centre and police—complete.

For **Non-Functional Requirements**, we've also met all targets:

NFR1 required risk calculation response time under 2 seconds—we achieved approximately 1 second average.

NFR2 specified map load time under 3 seconds—we're consistently at around 2 seconds.

NFR3 mandated modern browser compatibility—we've verified Chrome, Edge, and Firefox support.

And NFR4 required 95% API uptime reliability—the Open-Meteo API actually provides 99%+ uptime, exceeding our requirement.

This demonstrates that we've not only met but exceeded our initial requirements."

---

## [6:45 - 8:00] Slide 7: System Design

"The system design was guided by five key design goals:

First, **Accuracy**: We needed precise coordinate-based risk assessment, not just district-level approximations.

Second, **Real-time capability**: Live weather integration was non-negotiable for dynamic risk evaluation.

Third, **Usability**: The interface had to be intuitive enough for non-technical users—hence the cascading selectors.

Fourth, **Scalability**: A modular service architecture allows us to add new features like historical data analysis without major refactoring.

And fifth, **Performance**: We targeted sub-2-second response times to ensure a smooth user experience.

For the methodology, we used **Object-Oriented Analysis and Design** principles:

The component-based Blazor architecture naturally supports OOP with reusable components.

We implemented **Separation of Concerns** with a dedicated services layer for all business logic—no code duplication between UI and logic.

**Dependency Injection** was used throughout, creating loose coupling. Components request services through interfaces, not concrete implementations.

And we used centralized **State Management** with the AppState pattern, implementing the Observer design pattern for component communication.

This design foundation ensured clean, maintainable, and testable code."

---

## [8:00 - 10:30] Slide 8: Overall System Architecture

"Let me walk you through the high-level architecture, which follows a layered approach.

At the top, we have the **Client Layer** running in the user's browser. This includes Blazor Interactive Components for the UI, Leaflet.js for map rendering, and a real-time SignalR connection for live updates from the server.

Communication happens over HTTPS to the **Application Layer**, which runs on .NET 8 and contains four sub-layers:

The **Pages Layer** has three routable components: Landing.razor for the splash screen, LocationSelection.razor for choosing a location, and RiskLocation.razor for displaying results.

The **Components Layer** contains reusable widgets: LocationForm handles the cascading dropdowns, WeatherWidget displays live weather data, and RiskMapView renders the interactive map.

The **Services Layer** is where the real magic happens. This is our business logic tier. RiskService implements the point-in-polygon algorithm for risk calculation. WeatherService acts as an HTTP client for the Open-Meteo API. LocationService provides the hierarchical data for provinces, districts, and towns. And GeoJsonService loads and parses spatial polygon data.

Beneath that is **State Management** with AppState registered as a scoped service, maintaining user selections across all components.

Below the application layer, we have the **External API Layer**, which includes the Open-Meteo Weather API for real-time environmental data and OpenStreetMap for map tiles—both accessed via HTTPS.

Finally, at the bottom is the **Data Layer**, containing static GeoJSON files defining risk zones and in-memory location hierarchy data.

Now, let me explain the key algorithm: **Point-in-Polygon using Ray Casting**.

This geometric algorithm determines if a coordinate point falls inside a polygon boundary. We draw an imaginary ray from the point to infinity and count how many times it crosses the polygon edges. If the count is odd, the point is inside; if even, it's outside.

The time complexity is O(n times m), where n is the number of risk zones and m is the average number of vertices per polygon. However, we implemented an **early exit optimization**—as soon as we find a matching zone, we stop checking, significantly improving average performance."

---

## [10:30 - 11:30] Slide 9: Low-Level Design - Sequence Diagram

"This sequence diagram illustrates the risk assessment flow in detail.

The process starts when the user interacts with the LocationForm component to select a location.

LocationForm immediately updates the centralized AppState with the new selection—province ID, district ID, town ID, and crucially, the latitude-longitude coordinates.

The RiskService, subscribed to state changes, detects this update and triggers the CalculateRisk method.

RiskService then calls the GeoJsonService to load the polygon definitions for all risk zones. GeoJsonService returns the parsed polygon data.

RiskService now performs the point-in-polygon calculation, checking the user's coordinate against each zone until it finds a match—or determines the location is safe.

It returns a RiskResult object containing the risk level, message, and color code.

This result flows back through AppState, which notifies all subscribed components.

Finally, the UI components—the risk summary card, the map marker, and the recommendations panel—all update simultaneously to display the risk information.

This entire flow completes in under one second, providing near-instant feedback to the user."

---

## [11:30 - 12:30] Slide 10: Wireframes - User Interface Design

"Let me show you the user interface wireframes.

The **Landing Page** is minimalist: the SLIC logo centered, the application title 'SLIC Flood Management System' with the tagline 'Real-Time Risk Assessment,' and a single call-to-action button: 'Enter Dashboard.'

The **Location Selection Page** uses a split-view layout. On the left, we have the Location Form with three cascading dropdowns for Province, District, and Town, followed by a prominent 'Check Risk' button. On the right, there's a full-screen interactive map displaying Sri Lanka with zoom controls and a risk legend.

The **Risk Dashboard** also uses split-view. The left panel shows four stacked cards: Risk Summary with large, color-coded text—green for Low, yellow for Medium, red for High. Below that, the Weather Data card displays temperature, rainfall, and wind speed with icons. The Recommendations card provides context-aware safety advice. And at the bottom, the Emergency Contacts card shows hotline numbers for the DMC and police. The right panel displays the map with a pin marker at the selected location and a colored overlay showing the risk zone boundaries.

This design prioritizes clarity and accessibility—users can understand their risk at a glance."

---

## [12:30 - 14:00] Slide 11: Updated Time Schedule

"Here's the updated Gantt chart showing actual timelines versus original estimates.

**Phase 1: Requirements Gathering** was estimated at 2 weeks and completed exactly on time. No variance.

**Phase 2: System Design** was estimated at 2 weeks but took 2.5 weeks—a positive variance of half a week. The reason was additional stakeholder feedback iterations. During UI/UX review, the SLIC project manager requested changes to the color scheme and layout, which required multiple design revisions. This delay was acceptable as it significantly improved the final user experience.

**Phase 3: Core Development** was estimated at 4 weeks total and completed on time, but with internal variations:

- Location Service took exactly 1 week as planned.
- The Risk Algorithm took 1.5 weeks instead of 1 week—half a week over. This was because optimizing the point-in-polygon algorithm for large polygon datasets required more time than anticipated. I had to implement early exit logic and test numerous edge cases.
- Weather API integration was completed in only half a week instead of 1 week—half a week saved. The Open-Meteo API documentation was excellent, and the integration was straightforward.
- UI Components took exactly 1 week as planned.

These internal variances balanced out to keep the overall phase on schedule.

**Phase 4: Integration & Testing** was estimated at 2 weeks but completed in 1.5 weeks—half a week saved. The modular architecture with dependency injection made integration much smoother than expected. Components plugged together cleanly without major refactoring.

**Phase 5: Documentation** is currently in progress, estimated at 1 week, and on track to complete on time.

Overall, despite minor variances, the project has adhered closely to the original schedule."

---

## [14:00 - 15:30] Slide 12: Progress Since PPRS

"Let me highlight what has been accomplished since the PPRS submission.

First, **Full System Implementation**: At the time of PPRS, we had only completed the requirements analysis and initial design. Now, all six functional requirements and all four non-functional requirements have been fully implemented and tested.

Second, **Core Features Delivered**: The point-in-polygon risk calculation algorithm is operational and optimized. The Open-Meteo API integration is live and pulling real-time weather data every hour. The interactive Leaflet map dynamically updates with zoom and pan based on user selections. And the responsive Blazor UI provides real-time updates via SignalR without any page refreshes.

Third, **Testing & Optimization**: We've achieved performance optimization—response time is consistently under 2 seconds, averaging around 1 second. Cross-browser compatibility has been verified on Chrome, Edge, and Firefox. And API error handling has been implemented so that if Open-Meteo is unavailable, the system degrades gracefully and shows cached or fallback data.

Fourth, **Documentation**: Technical documentation is complete, including architecture diagrams, API documentation, and code comments. The user guide is in progress and will be finalized this week. And the video demonstration you'll see next has been prepared.

As for **Remaining Work**, we still need to conduct final user acceptance testing with actual SLIC agents. We're preparing deployment to a staging environment for broader testing. And we plan to set up performance monitoring to track API response times and system usage patterns in production.

This represents substantial progress from a design document to a fully functional prototype."

---

## [15:30 - 16:30] Slide 13: Technical Achievements

"I'd like to highlight some key technical achievements.

In terms of **Algorithmic Efficiency**, the point-in-polygon algorithm with early exit optimization achieves an average risk calculation time of 800 milliseconds. This is well below our 2-second target.

For **Real-Time Integration**, the weather API typically responds in 200 milliseconds, and map rendering completes in approximately 1.5 seconds. Combined, the entire page load from clicking 'Check Risk' to seeing results takes under 2 seconds.

Regarding **Architecture Quality**, we've implemented clean separation of concerns—UI code has zero business logic. We use 100% dependency injection—no 'new' keywords for service instantiation, making the entire service layer highly testable. In fact, I've written unit tests for RiskService and WeatherService that run in isolation.

And for **User Experience**, the intuitive cascading selectors guide users naturally from broad to specific selections. Color-coded risk visualization uses universal colors: red for danger, yellow for caution, green for safe. And the responsive design is ready for desktop, tablet, and mobile devices, though mobile optimization will be refined in the next phase.

These achievements demonstrate not just working code, but high-quality, production-ready software."

---

## [16:30 - 17:30] Slide 14: Challenges & Solutions

"Every project faces challenges. Let me share the major ones we encountered and how we solved them.

**Challenge 1: Large GeoJSON files slowing initial load.** The risk zone polygons for all of Sri Lanka resulted in a 2-megabyte file. Initial solution was lazy loading—only fetch when needed. But we went further with early exit optimization in the algorithm, so we don't process all polygons unnecessarily.

**Challenge 2: API rate limiting concerns.** While Open-Meteo is free, I was concerned about rate limits with multiple users. Solution: implement caching—weather data is cached for 1 hour per coordinate since weather doesn't change minute-to-minute. Plus, we added fallback error handling to show last-known data if the API is unavailable.

**Challenge 3: Complex polygon calculations.** Some risk zones have hundreds of vertices. Solution: the ray casting algorithm is naturally O(n), but by sorting zones by likelihood—checking high-risk zones first—we often exit early.

**Challenge 4: State synchronization across components.** With multiple components needing the same location data, prop drilling would create a nightmare. Solution: centralized AppState with the Observer pattern. One source of truth, automatic updates.

**Challenge 5: User understanding of risk levels.** Initial testing showed users didn't always understand what 'Medium Risk' meant. Solution: we added color-coded UI with explicit icons and contextual recommendations like 'Monitor weather reports' or 'Evacuation may be required.'

These solutions demonstrate practical problem-solving beyond just writing code."

---

## [17:30 - 18:45] Slide 15: Conclusion

"In conclusion, let me summarize the key takeaways.

We've successfully **solved the problem** of providing real-time, accessible flood risk assessment specifically for the Sri Lankan context.

Our **innovation** lies in combining geospatial analytics—specifically point-in-polygon algorithms—with live environmental data via API integration. This combination doesn't exist in current DMC or SLIC systems.

The **technology stack**—modern .NET 8 Blazor with clean architecture—ensures the system is maintainable, scalable, and performant.

And the **impact** is tangible: insurance agents can make data-driven coverage decisions in seconds instead of hours. The general public can check if their home is in a flood zone. And emergency responders can identify high-risk populations before disasters strike.

Looking ahead, we have several **future enhancements** planned:

First, **database integration** using PostgreSQL with the PostGIS extension for persistent storage and more efficient spatial queries.

Second, **historical data integration** for trend analysis—imagine showing 'this area has been high-risk for 3 out of the last 5 years.'

Third, **native mobile applications** for iOS and Android with offline capabilities.

Fourth, **SMS alert integration** so users can subscribe to automatic notifications when their area enters a high-risk state.

And fifth, **multi-language support** with Sinhala and Tamil localization to reach the entire Sri Lankan population.

Finally, our **project success metrics** show we've delivered: all functional requirements complete, all performance targets exceeded, stakeholder requirements met, and a scalable, maintainable codebase.

This project demonstrates that real-time disaster risk assessment is not just possible—it's practical and impactful."

---

## [18:45 - 20:00] Slide 16: References

"For full transparency, here are the key references cited in this presentation, listed alphabetically:

The Disaster Management Centre's 2017 Sri Lanka Flood Disaster Report provided the statistics on 2016-2017 flood impacts.

The IPCC's 2023 Climate Change Synthesis Report gave us the projection of 25% increased flood risk by 2050.

Open-Meteo's API documentation guided our weather integration implementation.

Silva and Fernando's 2021 paper on GIS-based flood risk mapping in Sri Lanka directly informed our research gap analysis.

The World Bank's 2023 South Asia Climate and Development Report contextualized the regional climate crisis.

Zhang, Wang, and Chen's 2022 IEEE paper on real-time flood monitoring using web-based GIS provided the algorithmic foundation.

And of course, the technical documentation for Leaflet.js and Microsoft's .NET 8 Blazor framework supported our implementation.

All these references are available in the written report for further reading.

---

This concludes my presentation. Thank you for your attention. I'm happy to answer any questions you may have."

---

## Delivery Notes

### Pacing Tips
- **Slide Transitions**: 2-3 second pause between slides
- **Technical Terms**: Emphasize and speak slightly slower for: "Point-in-Polygon," "Ray Casting," "SignalR," "GeoJSON"
- **Statistics**: Pause before and after numbers for emphasis
- **Breathing Points**: Natural pauses at bullet point transitions

### Emphasis Words
- "Real-time" (say this with energy)
- "One-point-five million affected" (slow down)
- "Under one second" (emphasize speed)
- "All requirements complete" (confident tone)

### Common Mistakes to Avoid
- Don't rush the architecture diagram—give viewers time to read
- Don't skip over variance explanations in the Gantt chart
- Don't forget to point/gesture at diagrams when referencing them
- Don't go monotone—vary pitch for engagement

### Recording Setup
- **Camera**: Eye level, well-lit face
- **Screen**: Share presentation in full-screen mode
- **Laser Pointer**: Use on-screen cursor/annotation for diagrams
- **Backup**: Record locally AND stream to YouTube simultaneously
