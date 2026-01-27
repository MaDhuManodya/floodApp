export function initMap(elementId, center, zoom, markers) {
    const map = L.map(elementId).setView([center.lat, center.lng], zoom);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    if (markers) {
        markers.forEach(m => {
            L.marker([m.lat, m.lng]).addTo(map)
                .bindPopup(m.popup);
        });
    }

    // Use ResizeObserver to ensure map checks its size whenever the container changes
    const resizeObserver = new ResizeObserver(() => {
        map.invalidateSize();
    });
    resizeObserver.observe(document.getElementById(elementId));

    // Store observer on map object to disconnect later if needed (optional but good practice)
    map._resizeObserver = resizeObserver;

    return map;
}

export function addGeoJson(map, geoJsonData, styleCallbackName) {
    L.geoJSON(geoJsonData, {
        style: function (feature) {
            // Simple style mapping based on properties
            var risk = feature.properties.riskLevel;
            var color = '#10B981'; // Low
            if (risk === 'High') color = '#EF4444';
            if (risk === 'Medium') color = '#F59E0B';

            return { color: color, weight: 2, fillOpacity: 0.4 };
        }
    }).addTo(map);
}

export function setView(map, lat, lng, zoom) {
    map.setView([lat, lng], zoom);
}

export function addClickEvent(map, dotNetHelper) {
    map.on('click', function (e) {
        dotNetHelper.invokeMethodAsync('OnMapClick', { lat: e.latlng.lat, lng: e.latlng.lng });
    });
}

export function addMarker(map, lat, lng) {
    if (window.currentMarker) {
        map.removeLayer(window.currentMarker);
    }
    window.currentMarker = L.marker([lat, lng]).addTo(map);
}
