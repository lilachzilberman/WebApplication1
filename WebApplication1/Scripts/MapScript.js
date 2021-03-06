﻿$(function () {
    google.maps.visualRefresh = true;

    // Setting the map definitions
    var mapOptions = {
        zoom: 8,
        center: new google.maps.LatLng(32.0853000, 34.7817680),
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // Bind #map_canvas element
    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

    var tooltip = new google.maps.InfoWindow({
        size: new google.maps.Size(150, 50)
    });

    // Get the branches json array with jquery
    $.getJSON("../Branches/GetBranchesInJsonFormat", function (branches) {
        var geocoder = new google.maps.Geocoder();
        branches.forEach(function (branch) {
            geocoder.geocode({ address: branch.Address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {

                        var content = $('<div>')
                            .append($('<h4>').text(branch.Name))
                            .append($('<div>').text(branch.Address))
                            .append($('<div>').text(branch.Phone))
                            .html();

                        var marker = new google.maps.Marker({
                            position: results[0].geometry.location,
                            map: map,
                            title: branch.Name
                        });
                        google.maps.event.addListener(marker, 'click', function () {
                            tooltip.setContent(content);
                            tooltip.open(map, marker);
                        });

                    } else {
                        console.log("No results found" + branch.Address);
                    }
                } else {
                    console.log("Geocode was not successful for the following reason: " + status + branch.Address);
                }
            });
        });
    });
});