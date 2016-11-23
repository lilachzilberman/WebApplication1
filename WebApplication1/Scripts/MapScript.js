var map;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {
            lat: 32.0853000,
            lng: 34.7817680
        },
        zoom: 8
    });
}