export function initMap(elementId) {
    var map = L.map(elementId).setView([7.8731, 80.7718], 7); // Center of Sri Lanka

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    return map;
}

export function updateMap(map, lat, lng, zoom) {
    if (map) {
        map.setView([lat, lng], zoom);
        // Optional: Add a marker
        // L.marker([lat, lng]).addTo(map);
    }
}
